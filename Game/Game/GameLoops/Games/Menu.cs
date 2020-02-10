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

        public Menu() : base() { }

        public override void LoadContent()
        {
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
            float gamePreviewExcess = (Program.Window.Size.X - buttonLargeSize * 4) / 3.5f;

            MenuTitle.Position = new Vector2f(Program.Window.Size.X / 2 - MenuTitle.Texture.Size.X / 2, Program.Window.Size.Y / 4 - MenuTitle.Texture.Size.Y / 2);
            MenuPong.Position = new Vector2f(gamePreviewExcess, Program.Window.Size.Y / 2);
            MenuSnake.Position = new Vector2f(gamePreviewExcess * 1.5f + buttonLargeSize, Program.Window.Size.Y / 2);
            MenuPacman.Position = new Vector2f(gamePreviewExcess * 2f + buttonLargeSize * 2, Program.Window.Size.Y / 2);
            MenuBreakout.Position = new Vector2f(gamePreviewExcess * 2.5f + buttonLargeSize * 3, Program.Window.Size.Y / 2);
            MenuOptions.Position = new Vector2f(Program.Window.Size.X / 6 - buttonSmallSize / 2, Program.Window.Size.Y / 4 - MenuOptions.Texture.Size.Y / 2);
            MenuGallery.Position = new Vector2f(Program.Window.Size.X / 6 * 5 - buttonSmallSize / 2, Program.Window.Size.Y / 4 - MenuGallery.Texture.Size.Y / 2);
        }

        public override void Update(GameTime gameTime)
        {
        }

        // Options button
        public void OptionsClick(object sender, EventArgs e)
        {
        }
        public void OptionsEnter(object sender, EventArgs e)
        {
        }
        public void OptionsLeave(object sender, EventArgs e)
        {
        }

        // Gallery button
        public void GalleryClick(object sender, EventArgs e)
        {
        }
        public void GalleryEnter(object sender, EventArgs e)
        {
        }
        public void GalleryLeave(object sender, EventArgs e)
        {
        }

        // Pong button
        public void PongClick(object sender, EventArgs e)
        {
        }
        public void PongEnter(object sender, EventArgs e)
        {
        }
        public void PongLeave(object sender, EventArgs e)
        {
        }

        // Snake button
        public void SnakeClick(object sender, EventArgs e)
        {
        }
        public void SnakeEnter(object sender, EventArgs e)
        {
        }
        public void SnakeLeave(object sender, EventArgs e)
        {
        }

        // Pacman button
        public void PacmanClick(object sender, EventArgs e)
        {
        }
        public void PacmanEnter(object sender, EventArgs e)
        {
        }
        public void PacmanLeave(object sender, EventArgs e)
        {
        }

        // Breakout button
        public void BreakoutClick(object sender, EventArgs e)
        {
        }
        public void BreakoutEnter(object sender, EventArgs e)
        {
        }
        public void BreakoutLeave(object sender, EventArgs e)
        {
        }
    }
}
