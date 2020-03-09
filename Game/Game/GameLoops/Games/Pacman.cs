using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    public class Pacman : GameLoop
    {
        bool canMove;
        public Pacman() : base() { }
        int Life = 2;
        int Score = 0;
        static SSprite Map;
        static SSprite BGscreen;
        static SSprite PacManLife;
        static PacmCharacter Character;
        static Image CollisionMap;
        static Image CollisionP;
        static Image CollisionSP;
        static Sound PacMan;
        string beginpcS = "Content/Pacman/beginpcS.wav";
        string eatpcS = "Content/Pacman/eatpcS.wav";
        string diepcS = "Content/Pacman/diepcS.wav";
        string superpcS = "Content/Pacman/superpcS.wav";

        public override void LoadContent()
        {
            Map = new SSprite(new Texture("Content/Pacman/Map.png"));
            Map.Position = new Vector2f(10, Program.Window.Size.Y / 2 - Map.Texture.Size.Y * Program.Scale / 2);
            Character = new PacmCharacter(new Texture("Content/Pacman/pacyellow.png"));
            CollisionMap = new Image("Content/Pacman/CollisionMap.png");
            CollisionP = new Image("Content/Pacman/pointmap.png");
            CollisionSP = new Image("Content/Pacman/Superpmapp.png");

            PacManLife = new SSprite(new Texture("Content/Pacman/PacManLife.png"));
            PacManLife.Position = new Vector2f(730, Program.Window.Size.Y / 2 - PacManLife.Texture.Size.Y * Program.Scale / 2);
            PacManLife = new SSprite(new Texture("Content/Pacman/PacManLife.png"));
            PacManLife.Position = new Vector2f(760, Program.Window.Size.Y / 2 - PacManLife.Texture.Size.Y * Program.Scale / 2);
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
            if (bgscreen == false)
            {
                PlaySound(beginpcS);
                BGscreen = new SSprite(new Texture("Content/Pacman/BGScreenpm.png"));
                BGscreen.Position = new Vector2f(65, Program.Window.Size.Y / 2 - BGscreen.Texture.Size.Y * Program.Scale / 2);
            }


        }

        public override void Initialise()
        {
            //Window.KeyPressed += Window_Keypressed;
            Character.Position = new Vector2f(300, 332);
        }

        public override void Update(GameTime gameTime)
        {
            if (Character.GetDirection() == 1)
            {
                if (CheckCollision((Character.Position.X - 4) / Program.Scale, Character.Position.Y / Program.Scale, Character.Position.X / Program.Scale, (Character.Position.Y + Character.Texture.Size.Y) / Program.Scale))
                {
                    Character.Position = new Vector2f(Character.Position.X - 4, Character.Position.Y);
                }
            }
            if (Character.GetDirection() == 2)
            {
                if (CheckCollision(Character.Position.X + Character.Texture.Size.X + 4, Character.Position.Y, Character.Position.X, Character.Position.Y + Character.Texture.Size.Y))
                {
                    Character.Position = new Vector2f(Character.Position.X + 4, Character.Position.Y);
                }
            }
            if (Character.GetDirection() == 3)
            {
                if (CheckCollision(Character.Position.X, Character.Position.Y - 4, Character.Position.X, Character.Position.Y + Character.Texture.Size.Y))
                {
                    Character.Position = new Vector2f(Character.Position.X, Character.Position.Y - 4);
                }
            }
            if (Character.GetDirection() == 4)
            {
                if (CheckCollision(Character.Position.X, Character.Position.Y + Character.Texture.Size.Y + 4, Character.Position.X, Character.Position.Y + Character.Texture.Size.Y))
                {
                    Character.Position = new Vector2f(Character.Position.X, Character.Position.Y + 4);
                }
            }
        }

        public bool CheckCollision(float x, float y, float x2, float y2)
        {
            bool canMove = true;
            for (float xCheck = x; xCheck < x2; xCheck++)
            {
                for (float yCheck = y; yCheck < y2; yCheck++)
                {
                    if (CollisionMap.GetPixel((uint)xCheck, (uint)yCheck) == Color.Blue)
                    {
                        canMove = false;
                        Console.WriteLine("worksss");
                    }
                }
            }
            return canMove;
        }
        bool CheckP(float b, float c, float b2, float c2)
        {
            for (float bCheck = b; bCheck < b2; bCheck++)
            {
                for (float cCheck = c; cCheck < c2; cCheck++)
                {
                    if (CollisionP.GetPixel((uint)bCheck, (uint)cCheck) == Color.Red)
                    {
                        Score += 20;
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

        public override void KeyInput(Keyboard.Key key)
        {
            if (key == Keyboard.Key.W)
            {
                Character.ChangeDirection(3);
            }
            if (key == Keyboard.Key.A)
            {
                Character.ChangeDirection(1);
            }
            if (key == Keyboard.Key.S)
            {
                Character.ChangeDirection(4);
            }
            if (key == Keyboard.Key.D)
            {
                Character.ChangeDirection(2);
            }
            if (key == Keyboard.Key.Return)
            {
                Character.ChangeDirection(5);
            }
        }
    }
}



