using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    // De Game class is een "child class" van de GameLoop class.
    // Omdat GameLoop een abstracte class is, kan hij niet op zichzelf gebruikt worden.
    // We moesten er dus een child class van maken die we wel kunnen gebruiken.
    // Omdat Game een child class is van GameLoop, heeft het alle methods, structures en properties van de GameLoop class,
    // plus alle methods, structures en properties die we hier maken.
    public class Menu : GameLoop
    {
        static SSprite MenuTitle;
        static Button MenuPong;
        static Button MenuSnake;
        static Button MenuPacman;
        static Button MenuBreakout;
        static Button MenuOptions;
        static Button MenuGallery;

        static SSprite OptionResolution;
        static SSprite OptionVolume;
        static Button OptionResLeft;
        static Button OptionResRight;
        static SText OptionResolutionText;

        public static List<int> SpriteScale = new List<int>
        {
            1, 2, 3, 4, 5, 6, 7
        };

        int Pan;
        int PanGoal;

        public Menu() : base() { }

        // LoadOptions loads the Options menu.
        // It is called when the screen pans left.
        public void LoadOptions()
        {
            OptionResolution = new SSprite(new Texture("Content/Menu/OptionResolution.png"));
            OptionVolume = new SSprite(new Texture("Content/Menu/OptionVolume.png"));


            OptionResLeft = new Button(Program.Window, new Texture("Content/Menu/OptionLeft.png"));
            OptionResLeft.Click += OptionResLeftClick;
            OptionResLeft.MouseEnter += OptionResLeftEnter;
            OptionResLeft.MouseLeave += OptionResLeftLeave;
            OptionResRight = new Button(Program.Window, new Texture("Content/Menu/OptionRight.png"));
            OptionResRight.Click += OptionResRightClick;
            OptionResRight.MouseEnter += OptionResRightEnter;
            OptionResRight.MouseLeave += OptionResRightLeave;

            OptionResolutionText = new SText(Program.Resolutions[Program.CurrentResolution].X + ", " + Program.Resolutions[Program.CurrentResolution].Y, 11);

            InitialiseOptions();
        }

        // InitialiseOptions initialises the Options menu.
        // It is called after LoadOptions.
        public void InitialiseOptions()
        {
            //OptionText.SetPosition(-Program.Texture.Size.X / 2 - OptionVolume.Texture.Size.X / 2, 0);
            OptionVolume.SetPosition(-Program.Texture.Size.X / 2 - OptionVolume.Texture.Size.X / 2, 50);
            OptionResolution.SetPosition(-Program.Texture.Size.X / 2 - OptionResolution.Texture.Size.X / 2, Program.Texture.Size.Y / 2);
            OptionResLeft.SetPosition(OptionResolution.Position.X - OptionResLeft.Texture.Size.X, OptionResolution.Position.Y + OptionResolution.Texture.Size.Y / 2);
            OptionResRight.SetPosition(OptionResolution.Position.X + OptionResolution.Texture.Size.X, OptionResolution.Position.Y + OptionResolution.Texture.Size.Y / 2);
            OptionResolutionText.SetPosition(OptionResolution.Position.X + OptionResolution.Texture.Size.X / 2 - OptionResolutionText.GetGlobalBounds().Width / 2, OptionResolution.Position.Y + 16);
        }

        public override void LoadContent()
        {
            Pan = 0;
            PanGoal = 0;

            // Load all SSprites and add functions to events.
            MenuTitle = new SSprite(new Texture("Content/Menu/Title.png"));
            MenuPong = new Button(Program.Window, new Texture("Content/Menu/Pong.png"));
            MenuPong.Click += PongClick;
            MenuPong.MouseEnter += PongEnter;
            MenuPong.MouseLeave += PongLeave;
            MenuSnake = new Button(Program.Window, new Texture("Content/Menu/Snake.png"));
            MenuSnake.Click += SnakeClick;
            MenuSnake.MouseEnter += SnakeEnter;
            MenuSnake.MouseLeave += SnakeLeave;
            MenuPacman = new Button(Program.Window, new Texture("Content/Menu/Pacman.png"));
            MenuPacman.Click += PacmanClick;
            MenuPacman.MouseEnter += PacmanEnter;
            MenuPacman.MouseLeave += PacmanLeave;
            MenuBreakout = new Button(Program.Window, new Texture("Content/Menu/Breakout.png"));
            MenuBreakout.Click += BreakoutClick;
            MenuBreakout.MouseEnter += BreakoutEnter;
            MenuBreakout.MouseLeave += BreakoutLeave;
            MenuOptions = new Button(Program.Window, new Texture("Content/Menu/Options.png"));
            MenuOptions.Click += OptionsClick;
            MenuOptions.MouseEnter += OptionsEnter;
            MenuOptions.MouseLeave += OptionsLeave;
            MenuGallery = new Button(Program.Window, new Texture("Content/Menu/Gallery.png"));
            MenuGallery.Click += GalleryClick;
            MenuGallery.MouseEnter += GalleryEnter;
            MenuGallery.MouseLeave += GalleryLeave;
        }

        public override void Initialise()
        {
            // Place all SSprites on the menu.
            float buttonSmallSize = MenuOptions.Texture.Size.X;
            float buttonLargeSize = MenuPong.Texture.Size.X;
            float gamePreviewExcess = (Program.Texture.Size.X - buttonLargeSize * 4) / 3.5f;

            MenuTitle.SetPosition(Program.Texture.Size.X / 2 - MenuTitle.Texture.Size.X / 2, Program.Texture.Size.Y / 4 - MenuTitle.Texture.Size.Y / 2);
            MenuPong.SetPosition(gamePreviewExcess, Program.Texture.Size.Y / 2);
            MenuSnake.SetPosition(gamePreviewExcess * 1.5f + buttonLargeSize, Program.Texture.Size.Y / 2);
            MenuPacman.SetPosition(gamePreviewExcess * 2f + buttonLargeSize * 2, Program.Texture.Size.Y / 2);
            MenuBreakout.SetPosition(gamePreviewExcess * 2.5f + buttonLargeSize * 3, Program.Texture.Size.Y / 2);
            MenuOptions.SetPosition(Program.Texture.Size.X / 6 - buttonSmallSize / 2, Program.Texture.Size.Y / 4 - MenuOptions.Texture.Size.Y / 2);
            MenuGallery.SetPosition(Program.Texture.Size.X / 6 * 5 - buttonSmallSize / 2, Program.Texture.Size.Y / 4 - MenuGallery.Texture.Size.Y / 2);
        }

        public override void Update(GameTime gameTime)
        {
            if(Pan != PanGoal)
            {
                if(Math.Abs(Pan - PanGoal) < 10)
                {
                    Pan = PanGoal;
                }
                else
                {
                    if (PanGoal < Pan)
                    {
                        Pan += Math.Min((PanGoal - Pan) / 10, -5);
                    }
                    else
                    {
                        Pan += Math.Max((PanGoal - Pan) / 10, 5);
                    }
                }

                foreach (SSprite sprite in Program.Sprites)
                {
                    sprite.Position = new Vector2f(sprite.RealPosition.X - Pan, sprite.Position.Y);
                }
                foreach (SText text in Program.Strings)
                {
                    text.Position = new Vector2f(text.RealPosition.X - Pan, text.Position.Y);
                }
            }
        }

        // Options button
        public void OptionsClick(object sender, EventArgs e)
        {
            PanGoal = -(int)Program.Texture.Size.X;
            LoadOptions();
        }
        public void OptionsEnter(object sender, EventArgs e)
        {
            MenuOptions.SetScale(1.2f, SSprite.Pin.Middle);
        }
        public void OptionsLeave(object sender, EventArgs e)
        {
            MenuOptions.SetScale(1f, SSprite.Pin.Middle);
        }

        // Gallery button
        public void GalleryClick(object sender, EventArgs e)
        {
            PanGoal = (int)Program.Texture.Size.X;
        }
        public void GalleryEnter(object sender, EventArgs e)
        {
            MenuGallery.SetScale(1.2f, SSprite.Pin.Middle);
        }
        public void GalleryLeave(object sender, EventArgs e)
        {
            MenuGallery.SetScale(1f, SSprite.Pin.Middle);
        }

        // Pong button
        public void PongClick(object sender, EventArgs e)
        {
            Program.ChangeGame = Program.GameName.Pong;
        }
        public void PongEnter(object sender, EventArgs e)
        {
            MenuPong.SetScale(1.1f, SSprite.Pin.BottomMiddle);
        }
        public void PongLeave(object sender, EventArgs e)
        {
            MenuPong.SetScale(1f, SSprite.Pin.BottomMiddle);
        }

        // Snake button
        public void SnakeClick(object sender, EventArgs e)
        {
            Program.ChangeGame = Program.GameName.Snake;
        }
        public void SnakeEnter(object sender, EventArgs e)
        {
            MenuSnake.SetScale(1.1f, SSprite.Pin.BottomMiddle);
        }
        public void SnakeLeave(object sender, EventArgs e)
        {
            MenuSnake.SetScale(1f, SSprite.Pin.BottomMiddle);
        }

        // Pacman button
        public void PacmanClick(object sender, EventArgs e)
        {
            Program.ChangeGame = Program.GameName.Pacman;
        }
        public void PacmanEnter(object sender, EventArgs e)
        {
            MenuPacman.SetScale(1.1f, SSprite.Pin.BottomMiddle);
        }
        public void PacmanLeave(object sender, EventArgs e)
        {
            MenuPacman.SetScale(1f, SSprite.Pin.BottomMiddle);
        }

        // Breakout button
        public void BreakoutClick(object sender, EventArgs e)
        {
            Program.ChangeGame = Program.GameName.Breakout;
        }
        public void BreakoutEnter(object sender, EventArgs e)
        {
            MenuBreakout.SetScale(1.1f, SSprite.Pin.BottomMiddle);
        }
        public void BreakoutLeave(object sender, EventArgs e)
        {
            MenuBreakout.SetScale(1f, SSprite.Pin.BottomMiddle);
        }

        // Decrease Resolution button in Options
        public void OptionResLeftClick(object sender, EventArgs e)
        {
            if(Program.CurrentResolution > 0)
            {
                Program.CurrentResolution -= 1;
                Program.ResizeWindow();
            }
        }
        public void OptionResLeftEnter(object sender, EventArgs e)
        {
            if (Pan == PanGoal)
            {
                OptionResLeft.SetScale(1.1f, SSprite.Pin.MiddleRight);
            }
        }
        public void OptionResLeftLeave(object sender, EventArgs e)
        {
            if (Pan == PanGoal)
            {
                OptionResLeft.SetScale(1f, SSprite.Pin.MiddleRight);
            }
        }

        // Increase Resolution button in Options
        public void OptionResRightClick(object sender, EventArgs e)
        {
            if(Program.CurrentResolution < Program.Resolutions.Count - 1)
            {
                Program.CurrentResolution += 1;
                Program.ResizeWindow();
            }
        }
        public void OptionResRightEnter(object sender, EventArgs e)
        {
            if (Pan == PanGoal)
            {
                OptionResRight.SetScale(1.1f, SSprite.Pin.MiddleLeft);
            }
        }
        public void OptionResRightLeave(object sender, EventArgs e)
        {
            if (Pan == PanGoal)
            {
                OptionResRight.SetScale(1f, SSprite.Pin.MiddleLeft);
            }
        }
    }
}
