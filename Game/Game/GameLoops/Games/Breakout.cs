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
        static List<SSprite> Bricks;

        static int SpeedModifier;
        static int BricksBroken;

        static SText ScoreInfoText;
        static SText ScoreText;
        static int Score;

        static SText LivesInfoText;
        static SText LivesText;
        static int Lives;

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

            SpeedModifier = 0;
            BricksBroken = 0;

            Score = 0;
            ScoreInfoText = new SText("Score", 11);
            ScoreText = new SText("0", 11);

            Lives = 2;
            LivesInfoText = new SText("Lives", 11);
            LivesText = new SText("0", 11);

            Ball = new SSprite(Color.White, 3, 3);
            BallSpeed = new Vector2i(0, 0);
            BallMoving = false;

            Bricks = new List<SSprite>();

            for(int i = 0; i < 13; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Bricks.Add(new SSprite(Color.White, 14, 6));
                }
            }

            WallLeft = new SSprite(Color.White, 6, 198);
            WallRight = new SSprite(Color.White, 6, 198);
            WallTop = new SSprite(Color.White, 210, 6);
        }

        public override void Initialise()
        {
            int brickAmount = 0;
            foreach (SSprite brick in Bricks)
            {
                int i = brickAmount / 13;
                int j = brickAmount - i * 13;
                brick.SetPosition(10 + j * 16, 18 + i * 8);
                brickAmount += 1;
            }

            WallLeft.SetPosition(2, 2);
            WallTop.SetPosition(WallLeft.Position.X + WallLeft.Texture.Size.X, WallLeft.Position.Y);
            WallRight.SetPosition(WallTop.Position.X + WallTop.Texture.Size.X, WallTop.Position.Y);

            Paddle.SetPosition(WallTop.Position.X + WallTop.Texture.Size.X / 2 - Paddle.Texture.Size.X / 2, 176);

            Ball.SetPosition(Paddle.Position.X + Paddle.Texture.Size.X / 2 - Ball.Texture.Size.X / 2,
                Paddle.Position.Y - Ball.Texture.Size.Y);

            int scoreboardLeft = (int)WallRight.Position.X + (int)WallRight.Texture.Size.X + 2;
            int scoreboardWidth = (int)Program.Texture.Size.X - scoreboardLeft;
            ScoreInfoText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - ScoreInfoText.GetGlobalBounds().Width / 2, Program.Texture.Size.Y / 2);
            ScoreText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - ScoreText.GetGlobalBounds().Width / 2, ScoreInfoText.Position.Y + ScoreInfoText.GetGlobalBounds().Height + 3);

            LivesInfoText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - LivesInfoText.GetGlobalBounds().Width / 2, Program.Texture.Size.Y / 4 * 3);
            LivesText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - LivesText.GetGlobalBounds().Width / 2, LivesInfoText.Position.Y + LivesInfoText.GetGlobalBounds().Height + 3);
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
                int newSpeed;
                if (distance < -9 || distance > 9)
                {
                    newSpeed = distance / Math.Abs(distance) * 2;
                }
                else if (distance < -4 || distance > 4)
                {
                    newSpeed = distance / Math.Abs(distance) * 1;
                }
                else
                {
                    newSpeed = BallSpeed.X;
                }
                BallSpeed = new Vector2i(newSpeed, -BallSpeed.Y);
            }

            // If ball goes below screen, reset ball and remove life.
            if (Ball.Position.Y > Program.Texture.Size.Y)
            {
                BallMoving = false;
                SetLives(Lives - 1);
            }

            foreach (SSprite brick in Bricks)
            {
                // If ball collides with a brick
                if (Ball.GetGlobalBounds().Intersects(brick.GetGlobalBounds()))
                {
                    // Get rid of brick
                    Program.Sprites.Remove(brick);
                    Bricks.Remove(brick);

                    if(++BricksBroken > 20)
                    {
                        BricksBroken = 0;
                        SpeedModifier += 1;
                    }

                    SetScore(Score + 1);

                    Console.WriteLine(BricksBroken + ", " + SpeedModifier);

                    if((Ball.Position.X + Ball.Texture.Size.X - brick.Position.X > 0 && Ball.Position.X + Ball.Texture.Size.X - brick.Position.X < BallSpeed.X)
                        || (Ball.Position.X - (brick.Position.X + brick.Texture.Size.X) < 0 && Ball.Position.X - (brick.Position.X + brick.Texture.Size.X) > BallSpeed.X))
                    {
                        BallSpeed = new Vector2i(-BallSpeed.X, BallSpeed.Y);
                    }

                    if ((Ball.Position.Y + Ball.Texture.Size.Y - brick.Position.Y > 0 && Ball.Position.Y + Ball.Texture.Size.Y - brick.Position.Y < BallSpeed.Y)
                        || (Ball.Position.Y - (brick.Position.Y + brick.Texture.Size.Y) < 0 && Ball.Position.Y - (brick.Position.Y + brick.Texture.Size.Y) > BallSpeed.Y))
                    {
                        BallSpeed = new Vector2i(BallSpeed.X, -BallSpeed.Y);
                    }

                    break;
                }
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
                    BallSpeed = new Vector2i(1, -2);
                }
            } 
        }

        public void SetScore(int newScore)
        {
            Score = newScore;
            ScoreText.DisplayedString = newScore.ToString();
        }

        public void SetLives(int newLives)
        {
            Lives = newLives;
            LivesText.DisplayedString = newLives.ToString();
        }
    }
}
