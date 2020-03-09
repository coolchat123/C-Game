using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    public class Breakout : GameLoop
    {
        static SSprite[,] Bricks;

        static SSprite Paddle;
        static int PaddleSpeed;

        static SSprite Ball;
        static Vector2i BallSpeed;
        static bool BallMoving;

        static SSprite WallLeft;
        static SSprite WallRight;
        static SSprite WallTop;

        public Breakout() : base() { }

        public override void LoadContent()
        {
            Paddle = new SSprite(Color.White, 28, 6);
            PaddleSpeed = 0;

            Ball = new SSprite(Color.White, 3, 3);
            BallSpeed = new Vector2i(0, 0);
            BallMoving = false;

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

            Paddle.SetPosition(WallTop.Position.X + WallTop.Texture.Size.X / 2 - Paddle.Texture.Size.X / 2, 176);

            Ball.SetPosition(Paddle.Position.X + Paddle.Texture.Size.X / 2 - Ball.Texture.Size.X / 2,
                Paddle.Position.Y - Ball.Texture.Size.Y);
        }

        public override void Update(GameTime gameTime)
        {
            // Set the paddle to its new position, using PaddleSpeed variable.
            Paddle.SetPosition(Paddle.Position.X + PaddleSpeed, Paddle.Position.Y);

            // If paddle intersects with left wall, set paddle to furthest left possible.
            if (Paddle.GetGlobalBounds().Intersects(WallLeft.GetGlobalBounds()))
            {
                Paddle.SetPosition(WallLeft.Position.X + WallLeft.Texture.Size.X, Paddle.Position.Y);
            }
            // If paddle intersects with right wall, set paddle to furthest right possible.
            else if (Paddle.GetGlobalBounds().Intersects(WallRight.GetGlobalBounds()))
            {
                Paddle.SetPosition(WallRight.Position.X - Paddle.Texture.Size.X, Paddle.Position.Y);
            }

            // If ball isn't moving, set ball position to be on the paddle.
            if (!BallMoving)
            {
                BallSpeed = new Vector2i(0, 0);

                Ball.SetPosition(Paddle.Position.X + Paddle.Texture.Size.X / 2 - Ball.Texture.Size.X / 2,
                    Paddle.Position.Y - Ball.Texture.Size.Y);
            }
            // If ball isn't moving set ball to new position using BallSpeed vector.
            else
            {
                Ball.SetPosition(Ball.Position.X + BallSpeed.X, Ball.Position.Y + BallSpeed.Y);
            }

            // If ball intersects with left wall, set ball to furthest left possible and reverse X direction.
            if (Ball.GetGlobalBounds().Intersects(WallLeft.GetGlobalBounds()))
            {
                Ball.SetPosition(WallLeft.Position.X + WallLeft.Texture.Size.X, Ball.Position.Y);
                BallSpeed = new Vector2i(-BallSpeed.X, BallSpeed.Y);
            }
            // If ball intersects with right wall, set ball to furthest right possible and reverse X direction.
            else if (Ball.GetGlobalBounds().Intersects(WallRight.GetGlobalBounds()))
            {
                Ball.SetPosition(WallRight.Position.X - Ball.Texture.Size.X, Ball.Position.Y);
                BallSpeed = new Vector2i(-BallSpeed.X, BallSpeed.Y);
            }

            // If ball intersects with top wall, set ball to furthest up possible and reverse Y direction.
            if (Ball.GetGlobalBounds().Intersects(WallTop.GetGlobalBounds()))
            {
                Ball.SetPosition(Ball.Position.X, WallTop.Position.Y + WallTop.Texture.Size.Y);
                BallSpeed = new Vector2i(BallSpeed.X, -BallSpeed.Y);
            }

            // If ball intersects with paddle, set ball to furthest down possible and reverse Y direction.
            if (Ball.GetGlobalBounds().Intersects(Paddle.GetGlobalBounds()))
            {
                Ball.SetPosition(Ball.Position.X, Paddle.Position.Y - Ball.Texture.Size.Y);

                // Set ball X speed to 0-3 depending on distance to paddle middle.
                int distance = (int)(Ball.Position.X + Ball.Texture.Size.X / 2 - (Paddle.Position.X + Paddle.Texture.Size.X / 2));
                int newSpeed = 3 * distance / 14;
                BallSpeed = new Vector2i(newSpeed, -BallSpeed.Y);
            }

            // Reset paddle speed to 0.
            // If the player is still pressing the button, then KeyInput will set it again next frame.
            PaddleSpeed = 0;
        }

        public override void KeyInput(Keyboard.Key key)
        {
            // If signal is left key, set speed to negative
            if (key == Keyboard.Key.A || key == Keyboard.Key.Left)
            {
                PaddleSpeed = -3;
            }
            // If signal is right key, set speed to positive
            else if (key == Keyboard.Key.D || key == Keyboard.Key.Right)
            {
                PaddleSpeed = 3;
            }
            // If signal is space, and ball is not yet moving, set ball to moving
            else if (key == Keyboard.Key.Space)
            {
                if (!BallMoving)
                {
                    BallMoving = true;
                    BallSpeed = new Vector2i(0, -2);
                }
            } 
        }
    }
}
