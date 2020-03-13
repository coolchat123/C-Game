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
        bool GameState = false;
        int WantedDirection = 1;
        int Direction = 1;
        int Life = 2;
        int Score = 0;
        static Color MapCol = new Color(252,188,176);
        static List<SSprite> SuperPoints;
        static List<SSprite> Points;
        static SSprite Map;
        public static Image CollisionMap;
        static SSprite PacMan;
        static SSprite GhostPink;
        static SSprite GhostBlue;
        static SSprite GhostOrange;
        static SSprite GhostRed;
        static SText ScoreText;
        static int ScoreP;
        static SSprite BeginScreen;
        string music = "Content/Pacman/eatpcS.wav";
        static List<SSprite> Lives;
        static SText LivesText;
        
        public Pacman() : base() { }

        public override void LoadContent()
        {
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
            Map.Position = new Vector2f(10, Program.Texture.Size.Y / 2 - Map.Texture.Size.Y / 2);
            Points = PointSet();
            PacMan = new SSprite(Color.Yellow, 16, 12);
            GhostRed = new SSprite(Color.Red, 16, 12);
            GhostBlue = new SSprite(Color.Blue, 16, 12);
            GhostPink = new SSprite(new Color(255, 0, 200), 16, 12);
            GhostOrange = new SSprite(new Color(255, 200, 0), 16, 12);
            CollisionMap = new Image("Content/Pacman/CollisionMap.png");
            BeginScreen = new SSprite(new Texture("Content/Pacman/BGscreenpm.png"));
            LivesText = new SText("lives",11);
            Lives = new List<SSprite>();
            SetLives(3);
        }

        public override void Initialise()
        {
            int scoreboardLeft = (int)Map.Position.X + (int)Map.Texture.Size.X + 2;
            int scoreboardWidth = (int)Program.Texture.Size.X - scoreboardLeft;
            SuperPoints[0].Position = new Vector2f(19, 17);
            SuperPoints[1].Position = new Vector2f(Map.Texture.Size.X - 5, 17);
            SuperPoints[2].Position = new Vector2f(19, Map.Texture.Size.Y - 35);
            SuperPoints[3].Position = new Vector2f(Map.Texture.Size.X - 5 , Map.Texture.Size.Y - 35);
            ScoreText.Position = new Vector2f(scoreboardLeft+ scoreboardWidth /2 - ScoreText.GetGlobalBounds().Width /2 , 10);
            ScoreText.Color = new Color(173,216,230);
            PacMan.Position = new Vector2f(90, Map.Position.Y + 100);
            Vector2u jailPosition = new Vector2u(88, 80);
            GhostBlue.Position = new Vector2f(Map.Position.X + jailPosition.X, Map.Position.Y + jailPosition.Y);
            GhostPink.Position = new Vector2f(Map.Position.X + jailPosition.X + 16, Map.Position.Y + jailPosition.Y);
            GhostRed.Position = new Vector2f(Map.Position.X + jailPosition.X + 32, Map.Position.Y + jailPosition.Y);
            GhostOrange.Position = new Vector2f(Map.Position.X + jailPosition.X + 10, Map.Position.Y + jailPosition.Y - 20);
            BeginScreen.Position = Map.Position;
            LivesText.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 5, 20);
            for (int i = 0; i < Lives.Count; i++)
            {
                Lives[i].Position = new Vector2f(LivesText.Position.X , LivesText.Position.Y + LivesText.GetGlobalBounds().Height + 17 * i + 5);
            } 
        }

        public override void Update(GameTime gameTime)
        {
            if (!GameState)
            {
                foreach(SSprite point in Points)
                {
                    Program.Sprites.Remove(point);
                }
                Points.Clear();

                if (Score == 0)
                {
                    ScoreText.Position = new Vector2f(2555, 2555);
                }
                else
                {
                    ScoreText.Position = new Vector2f(Program.Texture.Size.X / 2 - ScoreText.GetGlobalBounds().Width / 2, Program.Texture.Size.Y / 2 - ScoreText.GetGlobalBounds().Height / 2 - 20);
                }
                BeginScreen.Position = Map.Position;
            }
            else
            {
                int scoreboardLeft = (int)Map.Position.X + (int)Map.Texture.Size.X + 2;
                int scoreboardWidth = (int)Program.Texture.Size.X - scoreboardLeft;
                BeginScreen.Position = new Vector2f(2555, 2555);
                //Console.WriteLine(PacMan.Position);
                ScoreText.DisplayedString = Score.ToString();
                ScoreText.Position = new Vector2f(scoreboardLeft + scoreboardWidth / 2 - ScoreText.GetGlobalBounds().Width / 2, 10);
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
                if (Points.Count == 0 && SuperPoints.Count == 0)
                {
                    GameState = false;
                }
                for (int i = 0; i < SuperPoints.Count; i++)
                {
                    if (PacMan.GetGlobalBounds().Intersects(SuperPoints[i].GetGlobalBounds()))
                    {
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
            }
        }

        public List<SSprite> PointSet()
        {
            Console.WriteLine("ey");

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



        // 0 = W; 1 = A; 2 = S; 3 = D;
        public override void KeyInput(Keyboard.Key key)
        {
            if (key == Keyboard.Key.Return && !GameState)
            {
                RestartLevel();
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
            if (key == Keyboard.Key.Space)
            {
                SetLives(Lives.Count - 1);
            }
        } 
        public bool CheckCollision(float x, float y, float x2, float y2)
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
        public int CheckPoints()
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

        public void SetLives(int newLives)
        {
            Console.WriteLine(Lives.Count + " -> " + newLives);
            while (Lives.Count != newLives)
            {
                if (Lives.Count > newLives)
                {
                    if (Lives.Count > 0)
                    {
                        Program.Sprites.Remove(Lives[Lives.Count - 1]);
                        Lives.RemoveAt(Lives.Count - 1);
                    }
                    else
                    {
                        GameState = false;
                        return;
                    }
                }
                else
                {
                    SSprite newLife = new SSprite(Color.Yellow, 16, 12);
                    newLife.Position = new Vector2f(LivesText.Position.X, LivesText.Position.Y + LivesText.GetGlobalBounds().Height + 17 * Lives.Count + 5);
                    Program.Sprites.Add(newLife);
                    Lives.Add(newLife);
                }
            }
     
        }

        public void SuperMode()
        {
            //if ghost = eaten Score += 200
            bool GhostsEat = true;
            Score += 50;
        }

        public void RestartLevel()
        {
            foreach (SSprite pellet in Points)
            {
                Program.Sprites.Remove(pellet);
            }
            Points.Clear();
            Points = PointSet();
            GameState = true;
            SetLives(3);
        }
    }
}
