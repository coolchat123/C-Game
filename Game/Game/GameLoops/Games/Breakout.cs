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

        static Button Restart;
        static Button Return;

        static SText ScoreInfoText;
        static SText ScoreText;
        static int Score;

        static SText LevelInfoText;
        static SText LevelText;
        static int Level;

        static SText LivesInfoText;
        static SText LivesText;
        static int Lives;

        static SSprite Paddle;
        static int PaddleSpeed;

        static SSprite Ball;
        static Vector2i BallSpeed;
        static bool BallMoving;
        static int BallMovingCountDown;
        static bool LevelOver;

        static SText GameOverText;
        static SText GameOverText2;
        static bool GameOver;

        static SSprite WallLeft;
        static SSprite WallRight;
        static SSprite WallTop;

        public Breakout() : base() { }

        public override void LoadContent()
        {
            Paddle = new SSprite(Color.White, 28, 6);
            PaddleSpeed = 0;

            Restart = new Button(new Texture("Content/Menu/MenuLeft.png"));
            Restart.Click += RestartClick;
            Restart.MouseEnter += RestartEnter;
            Restart.MouseLeave += RestartLeave;
            Return = new Button(new Texture("Content/Menu/MenuRight.png"));
            Return.Click += ReturnClick;
            Return.MouseEnter += ReturnEnter;
            Return.MouseLeave += ReturnLeave;

            Score = 0;
            ScoreInfoText = new SText("Score", 11);
            ScoreText = new SText("0", 11);

            Level = 1;
            LevelInfoText = new SText("Level", 11);
            LevelText = new SText("1", 11);

            Lives = 2;
            LivesInfoText = new SText("Lives", 11);
            LivesText = new SText("2", 11);

            Ball = new SSprite(Color.White, 3, 3);
            BallSpeed = new Vector2i(0, 0);
            BallMoving = false;
            BallMovingCountDown = 15;
            LevelOver = false;
            GameOver = false;

            Bricks = new List<SSprite>();

            GenerateLevel();

            GameOverText = new SText("Game Over!", 12);
            GameOverText2 = new SText("press SPACE to restart", 11);

            WallLeft = new SSprite(Color.White, 6, 198);
            WallRight = new SSprite(Color.White, 6, 198);
            WallTop = new SSprite(Color.White, 210, 6);
        }

        public override void Initialise()
        {
            WallLeft.SetPosition(2, 2);
            WallTop.SetPosition(WallLeft.Position.X + WallLeft.Texture.Size.X, WallLeft.Position.Y);
            WallRight.SetPosition(WallTop.Position.X + WallTop.Texture.Size.X, WallTop.Position.Y);

            Paddle.SetPosition(WallTop.Position.X + WallTop.Texture.Size.X / 2 - Paddle.Texture.Size.X / 2, 176);

            Restart.SetPosition(Program.Texture.Size.X - 2 - Restart.Texture.Size.X, Program.Texture.Size.Y - 2 - Restart.Texture.Size.Y);
            Return.SetPosition(Restart.Position.X, 2);

            Ball.SetPosition(Paddle.Position.X + Paddle.Texture.Size.X / 2 - Ball.Texture.Size.X / 2,
                Paddle.Position.Y - Ball.Texture.Size.Y);

            int scoreboardLeft = (int)WallRight.Position.X + (int)WallRight.Texture.Size.X + 2;
            int scoreboardWidth = (int)Program.Texture.Size.X - scoreboardLeft;
            LevelInfoText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - LevelInfoText.GetGlobalBounds().Width / 2, Program.Texture.Size.Y / 4 - 13);
            LevelText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - LevelText.GetGlobalBounds().Width / 2, LevelInfoText.Position.Y + LevelInfoText.GetGlobalBounds().Height + 3);

            ScoreInfoText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - ScoreInfoText.GetGlobalBounds().Width / 2, Program.Texture.Size.Y / 2 - 13);
            ScoreText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - ScoreText.GetGlobalBounds().Width / 2, ScoreInfoText.Position.Y + ScoreInfoText.GetGlobalBounds().Height + 3);

            LivesInfoText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - LivesInfoText.GetGlobalBounds().Width / 2, Program.Texture.Size.Y / 4 * 3 - 13);
            LivesText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - LivesText.GetGlobalBounds().Width / 2, LivesInfoText.Position.Y + LivesInfoText.GetGlobalBounds().Height + 3);

            GameOverText.SetPosition((WallRight.Position.X + WallRight.Texture.Size.X) / 2 - GameOverText.GetGlobalBounds().Width / 2, Program.Texture.Size.Y);
            GameOverText2.SetPosition((WallRight.Position.X + WallRight.Texture.Size.X) / 2 - GameOverText2.GetGlobalBounds().Width / 2, GameOverText.Position.Y + GameOverText.GetGlobalBounds().Height + 1);
        }

        public override void Update(GameTime gameTime)
        {
            if(BallMovingCountDown > 0)
            {
                BallMovingCountDown -= 1;
            }

            if (GameOver)
            {
                if(GameOverText.Position.Y > Program.Texture.Size.Y / 2 + 5)
                {
                    GameOverText.SetPosition(GameOverText.Position.X, GameOverText.Position.Y - (GameOverText.Position.Y - Program.Texture.Size.Y / 2) / 3);
                }
                else
                {
                    GameOverText.SetPosition(GameOverText.Position.X, Program.Texture.Size.Y / 2);
                }

                GameOverText2.SetPosition(GameOverText2.Position.X, GameOverText.Position.Y + GameOverText.GetGlobalBounds().Height);
            }
            else
            {
                if (GameOverText.Position.Y < Program.Texture.Size.Y - 5)
                {
                    GameOverText.SetPosition(GameOverText.Position.X, GameOverText.Position.Y - (GameOverText.Position.Y - Program.Texture.Size.Y) / 3);
                }
                else
                {
                    GameOverText.SetPosition(GameOverText.Position.X, Program.Texture.Size.Y);
                }

                GameOverText2.SetPosition(GameOverText2.Position.X, GameOverText.Position.Y + GameOverText.GetGlobalBounds().Height + 1);

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
                    if (LevelOver)
                    {
                        BallMoving = false;
                        GenerateLevel();
                        LevelOver = false;
                        SetLives(Lives + 1);
                        SetLevel(Level + 1);
                    }
                    else
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
                }

                // If ball goes below screen, reset ball and remove life.
                if (Ball.Position.Y > Program.Texture.Size.Y)
                {
                    BallMoving = false;
                    BallSpeed = new Vector2i(0, 0);

                    Ball.SetPosition(Paddle.Position.X + Paddle.Texture.Size.X / 2 - Ball.Texture.Size.X / 2,
                        Paddle.Position.Y - Ball.Texture.Size.Y);

                    if (Lives == 0)
                    {
                        GameOver = true;
                    }
                    else
                    {
                        SetLives(Lives - 1);
                    }
                }

                foreach (SSprite brick in Bricks)
                {
                    // If ball collides with a brick
                    if (Ball.GetGlobalBounds().Intersects(brick.GetGlobalBounds()))
                    {
                        // Get rid of brick
                        Program.Sprites.Remove(brick);
                        Bricks.Remove(brick);

                        if (Bricks.Count == 0)
                        {
                            LevelOver = true;
                        }

                        SetScore(Score + 1);

                        if ((BallSpeed.Y > 0 && Ball.Position.Y + Ball.Texture.Size.Y > brick.Position.Y && Ball.Position.Y + Ball.Texture.Size.Y - BallSpeed.Y <= brick.Position.Y) ||
                            (BallSpeed.Y < 0 && Ball.Position.Y < brick.Position.Y + brick.Texture.Size.Y && Ball.Position.Y - BallSpeed.Y >= brick.Position.Y + brick.Texture.Size.Y))
                        {
                            BallSpeed = new Vector2i(BallSpeed.X, -BallSpeed.Y);
                        }

                        if ((BallSpeed.X > 0 && Ball.Position.X + Ball.Texture.Size.X > brick.Position.X && Ball.Position.X + Ball.Texture.Size.X - BallSpeed.X <= brick.Position.X) ||
                            (BallSpeed.X < 0 && Ball.Position.X < brick.Position.X + brick.Texture.Size.X && Ball.Position.X - BallSpeed.X >= brick.Position.X + brick.Texture.Size.X))
                        {
                            BallSpeed = new Vector2i(-BallSpeed.X, BallSpeed.Y);
                        }

                        break;
                    }
                }
            }

            // Reset paddle speed to 0.
            // If the player is still pressing the button, then KeyInput will set it again next frame.
            PaddleSpeed = 0;
        }

        public override void KeyInput(Keyboard.Key key)
        {
            if (GameOver)
            {
                // If we're game over and the signal is space, then restart.
                if(key == Keyboard.Key.Space)
                {
                    Restart.PerformClick();
                }
            }
            // If we're not game over
            else
            {
                // If signal is left key, set speed to negative
                if (key == Keyboard.Key.A || key == Keyboard.Key.Left)
                {
                    PaddleSpeed = -4;
                }
                // If signal is right key, set speed to positive
                else if (key == Keyboard.Key.D || key == Keyboard.Key.Right)
                {
                    PaddleSpeed = 4;
                }
                // If signal is space, and ball is not yet moving, set ball to moving
                else if (key == Keyboard.Key.Space)
                {
                    if (!BallMoving && BallMovingCountDown == 0)
                    {
                        BallMoving = true;
                        BallSpeed = new Vector2i(1, -2);
                    }
                }
            }
        }

        public void SetScore(int newScore)
        {
            int scoreboardLeft = (int)WallRight.Position.X + (int)WallRight.Texture.Size.X + 2;
            int scoreboardWidth = (int)Program.Texture.Size.X - scoreboardLeft;
            Score = newScore;
            ScoreText.DisplayedString = newScore.ToString();
            ScoreText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - ScoreText.GetGlobalBounds().Width / 2, ScoreInfoText.Position.Y + ScoreInfoText.GetGlobalBounds().Height + 3);
        }

        public void SetLives(int newLives)
        {
            int scoreboardLeft = (int)WallRight.Position.X + (int)WallRight.Texture.Size.X + 2;
            int scoreboardWidth = (int)Program.Texture.Size.X - scoreboardLeft;
            Lives = newLives;
            LivesText.DisplayedString = newLives.ToString();
            LivesText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - LivesText.GetGlobalBounds().Width / 2, LivesInfoText.Position.Y + LivesInfoText.GetGlobalBounds().Height + 3);
        }

        public void SetLevel(int newLevel)
        {
            int scoreboardLeft = (int)WallRight.Position.X + (int)WallRight.Texture.Size.X + 2;
            int scoreboardWidth = (int)Program.Texture.Size.X - scoreboardLeft;
            Level = newLevel;
            LevelText.DisplayedString = newLevel.ToString();
            LevelText.SetPosition(scoreboardLeft + scoreboardWidth / 2 - LevelText.GetGlobalBounds().Width / 2, LevelInfoText.Position.Y + LevelInfoText.GetGlobalBounds().Height + 3);
        }

        public void GenerateLevel()
        {
            List<Color> brickColors = new List<Color> { new Color(151, 27, 147), new Color(81, 35, 205), new Color(0, 48, 255), new Color(0, 147, 147),
                new Color(0, 249, 0), new Color(203, 250, 0), new Color(255, 251, 0), new Color(255, 200, 0), new Color(255, 148, 0), new Color(255, 79, 0),
                new Color(255, 33, 0), new Color(217, 28, 82) };

            bool[,] brickMap = RandomLevel();

            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (brickMap[i, j])
                    {
                        SSprite brick = new SSprite(brickColors[j], 14, 6);
                        Bricks.Add(brick);

                        brick.SetPosition(10 + i * 16, 18 + j * 8);
                        brick.Color = brickColors[j];
                    }
                }
            }
        }

        // Restart button
        public void RestartClick(object sender, EventArgs e)
        {
            foreach (SSprite brick in Bricks)
            {
                Program.Sprites.Remove(brick);
            }
            Bricks.Clear();

            GenerateLevel();

            SetLives(2);
            SetScore(0);
            SetLevel(1);

            BallMoving = false;
            BallMovingCountDown = 15;

            Paddle.SetPosition(WallTop.Position.X + WallTop.Texture.Size.X / 2 - Paddle.Texture.Size.X / 2, 176);

            GameOver = false;
        }
        public void RestartEnter(object sender, EventArgs e)
        {
            Restart.SetScale(1.1f, SSprite.Pin.Middle);
        }
        public void RestartLeave(object sender, EventArgs e)
        {
            Restart.SetScale(1f, SSprite.Pin.Middle);
        }

        // Return button
        public void ReturnClick(object sender, EventArgs e)
        {
        }
        public void ReturnEnter(object sender, EventArgs e)
        {
            Return.SetScale(1.1f, SSprite.Pin.Middle);
        }
        public void ReturnLeave(object sender, EventArgs e)
        {
            Return.SetScale(1f, SSprite.Pin.Middle);
        }

        bool[,] RandomLevel()
        {
            Random random = new Random();
            string returnString = "";
            bool[,] result = new bool[13, 12];

            int rand = random.Next() % 23;

            switch (rand)
            {
                case 0:
                    returnString =
                        "             " +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "             " +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "             " +
                        "#############";
                    break;
                case 1:
                    returnString =
                        "             " +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "             " +
                        "             " +
                        "             ";
                    break;
                case 2:
                    returnString =
                        " # # # # # # " +
                        "# # # # # # #" +
                        " # # # # # # " +
                        "# # # # # # #" +
                        " # # # # # # " +
                        "# # # # # # #" +
                        " # # # # # # " +
                        "# # # # # # #" +
                        " # # # # # # " +
                        "# # # # # # #" +
                        " # # # # # # " +
                        "             ";
                    break;
                case 3:
                    returnString =
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############";
                    break;
                case 4:
                    returnString =
                        "             " +
                        "    #####    " +
                        "  #########  " +
                        " ########### " +
                        "####     ####" +
                        "###       ###" +
                        "###       ###" +
                        "###       ###" +
                        "###       ###" +
                        "             " +
                        "             " +
                        "             ";
                    break;
                case 5:
                    returnString =
                        "             " +
                        "   ##   ##   " +
                        "   ##   ##   " +
                        "   ##   ##   " +
                        "   ##   ##   " +
                        "             " +
                        "  #########  " +
                        "  #########  " +
                        "  #########  " +
                        "    ######   " +
                        "             " +
                        "             ";
                    break;
                case 6:
                    returnString =
                        "             " +
                        "  #########  " +
                        " ########### " +
                        "###  ###  ###" +
                        "###  ###  ###" +
                        "###  ###  ###" +
                        "#############" +
                        "  # # # # #  " +
                        "  ## # # ##  " +
                        "   #######   " +
                        "             " +
                        "             ";
                    break;
                case 7:
                    returnString =
                        "###       ###" +
                        "####     ####" +
                        "#####   #####" +
                        "###### ######" +
                        "### ##### ###" +
                        "###  ###  ###" +
                        "###   #   ###" +
                        "###       ###" +
                        "###       ###" +
                        "###       ###" +
                        "             " +
                        "             ";
                    break;
                case 8:
                    returnString =
                        "       #     " +
                        " ###     ##  " +
                        "#####    ##  " +
                        "#####        " +
                        "#####   ###  " +
                        " ###    ###  " +
                        "     #  ###  " +
                        "             " +
                        "   ###  #    " +
                        "   ###    #  " +
                        "   ###       " +
                        "             ";
                    break;
                case 9:
                    returnString =
                        "  #          " +
                        " #  #######  " +
                        "#  #       # " +
                        "# #  #####  #" +
                        "# # #     # #" +
                        "# # #  #  # #" +
                        "# # #   # # #" +
                        "# #  ###  # #" +
                        "#  #     #  #" +
                        " #  #####  # " +
                        "  #       #  " +
                        "   #######   ";
                    break;
                case 10:
                    returnString =
                        "             " +
                        "   #     #   " +
                        "    #   #    " +
                        "   #######   " +
                        "  ## ### ##  " +
                        " ########### " +
                        " # ####### # " +
                        " # #     # # " +
                        "    ## ##    " +
                        "             " +
                        "             " +
                        "             ";
                    break;
                case 11:
                    returnString =
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # " +
                        " # # # # # # ";
                    break;
                case 12:
                    returnString =
                        "  ###   ###  " +
                        " ##### ##### " +
                        "#############" +
                        "#############" +
                        "#############" +
                        "#############" +
                        " ########### " +
                        "  #########  " +
                        "   #######   " +
                        "    #####    " +
                        "     ###     " +
                        "      #      ";
                    break;
                case 13:
                    returnString =
                        "      #      " +
                        "     ###     " +
                        "    #####    " +
                        "   #######   " +
                        "  #########  " +
                        " ########### " +
                        "  #########  " +
                        "   #######   " +
                        "    #####    " +
                        "     ###     " +
                        "      #      " +
                        "             ";
                    break;
                case 14:
                    returnString =
                        "             " +
                        "    #####    " +
                        "  #########  " +
                        " ########### " +
                        "#############" +
                        "#############" +
                        "#############" +
                        " ########### " +
                        "  #########  " +
                        "    #####    " +
                        "             " +
                        "             ";
                    break;
                case 15:
                    returnString =
                        " ########### " +
                        "             " +
                        " ##   #  ##  " +
                        " # # # # # # " +
                        " # # ### ##  " +
                        " # # # # # # " +
                        " ##  # # ##  " +
                        "             " +
                        " ########### " +
                        "             " +
                        "             " +
                        "             ";
                    break;
                case 16:
                    returnString =
                        "## ## ### #  " +
                        "# # # #   #  " +
                        "# # # ##  #  " +
                        "#   # #   #  " +
                        "#   # ### ###" +
                        "             " +
                        "   #   ###   " +
                        "   #   #     " +
                        "   #   ##    " +
                        "   #   #     " +
                        "   ### ###   " +
                        "             " ;
                    break;
                case 17:
                    returnString =
                        "             " +
                        "    #####    " +
                        "  #########  " +
                        " ########### " +
                        "###       ###" +
                        "##         ##" +
                        "##         ##" +
                        " ##       ## " +
                        "  #       #  " +
                        "             " +
                        "             " +
                        "             ";
                    break;
                case 18:
                    returnString =
                        " ## #########" +
                        " ## ####   ##" +
                        " ##  ##  #  #" +
                        "  ##  # # # #" +
                        "#  ##    #  #" +
                        "##  ###    ##" +
                        "##    ###  ##" +
                        "#  #    ##  #" +
                        "# # # #  ##  " +
                        "#  #  ##  ## " +
                        "##   #### ## " +
                        "######### ## ";
                    break;
                case 19:
                    returnString =
                        "      #      " +
                        "     ###     " +
                        "    ## ##    " +
                        "   #######   " +
                        "  #########  " +
                        " ########### " +
                        "#############" +
                        "             " +
                        "             " +
                        "             " +
                        "             " +
                        "             ";
                    break;
                case 20:
                    returnString =
                        "             " +
                        "  #   #   #  " +
                        "  ##  #  ##  " +
                        "#  #######  #" +
                        " ####   #### " +
                        "## #  #  # ##" +
                        " ####   #### " +
                        "   #######   " +
                        "      #      " +
                        "     ###     " +
                        "     ###     " +
                        "             ";
                    break;
                case 21:
                    returnString =
                        "#############" +
                        "#############" +
                        "  #########  " +
                        "   #######   " +
                        "    #####    " +
                        "    #####    " +
                        "     ###     " +
                        "     ###     " +
                        "      #      " +
                        "      #      " +
                        "      #      " +
                        "      #      ";
                    break;
                case 22:
                    returnString =
                        "  #########  " +
                        "#  #######  #" +
                        "##  #####  ##" +
                        "###  ###  ###" +
                        "####  #  ####" +
                        "###  ###  ###" +
                        "##  #####  ##" +
                        "#  #######  #" +
                        "  #########  " +
                        "             " +
                        "             " +
                        "             ";
                    break;
                case 99:
                    returnString =
                        "             " +
                        "             " +
                        "             " +
                        "             " +
                        "             " +
                        "             " +
                        "             " +
                        "             " +
                        "             " +
                        "             " +
                        "             " +
                        "             ";
                    break;
            }

            int characterAmount = 0;

            for(int i = 0; i < result.GetLength(0); i++)
            {
                for(int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = returnString[i + j * 13] == '#';

                    characterAmount += 1;
                }
            }

            return result;
        }
    }
}
