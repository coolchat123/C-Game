using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;

namespace Game
{
    public class Pong : GameLoop
    {
        static SSprite LeftPaddle;

        static SSprite RightPaddle;

        static SSprite Ball;

        static SSprite UpperLine;

        static SSprite BottomLine;

        static SSprite[] MiddleLine;

        public Pong() : base() { }

        public override void LoadContent()
        {
            LeftPaddle = new SSprite(Color.White, 6, 30);
            RightPaddle = new SSprite(Color.White, 6, 30);
            Ball = new SSprite(Color.White, 6, 6);
            UpperLine = new SSprite(Color.White, 268, 6);
            BottomLine = new SSprite(Color.White, 268, 6);
            MiddleLine = new SSprite[17];

            for (int i = 0; i < 17; i++)
            {
                MiddleLine[i] = new SSprite(Color.White, 6, 6);
            }
        }

        public override void Initialise()
        {
            LeftPaddle.SetPosition(6, Program.Texture.Size.Y / 2 - LeftPaddle.Texture.Size.Y / 2);
            RightPaddle.SetPosition(Program.Texture.Size.X - RightPaddle.Texture.Size.X - 6, Program.Texture.Size.Y / 2 - RightPaddle.Texture.Size.Y / 2);
            Ball.SetPosition(Program.Texture.Size.X / 2, Program.Texture.Size.Y / 2);
            BottomLine.SetPosition(0, Program.Texture.Size.Y - 6);

            for (int i = 0; i < 17; i++)
            {
                MiddleLine[i].SetPosition(Program.Texture.Size.X / 2 - MiddleLine[i].Texture.Size.X / 2, i * 12);
            }
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void KeyInput(Keyboard.Key key)
        {
        }
    }
}
