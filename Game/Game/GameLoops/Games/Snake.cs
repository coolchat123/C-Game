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

        public Snake() : base() { }

        public override void LoadContent()
        {
            Grid = new bool[23,20]; 

            for(int i = 0; i < 23; i++){
                for(int j = 0; j < 20; j++){
                    Grid[i,j]= false;    
                }
            }
            
            Scoreboard = new SSprite(Color.Red,38,Program.Texture.Size.Y);

            Score = 0;

            ScoreNumber = new SText(Score.ToString(), 11);

            ScoreText = new SText("score",11);

        }

        public override void Initialise()
        {
            Scoreboard.SetPosition(Program.Texture.Size.X - 38, 0);
            ScoreNumber.SetPosition(Scoreboard.Position.X + Scoreboard.Texture.Size.X/2 - ScoreNumber.GetGlobalBounds().Width/2,
                Scoreboard.Texture.Size.Y/2 - ScoreNumber.GetGlobalBounds().Height/2);

            ScoreText.SetPosition(Scoreboard.Position.X + Scoreboard.Texture.Size.X / 2 - ScoreText.GetGlobalBounds().Width / 2,
                ScoreNumber.Position.Y - ScoreText.GetGlobalBounds().Height - 5);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void KeyInput(Keyboard.Key key)
        {
            SetScore();
        }

        public void SetScore() 
        {
            Score += 1;

            ScoreNumber.DisplayedString = Score.ToString();

            ScoreNumber.SetPosition(Scoreboard.Position.X + Scoreboard.Texture.Size.X / 2 - ScoreNumber.GetGlobalBounds().Width / 2,
                Scoreboard.Texture.Size.Y / 2 - ScoreNumber.GetGlobalBounds().Height / 2);
        }
    }
}
