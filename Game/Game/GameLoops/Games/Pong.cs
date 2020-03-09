﻿using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    public class Pong : GameLoop
    {
        static int scoreLeft = 0;
        static int scoreRight = 0;
        static SText scoreLText = new SText(scoreLeft.ToString(),11);
        static SText scoreRText = new SText(scoreRight.ToString(), 11);

        
        //int score = 100000;

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
            int score = 100000;
            BallMoving = false;
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
            //scoreLText.DisplayedString = score.ToString();
            scoreLText.Position = new Vector2f(67,50);
            scoreLText.Color = Color.White;

            scoreRText.Position = new Vector2f(197, 50);
            scoreRText.Color = Color.White;
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

            Ball.SetPosition(Ball.Position.X + Ballspeed.X, Ball.Position.Y + Ballspeed.Y);

            if (Ball.GetGlobalBounds().Intersects(UpperLine.GetGlobalBounds()))
            {
                Ball.SetPosition(Ball.Position.X, UpperLine.Position.Y + UpperLine.Texture.Size.Y);
                Ballspeed = new Vector2i(Ballspeed.X, -Ballspeed.Y);
            }
            if (Ball.GetGlobalBounds().Intersects(BottomLine.GetGlobalBounds()))
            {
                Ball.SetPosition(Ball.Position.X, BottomLine.Position.Y - Ball.Texture.Size.Y);
                Ballspeed = new Vector2i(Ballspeed.X, -Ballspeed.Y);
            }
            if (LeftPaddle.GetGlobalBounds().Intersects(UpperLine.GetGlobalBounds()))
            {
                LeftPaddle.SetPosition(LeftPaddle.Position.X, UpperLine.Position.Y + UpperLine.Texture.Size.Y);
            }
            if (LeftPaddle.GetGlobalBounds().Intersects(BottomLine.GetGlobalBounds()))
            {
                LeftPaddle.SetPosition(LeftPaddle.Position.X, BottomLine.Position.Y - LeftPaddle.Texture.Size.Y);
            }

            if (RightPaddle.GetGlobalBounds().Intersects(UpperLine.GetGlobalBounds()))
            {
                RightPaddle.SetPosition(RightPaddle.Position.X, UpperLine.Position.Y + UpperLine.Texture.Size.Y);
            }
            if (RightPaddle.GetGlobalBounds().Intersects(BottomLine.GetGlobalBounds()))
            {
                RightPaddle.SetPosition(RightPaddle.Position.X, BottomLine.Position.Y - RightPaddle.Texture.Size.Y);
            }

            if (Ball.GetGlobalBounds().Intersects(RightPaddle.GetGlobalBounds()))
            {
                Ball.SetPosition(RightPaddle.Position.X - Ball.Texture.Size.X, Ball.Position.Y);

                int distance = (int)(Ball.Position.Y + Ball.Texture.Size.Y / 2 - (RightPaddle.Position.Y + RightPaddle.Texture.Size.Y / 2));
                int newSpeed = 3 * distance / 14;
                Ballspeed = new Vector2i(-Ballspeed.X, newSpeed);
            }
            if (Ball.GetGlobalBounds().Intersects(LeftPaddle.GetGlobalBounds()))
            {
                Ball.SetPosition(LeftPaddle.Position.X + LeftPaddle.Texture.Size.X, Ball.Position.Y);

                int distance = (int)(Ball.Position.Y + Ball.Texture.Size.Y / 2 - (LeftPaddle.Position.Y + LeftPaddle.Texture.Size.Y / 2));
                int newSpeed = 3 * distance / 14;
                Ballspeed = new Vector2i(-Ballspeed.X, newSpeed);
            }
            if (Ball.Position.X > RightPaddle.Position.X) {
                scoreLeft += 1;
                scoreLText.DisplayedString = scoreLeft.ToString();
                Ball.SetPosition(134, 100);
            }
            
            if( Ball.Position.X < LeftPaddle.Position.X) {
                scoreRight += 1;
                scoreRText.DisplayedString = scoreRight.ToString();
                Ball.SetPosition(134, 100);
            }



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
                   Ballspeed = new Vector2i(2, -2);
                }
            }

        }
    }
}
