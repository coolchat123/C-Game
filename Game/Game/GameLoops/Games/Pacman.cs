using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;

namespace Game
{
    public class Pacman : GameLoop
    {

        int Life = 2;
        int Score = 0;
        static SSprite Map;
        static Image CollisionMap;
        static SSprite PacMan;
        string music = "Content/Pacman/eatpcS.wav";
        public Pacman() : base() { }

        public override void LoadContent()
        {
            Map = new SSprite(new Texture("Content/Pacman/Map.png"));
            PacMan = new SSprite(Color.Yellow, 16, 12);
            Program.PlaySound(music);
        }

        public override void Initialise()
        {
            Map.Position = new Vector2f(10, Program.Texture.Size.Y / 2 - Map.Texture.Size.Y / 2);
            PacMan.Position = new Vector2f(100, Map.Position.Y + 100);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void KeyInput(Keyboard.Key key)
        {
            if (key == Keyboard.Key.W)
            {
                PacMan.SetPosition(PacMan.Position.X ,PacMan.Position.Y - 4);
            }
            if (key == Keyboard.Key.A)
            {
                PacMan.SetPosition(PacMan.Position.X - 4, PacMan.Position.Y);
            }
            if (key == Keyboard.Key.S)
            {
                PacMan.SetPosition(PacMan.Position.X, PacMan.Position.Y + 4);
            }
            if (key == Keyboard.Key.D)
            {
                PacMan.SetPosition(PacMan.Position.X + 4, PacMan.Position.Y);
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
                        Console.WriteLine("worksss");
                    }
                }
            }
            return canMove;
        }
    }
}
