using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;
using System.Threading;



namespace Game
{
    public class Pacman : GameLoop
    {
        // 0 = W; 1 = A; 2 = S; 3 = D;
        // GameState 0=BeginScreen; 1=Game; 2=GameOver
        int Life = 3;
        bool GameState = false;
        public static bool Hunt;
        int WantedDirection = 1;
        int Direction = 1;
        static int Score = 0;
        static Color MapCol = new Color(252,188,176);
        static List<SSprite> SuperPoints;
        static List<SSprite> Points;
        public static SSprite Map;
        public static Image CollisionMap;
        public static Image GhostCollisionMap;
        static SSprite PacMan;
        static SSprite Life1;
        static SSprite Life2;
        static SSprite Life3;
        static Ghost[] Ghosts;
        static SText ScoreText;
        static int ScoreP;
        static SSprite BeginScreen;
        string music = "Content/Pacman/eatpcS.wav";
        
        public Pacman() : base() { }

        public override void LoadContent()
        {
            Hunt = false;
            // 1 point = 20 points
            // eat ghost = 60 points
            SuperPoints = new List<SSprite>();
            Points = new List<SSprite>();
            ScoreText = new SText(ScoreP.ToString(), 11);
            Map = new SSprite(new Texture("Content/Pacman/Map.png"));
            Texture SuperPoint = new Texture("Content/Pacman/SuperPoint.png");
            SSprite SuperPoint1 = new SSprite(SuperPoint);
            SSprite SuperPoint2 = new SSprite(SuperPoint);
            SSprite SuperPoint3 = new SSprite(SuperPoint);
            SSprite SuperPoint4 = new SSprite(SuperPoint);
            SuperPoints.Add(SuperPoint1);
            SuperPoints.Add(SuperPoint2);
            SuperPoints.Add(SuperPoint3);
            SuperPoints.Add(SuperPoint4);
            Map.Position = new Vector2f(5, Program.Texture.Size.Y / 2 - Map.Texture.Size.Y / 2);
            Points = PointSet();
            PacMan = new SSprite(Color.Yellow, 16, 12);
            Ghosts = new Ghost[4];
            Ghosts[0] = new Ghost(Color.Red);
            Ghosts[1] = new Ghost(Color.Blue);
            Ghosts[2] = new Ghost(new Color(255, 0, 200));
            Ghosts[3] = new Ghost(new Color(255, 200, 0));
            Life1 = new SSprite(new Texture("Content/Pacman/PacManLife.png"));
            Life2 = new SSprite(new Texture("Content/Pacman/PacManLife.png"));
            Life3 = new SSprite(new Texture("Content/Pacman/PacManLife.png"));
            CollisionMap = new Image("Content/Pacman/CollisionMap.png");
            GhostCollisionMap = new Image("Content/Pacman/GhostCollisionMap.png");
            BeginScreen = new SSprite(new Texture("Content/Pacman/BGscreenpm.png"));

        }

        public override void Initialise()
        {
            SuperPoints[0].Position = new Vector2f(14, 17);
            SuperPoints[1].Position = new Vector2f(Map.Texture.Size.X - 9, 17);
            SuperPoints[2].Position = new Vector2f(14, Map.Texture.Size.Y - 35);
            SuperPoints[3].Position = new Vector2f(Map.Texture.Size.X - 9 , Map.Texture.Size.Y - 35);
            ScoreText.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 18, 10);
            Life1.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 2, 100);
            Life2.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 14, 100);
            Life3.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 26, 100);
            ScoreText.Color = new Color(173,216,230);
            PacMan.Position = new Vector2f(90, Map.Position.Y + 100);
            Vector2u jailPosition = new Vector2u(88, 80);
            Ghosts[0].Position = new Vector2f(Map.Position.X + jailPosition.X, Map.Position.Y + jailPosition.Y);
            Ghosts[1].Position = new Vector2f(Map.Position.X + jailPosition.X + 16, Map.Position.Y + jailPosition.Y);
            Ghosts[2].Position = new Vector2f(Map.Position.X + jailPosition.X + 32, Map.Position.Y + jailPosition.Y);
            Ghosts[3].Position = new Vector2f(Map.Position.X + jailPosition.X + 10, Map.Position.Y + jailPosition.Y - 20);
            Ghosts[3].Jailed = false;
            BeginScreen.Position = Map.Position;
        }

        public override void Update(GameTime gameTime)
        {
            if (!GameState)
            {

                if (Score == 0)
                {
                    ScoreText.Position = new Vector2f(2555, 2555);
                }
                else
                {
                    ScoreText.Position = new Vector2f(Program.Texture.Size.X / 2 - ScoreText.GetGlobalBounds().Width / 2, Program.Texture.Size.Y / 2 - ScoreText.GetGlobalBounds().Height / 2 - 20);
                }
                Life = 3;
                Life1.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 2, 100);
                Life2.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 14, 100);
                Life3.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 26, 100);
                BeginScreen.Position = Map.Position;
            }
            else
            {
                {
                    
                    if (PacMan.Position == Ghosts[0].Position || PacMan.Position == Ghosts[1].Position || PacMan.Position == Ghosts[2].Position || PacMan.Position == Ghosts[3].Position) { 
                        PacMan.Position = new Vector2f(90, Map.Position.Y + 100);
                        Life -= 1;
                        Console.WriteLine(Life.ToString());
                        if (Life == 0)
                        {
                            Life3.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 2, 1000);
                            GameState = false;
                        }
                        if (Life == 2)
                        {
                            Life1.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 2, 1000);
                        }
                        if (Life == 1)
                        {
                            Life2.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 2, 1000);
                        }
                    }
                

                ScoreText.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X , 10);
                BeginScreen.Position = new Vector2f(2555, 2555);
                ScoreText.DisplayedString = Score.ToString();
                for (int i = 0; i < Points.Count; i++)
                {
                    if (PacMan.GetGlobalBounds().Intersects(Points[i].GetGlobalBounds()))
                    {
                        Program.Sprites.Remove(Points[i]);
                        Points.RemoveAt(i);
                        if (i > 0)
                        {
                            i -= 1;
                        }

                        Score += 20;
                    }
                }
                //if (Points.Count == 0 && SuperPoints.Count == 0)
                //{
                //}
                
                }
                for (int i = 0; i < SuperPoints.Count; i++)
                {
                    if (PacMan.GetGlobalBounds().Intersects(SuperPoints[i].GetGlobalBounds()))
                    {
                        Console.WriteLine("test");
                        Program.Sprites.Remove(SuperPoints[i]);
                        SuperPoints.RemoveAt(i);
                        SuperMode();

                    }
                }
                if (PacMan.Position == new Vector2f(10, 86))
                {
                    PacMan.Position = new Vector2f(214, 86);
                }
                if (PacMan.Position == new Vector2f(218, 86))
                {
                    PacMan.Position = new Vector2f(10, 86);
                }

                if (WantedDirection == 0)
                {
                    if (CheckCollision(PacMan.Position.X - Map.Position.X, PacMan.Position.Y - 1f - Map.Position.Y,
                        PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y))
                    {
                        Direction = 0;
                    }
                }

                if (Direction == 0)
                {
                    if (CheckCollision(PacMan.Position.X - Map.Position.X, PacMan.Position.Y - 1f - Map.Position.Y,
                        PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y))
                    {
                        PacMan.SetPosition(PacMan.Position.X, PacMan.Position.Y - 1f);
                    }
                }

                if (WantedDirection == 1)
                {
                    if (CheckCollision(PacMan.Position.X - 1f - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                       PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y))
                    {
                        Direction = 1;
                    }
                }
                if (Direction == 1)
                {
                    if (CheckCollision(PacMan.Position.X - 1f - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                       PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y))
                    {
                        PacMan.SetPosition(PacMan.Position.X - 1f, PacMan.Position.Y);
                    }
                }
                if (WantedDirection == 2)
                {
                    if (CheckCollision(PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y,
                        PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y + 1f - Map.Position.Y))
                    {
                        Direction = 2;
                    }
                }
                if (Direction == 2)
                {
                    if (CheckCollision(PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y,
                        PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y + 1f - Map.Position.Y))
                    {
                        PacMan.SetPosition(PacMan.Position.X, PacMan.Position.Y + 1f);
                    }
                }

                if (WantedDirection == 3)
                {
                    if (CheckCollision(PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                    PacMan.Position.X + PacMan.Texture.Size.X + 1f - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y))
                    {
                        Direction = 3;
                    }
                }
                if (Direction == 3)
                {
                    if (CheckCollision(PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                     PacMan.Position.X + PacMan.Texture.Size.X + 1f - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y))
                    {
                        PacMan.SetPosition(PacMan.Position.X + 1f, PacMan.Position.Y);

                    }
                }

                foreach(Ghost ghost in Ghosts)
                {
                    ghost.Move();
                }
            }
        }

        public List<SSprite> PointSet()
        {
            List<SSprite> result = new List<SSprite>();
            Image pointMap = new Image("Content/Pacman/pointmap.png");
            for (uint i = 0; i < pointMap.Size.X; i++)
            {
                for(uint j = 0; j < pointMap.Size.Y; j++)
                {
                    if (pointMap.GetPixel(i, j) == Color.Red)
                    {
                        SSprite newpoint = new SSprite(new Color(252, 188, 176), 2, 2);
                        newpoint.Position = new Vector2f(i + Map.Position.X , j + Map.Position.Y);
                        result.Add(newpoint);
                    }
                }
            }
            return result;
        }

        public override void KeyInput(Keyboard.Key key)
        {
            if (key == Keyboard.Key.Return && !GameState)
            {
                //Points = PointSet();
                GameState = true;
            }
            if (key == Keyboard.Key.W)
            {
                WantedDirection = 0;

            }
            if (key == Keyboard.Key.A)
            {
                WantedDirection = 1;

            }
            if (key == Keyboard.Key.S)
            {

                WantedDirection = 2;


            }
            if (key == Keyboard.Key.D)
            {
                WantedDirection = 3;


            }
        }

        public static bool CheckCollision(float x, float y, float x2, float y2)
        {
            bool canMove = true;
            for (float xCheck = x; xCheck < x2; xCheck++)
            {
                for (float yCheck = y; yCheck < y2; yCheck++)
                {
                    if (CollisionMap.GetPixel((uint)xCheck, (uint)yCheck) == Color.Blue)
                    {

                        canMove = false;
                    }
                }
            }
            return canMove;
        }

        // Point is 2px/2px color of a point is Color.Red;
        public static int CheckPoints()
        {
            Image MapImage = Map.Texture.CopyToImage();

            for (float xCheck = PacMan.Position.X - Map.Position.X; xCheck < PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X; xCheck++)
            {
                for (float yCheck = PacMan.Position.Y - Map.Position.Y; yCheck < PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y; yCheck++)
                {
                    if (MapImage.GetPixel((uint)xCheck, (uint)yCheck) == MapCol)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++){
                                MapImage.SetPixel((uint)(xCheck + i), (uint)(yCheck + j), Color.Black);
                            }
                        }
                        Score += 20;
                    }
                    Map.Texture = new Texture(MapImage);
                }
            }
            return Score;
        }
        public void SuperMode()
        {
            //if ghost = eaten Score += 200
            bool GhostsEat = true;
            Score += 50;
        }
    }
}
