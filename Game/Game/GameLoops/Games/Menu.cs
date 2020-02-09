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
        static SSprite sMenuTitle;
        static SSprite sMenuPong;
        static SSprite sMenuGame4;
        static SSprite sMenuRacing;
        static SSprite sMenuBreakout;
        static SSprite sMenuOptions;
        static SSprite sMenuGallery;

        public Menu() : base() { }

        public override void LoadContent()
        {
            sMenuTitle = new SSprite(new Texture("Content/Menu/Title.png"));
            sMenuPong = new SSprite(new Texture("Content/Menu/Pong.png"));
            sMenuGame4 = new SSprite(new Texture("Content/Menu/Game4.png"));
            sMenuRacing = new SSprite(new Texture("Content/Menu/Racing.png"));
            sMenuBreakout = new SSprite(new Texture("Content/Menu/Breakout.png"));
            sMenuOptions = new SSprite(new Texture("Content/Menu/Options.png"));
            sMenuGallery = new SSprite(new Texture("Content/Menu/Gallery.png"));
        }

        public override void Initialise()
        {
            float buttonSmallSize = sMenuOptions.Texture.Size.X;
            float buttonLargeSize = sMenuPong.Texture.Size.X;
            float gamePreviewExcess = (Program.Window.Size.X - buttonLargeSize * 4) / 3.5f;

            sMenuTitle.Position = new Vector2f(Program.Window.Size.X / 2 - sMenuTitle.Texture.Size.X / 2, Program.Window.Size.Y / 4 - sMenuTitle.Texture.Size.Y / 2);
            sMenuPong.Position = new Vector2f(gamePreviewExcess, Program.Window.Size.Y / 2);
            sMenuGame4.Position = new Vector2f(gamePreviewExcess * 1.5f + buttonLargeSize, Program.Window.Size.Y / 2);
            sMenuRacing.Position = new Vector2f(gamePreviewExcess * 2f + buttonLargeSize * 2, Program.Window.Size.Y / 2);
            sMenuBreakout.Position = new Vector2f(gamePreviewExcess * 2.5f + buttonLargeSize * 3, Program.Window.Size.Y / 2);
            sMenuOptions.Position = new Vector2f(Program.Window.Size.X / 6 - buttonSmallSize / 2, Program.Window.Size.Y / 4 - sMenuOptions.Texture.Size.Y / 2);
            sMenuGallery.Position = new Vector2f(Program.Window.Size.X / 6 * 5 - buttonSmallSize / 2, Program.Window.Size.Y / 4 - sMenuGallery.Texture.Size.Y / 2);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
