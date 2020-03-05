using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;

namespace Game
{
    public class Breakout : GameLoop
    {
        public SSprite[,] Bricks;

        public SSprite WallLeft;
        public SSprite WallRight;
        public SSprite WallTop;

        public Breakout() : base() { }

        public override void LoadContent()
        {
            Bricks = new SSprite[13, 8];

            for(int i = 0; i < 13; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Bricks[i, j] = new SSprite(Color.White, 14, 6);
                }
            }

            WallLeft = new SSprite(Color.White, 6, 198);
            WallRight = new SSprite(Color.White, 6, 198);
            WallTop = new SSprite(Color.White, 210, 6);
        }

        public override void Initialise()
        {
            for(int i = 0; i < 13; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Bricks[i, j].SetPosition(10 + i * 16, 18 + j * 8);
                }
            }

            WallLeft.SetPosition(2, 2);
            WallTop.SetPosition(WallLeft.Position.X + WallLeft.Texture.Size.X, WallLeft.Position.Y);
            WallRight.SetPosition(WallTop.Position.X + WallTop.Texture.Size.X, WallTop.Position.Y);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void KeyInput(Keyboard.Key key)
        {
        }
    }
}
