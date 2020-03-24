using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace Game
{
    public class Snake : GameLoop
    {
        static bool[,] Grid;

        static SSprite Scoreboard;

        //DB score
        static int Score;

        static SText ScoreNumber;

        static SText ScoreText;

        static string Direction;

        static int Timer;

        static string NewDirection;

        static Button Return;

        static SSprite Apple;

        static SText GameOverText;

        static bool bGame;

        static SFML.System.Vector2f LastBodyPos;

        static List<SSprite> SnakeList;

        static SText ScoreHeadsUp;

        static Sound BackgroundMusic;

        public Snake() : base() { }

        public override void LoadContent()
        {
            Grid = new bool[23,20];

            for(int i = 0; i < 23; i++){
                for(int j = 0; j < 20; j++){
                    Grid[i,j]= false;    
                }
            }
            
            Scoreboard = new SSprite(Color.Green,38,Program.Texture.Size.Y);

            Score = 0;

            ScoreNumber = new SText(Score.ToString(), 11);

            ScoreText = new SText("score",11);
            
            Return = new Button(new Texture("Content/Menu/MenuLeft.png"));
            Return.Click += ReturnClick;
            Return.MouseEnter += ReturnEnter;
            Return.MouseLeave += ReturnLeave;

            Apple = new SSprite(new Texture("content/snake/apple.png"));
            Apple.Position = new SFML.System.Vector2f(50,80);

            Direction = "left";

            Timer = 8;

            NewDirection = "left";

            bGame = true;

            SnakeList = new List<SSprite>();

            SSprite SnakeBody = new SSprite(new Texture("content/snake/snakeHead.png"));
            SnakeBody.Position = new SFML.System.Vector2f(110, 90);
            SnakeList.Add(SnakeBody);

            BackgroundMusic = new Sound(new SoundBuffer("content/snake/theme-music.wav"));
            BackgroundMusic.Loop = true;
            BackgroundMusic.Volume = Program.sound;
            BackgroundMusic.Play();


        }

        public override void Initialise()
        {
            Scoreboard.SetPosition(Program.Texture.Size.X - 38, 0);
            ScoreNumber.SetPosition(Scoreboard.Position.X + Scoreboard.Texture.Size.X/2 - ScoreNumber.GetGlobalBounds().Width/2,
                Scoreboard.Texture.Size.Y/2 - ScoreNumber.GetGlobalBounds().Height/2);

            ScoreText.SetPosition(Scoreboard.Position.X + Scoreboard.Texture.Size.X / 2 - ScoreText.GetGlobalBounds().Width / 2,
                ScoreNumber.Position.Y - ScoreText.GetGlobalBounds().Height - 5);

            Return.SetPosition(Program.Texture.Size.X - Return.Texture.Size.X, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if(bGame)
                GameOver();

            if (!bGame)
                return;

            if (Timer == 0) 
            {
                SnakeMove();
                Timer = 12;           
            } 
            else 
            {
                Timer -= 1;
            }

            if (SnakeList[0].Position == Apple.Position)
            {
                void playSound(string filePath)
                {
                    var sound = new Sound(new SoundBuffer(filePath));
                    sound.Loop = false;
                    sound.Volume = Program.sound;
                    sound.Play();
                }

                scoreFunction:
                float[] x_positions = { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220 };
                float[] y_positions = { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190 };
                float x = SnakeList[0].Position.X;
                float y = SnakeList[0].Position.Y;
                Random rnd = new Random();
               
                x = x_positions[rnd.Next(0, 22)];
                y = y_positions[rnd.Next(0, 19)];

                for(int i = 0; i < SnakeList.Count; i++)
                {
                    if(SnakeList[i].Position.X == x && SnakeList[i].Position.Y == y)
                    {
                        goto scoreFunction;
                    }
                }

                Apple.Position = new SFML.System.Vector2f(x, y);
                SetScore();
                SSprite SnakeBody = new SSprite(new Texture("content/snake/snakeHead.png"));
                SnakeBody.Position = new SFML.System.Vector2f(LastBodyPos.X, LastBodyPos.Y);
                SnakeList.Add(SnakeBody);
                playSound("content/snake/nom-ping-1.wav");
            }

            LastBodyPos = new SFML.System.Vector2f(SnakeList[SnakeList.Count - 1].Position.X, SnakeList[SnakeList.Count - 1].Position.Y);
        }

        public override void KeyInput(Keyboard.Key key)
        {
            if (bGame)
            {
                if ((key == Keyboard.Key.A || key == Keyboard.Key.Left) && Direction != "right")
                {
                    NewDirection = "left";
                }

                if ((key == Keyboard.Key.S || key == Keyboard.Key.Down) && Direction != "up")
                {
                    NewDirection = "down";
                }

                if ((key == Keyboard.Key.D || key == Keyboard.Key.Right) && Direction != "left")
                {
                    NewDirection = "right";
                }

                if ((key == Keyboard.Key.W || key == Keyboard.Key.Up) && Direction != "down")
                {
                    NewDirection = "up";
                }
            }                
            else
            {
                if(key == Keyboard.Key.Space)
                {
                    SSprite SnakeBody = new SSprite(new Texture("content/snake/snakeHead.png"));
                    SnakeBody.Position = new SFML.System.Vector2f(110, 90);
                    SnakeList.Add(SnakeBody);
                    bGame = true;
                    GameOverText.DisplayedString = "";
                    ScoreHeadsUp.DisplayedString = "";
                    BackgroundMusic.Play();
                }
            }            
        }

        public void SetScore() 
        {
            Score += 1;

            ScoreNumber.DisplayedString = Score.ToString();

            ScoreNumber.SetPosition(Scoreboard.Position.X + Scoreboard.Texture.Size.X / 2 - ScoreNumber.GetGlobalBounds().Width / 2,
                Scoreboard.Texture.Size.Y / 2 - ScoreNumber.GetGlobalBounds().Height / 2);
        }

        public void SnakeMove()
        {
            if (!bGame)
                return;

            for(int i = SnakeList.Count - 1; i > 0; i--)
            {
                SnakeList[i].Position = SnakeList[i - 1].Position;
            }

            if (NewDirection == "left")
            {
                SnakeList[0].SetPosition(SnakeList[0].Position.X-10, SnakeList[0].Position.Y);
                Console.WriteLine(SnakeList[0].Position);
            }

            if (NewDirection == "down")
            {
                SnakeList[0].SetPosition(SnakeList[0].Position.X, SnakeList[0].Position.Y + 10);
                Console.WriteLine(SnakeList[0].Position);
            }

            if (NewDirection == "right")
            {
                SnakeList[0].SetPosition(SnakeList[0].Position.X + 10, SnakeList[0].Position.Y);
                Console.WriteLine(SnakeList[0].Position);
            }

            if (NewDirection == "up")
            {
                SnakeList[0].SetPosition(SnakeList[0].Position.X, SnakeList[0].Position.Y - 10);
                Console.WriteLine(SnakeList[0].Position);
            }
            Direction = NewDirection;
        }

        public void ReturnClick(object sender, EventArgs e)
        {
            Program.ChangeGame = Program.GameName.Menu;
            BackgroundMusic.Stop();
        }

        public void ReturnEnter(object sender, EventArgs e)
        {
            Return.SetScale(1.1f, SSprite.Pin.Middle);
        }

        public void ReturnLeave(object sender, EventArgs e)
        {
            Return.SetScale(1f, SSprite.Pin.Middle);
        }

        public void ResetScore()
        {
            Score = 0;
            ScoreNumber.DisplayedString = "0";
        }

        public void GameOver()
        {
            void playSound(string filePath){
                var sound = new Sound(new SoundBuffer(filePath));
                sound.Loop = false;
                sound.Volume = Program.sound;
                sound.Play();
            }

            if(SnakeList[0].Position.X <= -1 || SnakeList[0].Position.Y <= -1 || SnakeList[0].Position.X >= 221 || SnakeList[0].Position.Y >= 191)
            {
                GameOverText = new SText("Game over!", 11);
                bGame = false;

                for (int j = SnakeList.Count - 1; j >= 0; j--)
                {
                    Program.Sprites.Remove(SnakeList[j]);
                }

                SnakeList.Clear();

                BackgroundMusic.Stop();

                playSound("content/snake/dead.wav");

                GameOverText.SetPosition(Program.Texture.Size.X / 2, Program.Texture.Size.Y / 2);
                Color textColor = new Color(255, 255, 255);
                GameOverText.Color = textColor;
                ScoreHeadsUp = new SText("Your score: " + Score.ToString(), 12);
                ScoreHeadsUp.Position = new SFML.System.Vector2f((Program.Texture.Size.X / 2), (Program.Texture.Size.Y / 2) + 15);
                ResetScore();

            } 
            
            else
            {
                for (int i = 1; i < SnakeList.Count; i++)
                {
                    if (SnakeList[i].Position.X == SnakeList[0].Position.X && SnakeList[i].Position.Y == SnakeList[0].Position.Y)
                    {
                        GameOverText = new SText("Game over!", 11);
                        bGame = false;

                        for(int j = SnakeList.Count - 1; j >= 0; j--)
                        {
                            Program.Sprites.Remove(SnakeList[j]);
                        }

                        SnakeList.Clear();

                        BackgroundMusic.Stop();

                        playSound("content/snake/dead.wav");

                        GameOverText.SetPosition(Program.Texture.Size.X / 2, Program.Texture.Size.Y / 2);
                        Color textColor = new Color(255, 255, 255);
                        GameOverText.Color = textColor;
                        ScoreHeadsUp = new SText("Your score: " + Score.ToString(), 12);
                        ScoreHeadsUp.Position = new SFML.System.Vector2f((Program.Texture.Size.X / 2), (Program.Texture.Size.Y / 2) + 15);
                        ResetScore();
                    }
                }
            }
        }
    }
}