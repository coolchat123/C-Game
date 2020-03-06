using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;

namespace Game
{
    public class Snake : GameLoop
    {
        static bool[,] Grid;

        static SSprite Scoreboard;

        static int Score;

        static SText ScoreNumber;

        static SText ScoreText;

        static SSprite SnakeHead;

        static string Direction;

        static int Timer;

        static string NewDirection;

        static Button Return;

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

            SnakeHead = new SSprite(new Texture("content/snake/snakebox.png"));
            SnakeHead.Position = new SFML.System.Vector2f(210, 20);

            Direction = "left";

            Timer = 12;

            NewDirection = "left";

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
            if (Timer == 0) 
            {
                SnakeMove();
                Timer = 12;           
            } 
            else 
            {
                Timer -= 1;
            }
        }

        public override void KeyInput(Keyboard.Key key)
        {
            SetScore();

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

        public void SetScore() 
        {
            Score += 1;

            ScoreNumber.DisplayedString = Score.ToString();

            ScoreNumber.SetPosition(Scoreboard.Position.X + Scoreboard.Texture.Size.X / 2 - ScoreNumber.GetGlobalBounds().Width / 2,
                Scoreboard.Texture.Size.Y / 2 - ScoreNumber.GetGlobalBounds().Height / 2);
        }
        public void SnakeMove()
        {
            if (NewDirection == "left")
            {
                SnakeHead.SetPosition(SnakeHead.Position.X-10, SnakeHead.Position.Y);
                Console.WriteLine(SnakeHead.Position);
            }

            if (NewDirection == "down")
            {
                SnakeHead.SetPosition(SnakeHead.Position.X, SnakeHead.Position.Y + 10);
                Console.WriteLine(SnakeHead.Position);
            }

            if (NewDirection == "right")
            {
                SnakeHead.SetPosition(SnakeHead.Position.X + 10, SnakeHead.Position.Y);
                Console.WriteLine(SnakeHead.Position);
            }

            if (NewDirection == "up")
            {
                SnakeHead.SetPosition(SnakeHead.Position.X, SnakeHead.Position.Y - 10);
                Console.WriteLine(SnakeHead.Position);
            }
            Direction = NewDirection;
        }

        public void ReturnClick(object sender, EventArgs e)
        {
            Program.ChangeGame = Program.GameName.Menu;
        }
        public void ReturnEnter(object sender, EventArgs e)
        {
            Return.SetScale(1.1f, SSprite.Pin.Middle);
        }
        public void ReturnLeave(object sender, EventArgs e)
        {
            Return.SetScale(1f, SSprite.Pin.Middle);
        }
    }
}
