﻿using System;
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
        int WantedDirection = 1;
        int Direction = 1;
        int Life = 2;
        int Score = 0;
        static SSprite Map;
        public static Image CollisionMap;
        static SSprite PacMan;
        static SSprite GhostPink;
        static SSprite GhostBlue;
        static SSprite GhostOrange;
        static SSprite GhostRed;
        static SText ScoreText;
        static Image PointMap;
        static int ScoreP;
        string music = "Content/Pacman/eatpcS.wav";
        public Pacman() : base() { }

        public override void LoadContent()
        {
            // 1 point = 20 points
            // eat ghost = 60 points
            PointMap = new Image("Content/Pacman/pointmap.png");
            ScoreText = new SText(ScoreP.ToString(), 11);
            Map = new SSprite(new Texture("Content/Pacman/Map.png"));
            PacMan = new SSprite(Color.Yellow, 16, 12);
            GhostRed = new SSprite(Color.Red, 16, 12);
            GhostBlue = new SSprite(Color.Blue, 16, 12);
            GhostPink = new SSprite(Color.Yellow, 16, 12);
            GhostOrange = new SSprite(Color.Green, 16, 12);
            CollisionMap = new Image("Content/Pacman/CollisionMap.png");
        }

        public override void Initialise()
        {
            ScoreText.Position = new Vector2f(Map.Position.X + Map.Texture.Size.X + 18, 10);
            ScoreText.Color = new Color(173,216,230);
            Map.Position = new Vector2f(10, Program.Texture.Size.Y / 2 - Map.Texture.Size.Y / 2);
            PacMan.Position = new Vector2f(90, Map.Position.Y + 100);
            GhostBlue.Position = new Vector2f(90, Map.Position.Y + 100);
            GhostOrange.Position = new Vector2f(200, Map.Position.Y + 100);
            GhostPink.Position = new Vector2f(90, Map.Position.Y + 80);
            GhostBlue.Position = new Vector2f(90, Map.Position.Y + 80);
            GhostRed.Position = new Vector2f(90, Map.Position.Y + 100);

        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(PacMan.Position);
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
                if (CheckCollision(PacMan.Position.X - Map.Position.X, PacMan.Position.Y - 2 - Map.Position.Y,
                    PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y))
                {
                    Direction = 0;
                }
            }

            if (Direction == 0)
            {
                if (CheckCollision(PacMan.Position.X - Map.Position.X, PacMan.Position.Y - 2 - Map.Position.Y,
                    PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y))
                {
                    CheckPoints(PacMan.Position.X - Map.Position.X, PacMan.Position.Y - 2 - Map.Position.Y,
                    PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y);
                    PacMan.SetPosition(PacMan.Position.X, PacMan.Position.Y - 2);
                }
            }

            if (WantedDirection == 1)
            {
                if (CheckCollision(PacMan.Position.X - 2 - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                   PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y))
                {
                    Direction = 1;
                }
            }
            if (Direction == 1)
            {
                if (CheckCollision(PacMan.Position.X - 2 - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                   PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y))
                {
                    PacMan.SetPosition(PacMan.Position.X - 2, PacMan.Position.Y);
                }
            }
            if (WantedDirection == 2)
            {
                if (CheckCollision(PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y,
                    PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y + 2 - Map.Position.Y))
                {
                    Direction = 2;
                }
            }
            if (Direction == 2)
            {
                if (CheckCollision(PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y,
                    PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y + 2 - Map.Position.Y))
                {
                    PacMan.SetPosition(PacMan.Position.X, PacMan.Position.Y + 2);
                }
            }

            if (WantedDirection == 3)
            {
                if (CheckCollision(PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                PacMan.Position.X + PacMan.Texture.Size.X + 2 - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y))
                {
                    Direction = 3;
                }
            }
            if (Direction == 3)
            {
                if (CheckCollision(PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                 PacMan.Position.X + PacMan.Texture.Size.X + 2 - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y))
                {
                    PacMan.SetPosition(PacMan.Position.X + 2, PacMan.Position.Y);
                }
            }

        }
        // 0 = W; 1 = A; 2 = S; 3 = D;
        public override void KeyInput(Keyboard.Key key)
        {
            if (key == Keyboard.Key.W)
            {
                WantedDirection = 0;
               // CheckPoints(PacMan.Position.X - Map.Position.X, PacMan.Position.Y - 2 - Map.Position.Y,
                 //   PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y);
            }
            if (key == Keyboard.Key.A)
            {
                WantedDirection = 1;
                CheckPoints(PacMan.Position.X - 2 - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                    PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y);
            }
            if (key == Keyboard.Key.S)
            {

                WantedDirection = 2;
                CheckPoints(PacMan.Position.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y,
                    PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y + 2 - Map.Position.Y);

            }
            if (key == Keyboard.Key.D)
            {
                WantedDirection = 3;

                CheckPoints(PacMan.Position.X + PacMan.Texture.Size.X - Map.Position.X, PacMan.Position.Y - Map.Position.Y,
                        PacMan.Position.X + PacMan.Texture.Size.X + 2 - Map.Position.X, PacMan.Position.Y + PacMan.Texture.Size.Y - Map.Position.Y);
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
        public int CheckPoints(float x, float y, float x2, float y2)
        {
            bool getp = false;
            for (float xCheck = x; xCheck < x2; xCheck++)
            {
                for (float yCheck = y; yCheck < y2; yCheck++)
                {
                    if (PointMap.GetPixel((uint)xCheck, (uint)yCheck) == Color.Red)
                    {
                        getp = true;
                        Score += 20;
                        Console.WriteLine(Score);
                    }
                }
            }
            return Score;
        }
    }
}
