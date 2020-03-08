using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

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

        static int RightPaddlespeed;

        static int LeftPaddlespeed;

        static bool BallMoving;

        static Vector2i Ballspeed;

        public Pong() : base() { }

        public override void LoadContent()
        {
            LeftPaddle = new SSprite(Color.White, 6, 30);
            RightPaddle = new SSprite(Color.White, 6, 30);
            Ball = new SSprite(Color.White, 7, 7);
            UpperLine = new SSprite(Color.White, 268, 6);
            BottomLine = new SSprite(Color.White, 268, 6);
            MiddleLine = new SSprite[17];

            for (int i = 0; i < 17; i++)
            {
                MiddleLine[i] = new SSprite(Color.White, 3, 3);
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
            RightPaddle.SetPosition(RightPaddle.Position.X, RightPaddle.Position.Y + RightPaddlespeed);

            RightPaddlespeed = 0;

            LeftPaddle.SetPosition(LeftPaddle.Position.X, LeftPaddle.Position.Y + LeftPaddlespeed);

            LeftPaddlespeed = 0;       
        }

        public override void KeyInput(Keyboard.Key key)
        {
            if (key == Keyboard.Key.K)
            {
                RightPaddlespeed = -3;
            }
            else if (key == Keyboard.Key.M)
            {
                RightPaddlespeed = 3;
            }

            if (key == Keyboard.Key.A)
            {
                LeftPaddlespeed = -3;
            }
            else if (key == Keyboard.Key.Z)
            {
                LeftPaddlespeed = 3;
            }
            if (key == Keyboard.Key.Space)
            {
                if (!BallMoving)
                {
                    BallMoving = true;
                   Ballspeed = new Vector2i(0, -2);
                }
            }

        }
    }
}
