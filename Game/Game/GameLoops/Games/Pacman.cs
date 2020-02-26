﻿using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    public class Pacman : GameLoop
    {
        public Pacman() : base() { }
        int Life = 2;
        int Score = 0;
        static SSprite Map;
        static SSprite Bscreen;
        static PacmCharacter Character;
        static Image CollisionMap;
        static Image PointMap;
        static Sound PacMan;
        string beginpcS = "Content/Pacman/beginpcS.wav";
        string eatpcS = "Content/Pacman/eatpcS.wav";
        string diepcS = "Content/Pacman/diepcS.wav";
        string superpcS = "Content/Pacman/superpcS.wav";

        public override void LoadContent()
        {
            Map = new SSprite(new Texture("Content/Pacman/Map.png"));
            //Bscreen = new SSprite(new Texture("Content/Pacman/BGScreenpm.png"));
            Character = new PacmCharacter(new Texture("Content/Pacman/pactest.png"));
            CollisionMap = new Image("Content/Pacman/CollisionMap.png");
            PointMap = new Image("Content/Pacman/pointmap.png");
            PointMap = new Image("Content/Pacman/Superpmapp.png");

        }
        public static void PlaySound(string filename)
        {
            var sound = new Sound(new SoundBuffer(filename));
            sound.Loop = true;
            sound.Volume = 50;
            sound.Play();
        }
        public void BeginScreen()
        {
            bool bgscreen = false;
            if (bgscreen = false)
            {
                PlaySound(beginpcS);
            }

            
        }

        public override void Initialise()
        {
            PlaySound(beginpcS);
            //Window.KeyPressed += Window_Keypressed;
            Map.Position = new Vector2f(65, Program.Window.Size.Y / 2 - Map.Texture.Size.Y * Program.Scale / 2);
            Character.Position = new Vector2f(300, 332);
        }

        public override void Update(GameTime gameTime)
        {
            if (Character.GetDirection() == 1)
            {
                Character.Position = new Vector2f(Character.Position.X - 4, Character.Position.Y);
                Console.WriteLine(Character.Position - Map.Position);
                CheckCollision(Character.Position.X - 4, Character.Position.Y, Character.Position.X, Character.Position.Y + Character.Texture.Size.Y);
            }
            if (Character.GetDirection() == 2)
            {
                Character.Position = new Vector2f(Character.Position.X + 4, Character.Position.Y);
                Console.WriteLine(Character.Position);
                CheckCollision(Character.Position.X + 4, Character.Position.Y, Character.Position.X, Character.Position.Y + Character.Texture.Size.Y);
            }
            if (Character.GetDirection() == 3)
            {
                Character.Position = new Vector2f(Character.Position.X, Character.Position.Y - 4);
                Console.WriteLine(Map.Position);
                CheckCollision(Character.Position.X, Character.Position.Y - 4, Character.Position.X, Character.Position.Y + Character.Texture.Size.Y);
            }
            if (Character.GetDirection() == 4)
            {
                Character.Position = new Vector2f(Character.Position.X, Character.Position.Y + 4);
                CheckCollision(Character.Position.X, Character.Position.Y + 4, Character.Position.X, Character.Position.Y + Character.Texture.Size.Y);
            }
        }

        public bool CheckCollision(float x, float y, float x2, float y2)
        {
            bool canMove = true;
            for (float xCheck = x; xCheck < x2; xCheck++)
            {
                for (float yCheck = y; yCheck < y2; yCheck++)
                {
                    Console.WriteLine("hello");
                    if (CollisionMap.GetPixel((uint)xCheck, (uint)yCheck) == Color.Blue)
                    {
                        canMove = false;
                        Console.WriteLine("worksss");
                    }
                }
            }

                /* if(!canmove)
                 *      kijken hoever ik wel kan bewegen
                 * 
                 * 
                 */

                return false;
            }
        }

        /* if(!canmove)
         *      kijken hoever ik wel kan bewegen
         * 
         * 
         */
}


