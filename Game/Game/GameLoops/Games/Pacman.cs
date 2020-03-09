using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    public class Pacman : GameLoop
    {

        int Life = 2;
        int Score = 0;
        static SSprite Map;
        static Image CollisionMap;
        public Pacman() : base() { }

        public override void LoadContent()
        {
            Map = new SSprite(new Texture("Content/Pacman/Map.png"));
            Map.Position = new Vector2f(10, Program.Window.Size.Y / 2 - Map.Texture.Size.Y * Program.Scale / 2);
        }

        public override void Initialise()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void KeyInput(Keyboard.Key key)
        {
            if (key == Keyboard.Key.W)
            {
                Console.WriteLine("test");
                
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
