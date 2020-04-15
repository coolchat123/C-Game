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

        static SSprite OptionTitle;
        static SSprite OptionResolution;
        static Button OptionResolutionDown;
        static Button OptionResolutionUp;
        static Button OptionResolutionCheck;
        static SText OptionResolutionText;
        static Button OptionFullscreen;
        static Button OptionWindow;
        static Button OptionReturn;
        static Button Geluid0;
        static Button Geluid1;
        static Button Geluid2;
        static Button Geluid3;

        static Button GalleryReturn;

        public static int UnconfirmedResolution = 0;

        int Pan;
        int PanGoal;

        public Menu() : base() { }

        // UpdateResolutionText updates the text on the resolution option in the settings menu.
        // It is called when the resolution is changed.
        public void UpdateResolutionText()
        {
            OptionResolutionText.DisplayedString = (Program.Resolutions[UnconfirmedResolution].X + ", " + Program.Resolutions[UnconfirmedResolution].Y);
            OptionResolutionText.Position = new Vector2f(OptionResolution.Position.X + OptionResolution.Texture.Size.X / 2 - OptionResolutionText.GetGlobalBounds().Width / 2, OptionResolutionText.Position.Y);
            OptionResolutionText.RealPosition = new Vector2f(OptionResolutionText.Position.X + Pan, OptionResolutionText.Position.Y);
        }

        // LoadContent loads the textures of all our SSprites.
        // It is called when an instance of this class is created.
        public override void LoadContent()
        {
            Pan = 0;
            PanGoal = 0;

            // Load all SSprites of the main menu and add functions to events.
            MenuTitle = new SSprite(new Texture("Content/Menu/Title.png"), "menu");
            MenuPong = new Button(new Texture("Content/Menu/Pong.png"), "menu");
            MenuPong.Click += PongClick;
            MenuPong.MouseEnter += PongEnter;
            MenuPong.MouseLeave += PongLeave;
            MenuSnake = new Button(new Texture("Content/Menu/Snake.png"), "menu");
            MenuSnake.Click += SnakeClick;
            MenuSnake.MouseEnter += SnakeEnter;
            MenuSnake.MouseLeave += SnakeLeave;
            MenuPacman = new Button(new Texture("Content/Menu/Pacman.png"), "menu");
            MenuPacman.Click += PacmanClick;
            MenuPacman.MouseEnter += PacmanEnter;
            MenuPacman.MouseLeave += PacmanLeave;
            MenuBreakout = new Button(new Texture("Content/Menu/Breakout.png"), "menu");
            MenuBreakout.Click += BreakoutClick;
            MenuBreakout.MouseEnter += BreakoutEnter;
            MenuBreakout.MouseLeave += BreakoutLeave;
            MenuOptions = new Button(new Texture("Content/Menu/Options.png"), "menu");
            MenuOptions.Click += OptionsClick;
            MenuOptions.MouseEnter += OptionsEnter;
            MenuOptions.MouseLeave += OptionsLeave;
            MenuGallery = new Button(new Texture("Content/Menu/Gallery.png"), "menu");
            MenuGallery.Click += GalleryClick;
            MenuGallery.MouseEnter += GalleryEnter;
            MenuGallery.MouseLeave += GalleryLeave;

            // Load all SSprites of the options menu and add functions to events.
            OptionTitle = new SSprite(new Texture("Content/Menu/OptionTitle.png"), "options");
            OptionResolution = new SSprite(new Texture("Content/Menu/OptionResolution.png"), "options");
            OptionResolutionDown = new Button(new Texture("Content/Menu/OptionDown.png"), "options");
            OptionResolutionDown.Click += OptionResolutionDownClick;
            OptionResolutionDown.MouseEnter += OptionResolutionDownEnter;
            OptionResolutionDown.MouseLeave += OptionResolutionDownLeave;
            OptionResolutionUp = new Button(new Texture("Content/Menu/OptionUp.png"), "options");
            OptionResolutionUp.Click += OptionResolutionUpClick;
            OptionResolutionUp.MouseEnter += OptionResolutionUpEnter;
            OptionResolutionUp.MouseLeave += OptionResolutionUpLeave;
            OptionReturn = new Button(new Texture("Content/Menu/MenuRight.png"), "options");
            OptionReturn.Click += OptionReturnClick;
            OptionReturn.MouseEnter += OptionReturnEnter;
            OptionReturn.MouseLeave += OptionReturnLeave;
            OptionResolutionText = new SText(Program.Resolutions[Program.CurrentResolution].X + ", " + Program.Resolutions[Program.CurrentResolution].Y, 11, "options");
            OptionResolutionCheck = new Button(new Texture("Content/Menu/OptionCheck.png"), "options");
            OptionResolutionCheck.Click += OptionResolutionCheckClick;
            OptionResolutionCheck.MouseEnter += OptionResolutionCheckEnter;
            OptionResolutionCheck.MouseLeave += OptionResolutionCheckLeave;
            OptionFullscreen = new Button(new Texture("Content/Menu/OptionFullscreen.png"), "options");
            OptionFullscreen.Click += OptionFullscreenClick;
            OptionFullscreen.MouseEnter += OptionFullscreenEnter;
            OptionFullscreen.MouseLeave += OptionFullscreenLeave;
            OptionWindow = new Button(new Texture("Content/Menu/OptionWindow.png"), "options");
            OptionWindow.Click += OptionWindowClick;
            OptionWindow.MouseEnter += OptionWindowEnter;
            OptionWindow.MouseLeave += OptionWindowLeave;
            Geluid0 = new Button(new Texture("Content/Menu/geluid0.png"), "options");
            Geluid1 = new Button(new Texture("content/menu/geluid1.png"), "options");
            Geluid2 = new Button(new Texture("content/menu/geluid2.png"), "options");
            Geluid3 = new Button(new Texture("content/menu/geluid3.png"), "options");
            Geluid0.Click += Geluid0Click;
            Geluid0.MouseEnter += Geluid0Enter;
            Geluid0.MouseLeave += Geluid0Leave;
            Geluid1.Click += Geluid1Click;
            Geluid1.MouseEnter += Geluid1Enter;
            Geluid1.MouseLeave += Geluid1Leave;
            Geluid2.Click += Geluid2Click;
            Geluid2.MouseEnter += Geluid2Enter;
            Geluid2.MouseLeave += Geluid2Leave;
            Geluid3.Click += Geluid3Click;
            Geluid3.MouseEnter += Geluid3Enter;
            Geluid3.MouseLeave += Geluid3Leave;

            // Load all SSprites of the gallery menu and add functions to events.
            GalleryReturn = new Button(new Texture("Content/Menu/MenuLeft.png"), "options");
            GalleryReturn.Click += GalleryReturnClick;
            GalleryReturn.MouseEnter += GalleryReturnEnter;
            GalleryReturn.MouseLeave += GalleryReturnLeave;
        }

        // Initialise puts all the SSprites in their right place.
        // It is called when an instance of this class is created.
        public override void Initialise()
        {
            // Place all SSprites on the menu.
            float buttonSmallSize = MenuOptions.Texture.Size.X;
            float buttonLargeSize = MenuPong.Texture.Size.X;
            float gamePreviewExcess = (Program.Texture.Size.X - buttonLargeSize * 4) / 3.5f;

            // Place all SSprites on the main menu.
            MenuTitle.SetPosition(Program.Texture.Size.X / 2 - MenuTitle.Texture.Size.X / 2, Program.Texture.Size.Y / 4 - MenuTitle.Texture.Size.Y / 2);
            MenuPong.SetPosition(gamePreviewExcess, Program.Texture.Size.Y / 2);
            MenuSnake.SetPosition(gamePreviewExcess * 1.5f + buttonLargeSize, Program.Texture.Size.Y / 2);
            MenuPacman.SetPosition(gamePreviewExcess * 2f + buttonLargeSize * 2, Program.Texture.Size.Y / 2);
            MenuBreakout.SetPosition(gamePreviewExcess * 2.5f + buttonLargeSize * 3, Program.Texture.Size.Y / 2);
            MenuOptions.SetPosition(Program.Texture.Size.X / 6 - buttonSmallSize / 2, Program.Texture.Size.Y / 4 - MenuOptions.Texture.Size.Y / 2);
            MenuGallery.SetPosition(Program.Texture.Size.X / 6 * 5 - buttonSmallSize / 2, Program.Texture.Size.Y / 4 - MenuGallery.Texture.Size.Y / 2);

            // Place all SSprites on the options menu.
            OptionTitle.SetPosition(-Program.Texture.Size.X / 2 - OptionTitle.Texture.Size.X / 2, Program.Texture.Size.Y / 4 - OptionTitle.Texture.Size.Y / 2);
            OptionResolution.SetPosition(-Program.Texture.Size.X / 2 - OptionResolution.Texture.Size.X / 2, Program.Texture.Size.Y / 2);
            OptionResolutionDown.SetPosition(OptionResolution.Position.X + OptionResolution.Texture.Size.X, OptionResolution.Position.Y + OptionResolution.Texture.Size.Y / 2);
            OptionResolutionUp.SetPosition(OptionResolution.Position.X + OptionResolution.Texture.Size.X, OptionResolution.Position.Y);
            OptionResolutionCheck.SetPosition(OptionResolution.Position.X - OptionResolutionCheck.Texture.Size.X, OptionResolution.Position.Y);
            OptionResolutionText.SetPosition(OptionResolution.Position.X + OptionResolution.Texture.Size.X / 2 - OptionResolutionText.GetGlobalBounds().Width / 2, OptionResolution.Position.Y + 8);
            OptionReturn.SetPosition(MenuGallery.Position.X - Program.Texture.Size.X, MenuGallery.Position.Y);
            OptionFullscreen.SetPosition(-Program.Texture.Size.X / 2 - OptionFullscreen.Texture.Size.X - 2, OptionResolution.Position.Y - OptionFullscreen.Texture.Size.Y - 4); ;
            OptionWindow.SetPosition(-Program.Texture.Size.X / 2 + 2, OptionFullscreen.Position.Y);
            Geluid0.SetPosition(-Program.Texture.Size.X / 2 - 95, OptionFullscreen.Position.Y + 75);
            Geluid1.SetPosition(-Program.Texture.Size.X / 2 - 45, OptionFullscreen.Position.Y + 75);
            Geluid2.SetPosition(-Program.Texture.Size.X / 2 + 5, OptionFullscreen.Position.Y + 75);
            Geluid3.SetPosition(-Program.Texture.Size.X / 2 + 55, OptionFullscreen.Position.Y + 75);

            // Place all SSprites on the gallery menu.
            GalleryReturn.SetPosition(MenuOptions.Position.X + Program.Texture.Size.X, MenuOptions.Position.Y);
        }

        // Update is called by Program every tick.
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
            }

            foreach (SSprite sprite in Program.Sprites)
            {
                sprite.Position = new Vector2f(sprite.RealPosition.X - Pan, sprite.Position.Y);
            }
            foreach (SText text in Program.Strings)
            {
                text.Position = new Vector2f(text.RealPosition.X - Pan, text.Position.Y);
            }

            // The "confirm resolution"-button shouldn't be visible if the resolution hasn't been changed.
            if(UnconfirmedResolution == Program.CurrentResolution)
            {
                OptionResolutionCheck.Position = new Vector2f(OptionResolutionCheck.Position.X, Program.Window.Size.Y);
            }
            else
            {
                OptionResolutionCheck.Position = new Vector2f(OptionResolutionCheck.Position.X, OptionResolution.Position.Y);
            }
        }

        // KeyInput is called by Program every tick, once for every possibly relevant key that is being pressed by the player.
        public override void KeyInput(Keyboard.Key key)
        {
        }

        // Options button
        public void OptionsClick(object sender, EventArgs e)
        {
            PanGoal = -(int)Program.Texture.Size.X;
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
        public void OptionResolutionDownClick(object sender, EventArgs e)
        {
            if(UnconfirmedResolution > 0 && !Program.Fullscreen)
            {
                UnconfirmedResolution -= 1;
                UpdateResolutionText();
            }
        }
        public void OptionResolutionDownEnter(object sender, EventArgs e)
        {
            OptionResolutionDown.SetScale(1.1f, SSprite.Pin.TopLeft);
        }
        public void OptionResolutionDownLeave(object sender, EventArgs e)
        {
            OptionResolutionDown.SetScale(1f, SSprite.Pin.TopLeft);
        }

        // Increase Resolution button in Options
        public void OptionResolutionUpClick(object sender, EventArgs e)
        {
            if(UnconfirmedResolution < Program.Resolutions.Count - 1 && !Program.Fullscreen)
            {
                UnconfirmedResolution += 1;
                UpdateResolutionText();
            }
        }
        public void OptionResolutionUpEnter(object sender, EventArgs e)
        {
            OptionResolutionUp.SetScale(1.1f, SSprite.Pin.BottomLeft);
        }
        public void OptionResolutionUpLeave(object sender, EventArgs e)
        {
            OptionResolutionUp.SetScale(1f, SSprite.Pin.BottomLeft);
        }

        // Confirm Resolution button in Options
        public void OptionResolutionCheckClick(object sender, EventArgs e)
        {
            if (!Program.Fullscreen)
            {
                Program.CurrentResolution = UnconfirmedResolution;
                Program.ResizeWindow();
                UpdateResolutionText();
            }
        }
        public void OptionResolutionCheckEnter(object sender, EventArgs e)
        {
            OptionResolutionCheck.SetScale(1.1f, SSprite.Pin.MiddleLeft);
        }
        public void OptionResolutionCheckLeave(object sender, EventArgs e)
        {
            OptionResolutionCheck.SetScale(1f, SSprite.Pin.MiddleLeft);
        }

        // Return button in options menu
        public void OptionReturnClick(object sender, EventArgs e)
        {
            PanGoal = 0;
        }
        public void OptionReturnEnter(object sender, EventArgs e)
        {
            OptionReturn.SetScale(1.2f, SSprite.Pin.Middle);
        }
        public void OptionReturnLeave(object sender, EventArgs e)
        {
            OptionReturn.SetScale(1f, SSprite.Pin.Middle);
        }

        // Return button in gallery menu
        public void GalleryReturnClick(object sender, EventArgs e)
        {
            PanGoal = 0;
        }
        public void GalleryReturnEnter(object sender, EventArgs e)
        {
            GalleryReturn.SetScale(1.2f, SSprite.Pin.Middle);
        }
        public void GalleryReturnLeave(object sender, EventArgs e)
        {
            GalleryReturn.SetScale(1f, SSprite.Pin.Middle);
        }

        // Fullscreen button in Options
        public void OptionFullscreenClick(object sender, EventArgs e)
        {
            if (!Program.Fullscreen)
            {
                Program.ResizeWindow(true);
                UpdateResolutionText();
            }
        }
        public void OptionFullscreenEnter(object sender, EventArgs e)
        {
            OptionFullscreen.SetScale(1.1f, SSprite.Pin.MiddleLeft);
        }
        public void OptionFullscreenLeave(object sender, EventArgs e)
        {
            OptionFullscreen.SetScale(1f, SSprite.Pin.MiddleLeft);
        }

        // Windowed button in Options
        public void OptionWindowClick(object sender, EventArgs e)
        {
            if (Program.Fullscreen)
            {
                Program.ResizeWindow();
                UpdateResolutionText();
            }
        }
        public void OptionWindowEnter(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1.1f, SSprite.Pin.MiddleLeft);
        }
        public void OptionWindowLeave(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1f, SSprite.Pin.MiddleLeft);
        }

        public void Geluid0Click(object sender, EventArgs e)
        {
            Program.sound = 0;
            Console.Write("New sound: {0}", Program.sound);
        }
        public void Geluid0Enter(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1.1f, SSprite.Pin.MiddleLeft);
        }
        public void Geluid0Leave(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1f, SSprite.Pin.MiddleLeft);
        }

        public void Geluid1Click(object sender, EventArgs e)
        {
            Program.sound = 33;
            Console.Write("New sound: {0}", Program.sound);
        }
        public void Geluid1Enter(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1.1f, SSprite.Pin.MiddleLeft);
        }
        public void Geluid1Leave(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1f, SSprite.Pin.MiddleLeft);
        }

        public void Geluid2Click(object sender, EventArgs e)
        {
            Program.sound = 67;
            Console.Write("New sound: {0}", Program.sound);
        }
        public void Geluid2Enter(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1.1f, SSprite.Pin.MiddleLeft);
        }
        public void Geluid2Leave(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1f, SSprite.Pin.MiddleLeft);
        }

        public void Geluid3Click(object sender, EventArgs e)
        {
            Program.sound = 100;
            Console.Write("New sound: {0}", Program.sound);
        }
        public void Geluid3Enter(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1.1f, SSprite.Pin.MiddleLeft);
        }
        public void Geluid3Leave(object sender, EventArgs e)
        {
            OptionWindow.SetScale(1f, SSprite.Pin.MiddleLeft);
        }
    }
}
