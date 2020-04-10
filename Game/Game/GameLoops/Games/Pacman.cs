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
        bool GhostsEat = false;
        int glob = 300;
        int Ghost1Timer = 180;
        bool Ghost1D = false;
        int Ghost2Timer = 280;
        bool Ghost2D = false;
        int Ghost3Timer = 280;
        bool Ghost3D = false;
        int Ghost4Timer = 280;
        bool Ghost4D = false;
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
        static SSprite PacManR;
        static SSprite PacManUp;
        static SSprite PacManDown;
        static SSprite PacManL;
        static SSprite Life1;
        static SSprite Life2;
        static SSprite Life3;
        static Ghost[] Ghosts;
        static SText ScoreText;
        static int ScoreP;
        static SSprite BeginScreen;
        string eatm = "Content/Pacman/eatpcS.wav";
        string beginm = "Content/Pacman/beginpcS.wav";
        string diem = "Content/Pacman/diepcS.wav";

        public Pacman() : base() { }

        public override void LoadContent()
        {
            PlaySound(beginm, true);
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
            PacMan = new SSprite(Color.Black, 16, 12);
            PacManUp = new SSprite(new Texture("Content/Pacman/PacmanUp.png"));
            PacManR = new SSprite(new Texture("Content/Pacman/PacmanR.png"));
            PacManL = new SSprite(new Texture("Content/Pacman/PacmanL.png"));
            PacManDown = new SSprite(new Texture("Content/Pacman/PacmanDown.png"));
            Ghosts = new Ghost[4];
            Ghosts[0] = new Ghost(new Texture("Content/Pacman/PacManLife.png"));
            Ghosts[1] = new Ghost(new Texture("Content/Pacman/PacManLife.png"));
            Ghosts[2] = new Ghost(new Texture("Content/Pacman/PacManLife.png"));
            Ghosts[3] = new Ghost(new Texture("Content/Pacman/PacManLife.png"));
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
            PacManUp.Position = new Vector2f(1000, 1000);
            PacManR.Position = new Vector2f(1000, 1000);
            PacManL.Position = new Vector2f(1000, 1000);
            PacManDown.Position = new Vector2f(1000, 1000);
            PacMan.Position = new Vector2f(90, Map.Position.Y + 100);
            Vector2u jailPosition = new Vector2u(88, 80);
            Ghosts[0].Position = new Vector2f(Map.Position.X + jailPosition.X + 0, Map.Position.Y + jailPosition.Y - 20);
            Ghosts[1].Position = new Vector2f(Map.Position.X + jailPosition.X + 10, Map.Position.Y + jailPosition.Y - 20);
            Ghosts[2].Position = new Vector2f(Map.Position.X + jailPosition.X + 10, Map.Position.Y + jailPosition.Y - 20);
            Ghosts[3].Position = new Vector2f(Map.Position.X + jailPosition.X + 10, Map.Position.Y + jailPosition.Y - 20);
            Ghosts[3].Jailed = true;
            Ghosts[2].Jailed = true;
            Ghosts[1].Jailed = true;
            BeginScreen.Position = Map.Position;
        }

        public override void Update(GameTime gameTime)
        {
            CheckIfDead();
            if (GhostsEat == true)
            {
                glob -= 1;
            }
            if (glob == 0)
            {
                GhostsEat = false;
            }

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
                    
                    if (PacMan.Position == Ghosts[0].Position && GhostsEat == false || PacMan.Position == Ghosts[1].Position && GhostsEat == false || PacMan.Position == Ghosts[2].Position && GhostsEat == false || PacMan.Position == Ghosts[3].Position && GhostsEat == false) { 
                        PacMan.Position = new Vector2f(90, Map.Position.Y + 100);
                        WantedDirection = 1;
                        Life -= 1;
                        if (Life == 0)
                        {
                            PlaySound(diem, false);
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
                    Vector2u jailPosition = new Vector2u(88, 80);
                    if (PacMan.Position == Ghosts[0].Position && GhostsEat == true)
                    {
                        Score += 100;
                        Ghost1D = true;
                        Ghosts[0].Position = new Vector2f(Map.Position.X + jailPosition.X, Map.Position.Y + jailPosition.Y);
                    }
                    if (PacMan.Position == Ghosts[1].Position && GhostsEat == true)
                    {
                        Score += 100;
                        Ghost2D = true;
                        Ghosts[1].Position = new Vector2f(Map.Position.X + jailPosition.X + 16, Map.Position.Y + jailPosition.Y);
                    }
                    if (PacMan.Position == Ghosts[2].Position && GhostsEat == true)
                    {
                        Score += 100;
                        Ghost3D = true;
                        Ghosts[2].Position = new Vector2f(Map.Position.X + jailPosition.X + 32, Map.Position.Y + jailPosition.Y);
                    }
                    if (PacMan.Position == Ghosts[3].Position && GhostsEat == true)
                    {
                        Score += 100;
                        Ghost4D = true;
                        Ghosts[3].Position = new Vector2f(Map.Position.X + jailPosition.X, Map.Position.Y + jailPosition.Y);
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
                        SuperMode();
                        Program.Sprites.Remove(SuperPoints[i]);
                        SuperPoints.RemoveAt(i);

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
                        PacManR.Position = new Vector2f(1000, 1000);
                        PacManL.Position = new Vector2f(1000, 1000);
                        PacManDown.Position = new Vector2f(1000, 1000);
                        PacManUp.SetPosition(PacMan.Position);
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
                        PacManUp.Position = new Vector2f(1000, 1000);
                        PacManR.Position = new Vector2f(1000, 1000);
                        PacManDown.Position = new Vector2f(1000, 1000);
                        PacManL.SetPosition(PacMan.Position);
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
                        PacManUp.Position = new Vector2f(1000, 1000);
                        PacManR.Position = new Vector2f(1000, 1000);
                        PacManL.Position = new Vector2f(1000, 1000);
                        PacManDown.SetPosition(PacMan.Position);
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
                        PacManUp.Position = new Vector2f(1000, 1000);
                        PacManL.Position = new Vector2f(1000, 1000);
                        PacManDown.Position = new Vector2f(1000, 1000);
                        PacManR.SetPosition(PacMan.Position);
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
                PlaySound(eatm, false);
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
            GhostsEat = true;
            Score += 50;
            glob = 300;
        }
        public static void PlaySound(string filename, bool loop)
        {
            var sound = new Sound(new SoundBuffer(filename));
            if (loop == false)
            {
                sound.Loop = false;
            }
            if (loop == true)
            {
                sound.Loop = true;
            }
            int volume = 50;
            sound.Volume = volume;
            sound.Play();
            
        }
        public void CheckIfDead()
        {
            Vector2u jailPosition = new Vector2u(88, 80);
            if (Ghost1D == true)
            {
                Ghost1Timer -= 1;
                if (Ghost1Timer < 10)
                {
                    Ghost1Timer = 280;
                    Ghost1D = false;
                    Ghosts[0].Position = new Vector2f(Map.Position.X + jailPosition.X + 0, Map.Position.Y + jailPosition.Y - 20);
                }
            }
            if (Ghost2D == true)
            {
                Ghost2Timer -= 1;
                if (Ghost2Timer < 10)
                {
                    Ghost2Timer = 280;
                    Ghost2D = false;
                    Ghosts[1].Position = new Vector2f(Map.Position.X + jailPosition.X + 0, Map.Position.Y + jailPosition.Y - 20);
                }
            }
            if (Ghost3D == true)
            {
                Ghost3Timer -= 1;
                if (Ghost3Timer < 10)
                {
                    Ghost3Timer = 280;
                    Ghost3D = false;
                    Ghosts[2].Position = new Vector2f(Map.Position.X + jailPosition.X + 0, Map.Position.Y + jailPosition.Y - 20);
                }
            }
            if (Ghost4D == true)
            {
                Ghost4Timer -= 1;
                if (Ghost4Timer < 10)
                {
                    Ghost4Timer = 280;
                    Ghost4D = false;
                    Ghosts[3].Position = new Vector2f(Map.Position.X + jailPosition.X + 0, Map.Position.Y + jailPosition.Y - 20);
                }
            }
        }
    }
}
