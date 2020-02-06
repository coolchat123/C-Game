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
    public class Game : GameLoop
    {
        // Dit zijn de drie constant properties van de Game class.
        public const uint DEFAULT_WINDOW_WIDTH = 800;
        public const uint DEFAULT_WINDOW_HEIGHT = 600;
        public const string DEFAULT_WINDOW_TITLE = "Steenboy Color";

        static Sprite sMenuTitle = new Sprite(new Texture("Content/Menu/Title.png"));
        static Sprite sMenuPong = new Sprite(new Texture("Content/Menu/Pong.png"));
        static Sprite sMenuGame4 = new Sprite(new Texture("Content/Menu/Game4.png"));
        static Sprite sMenuRacing = new Sprite(new Texture("Content/Menu/Racing.png"));
        static Sprite sMenuBreakout = new Sprite(new Texture("Content/Menu/Breakout.png"));
        static Sprite sMenuOptions = new Sprite(new Texture("Content/Menu/Options.png"));
        static Sprite sMenuGallery = new Sprite(new Texture("Content/Menu/Gallery.png"));

        // Dit is de constructor van de Game class.
        // Omdat Game een child class is van GameLoop, roepen we hier de constructor van GameLoop aan.
        // Dat doen we met behulp van het keyword "base".
        // We spelen de constant properties DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT en DEFAULT_WINDOW_TITLE door
        // naar de GameLoop contructor als parameters.
        // Ook geven we Color.Blue door als achtergrondkleur; dit is een van de kleuren die SFML bij naam kent.
        public Game() : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, DEFAULT_WINDOW_TITLE, Color.Blue)
        {

        }

        // De LoadContent method is een override van de LoadContent method in GameLoop.
        // De GameLoop.LoadContent method is abstract, net als de GameLoop class zelf.
        // Net als de GameLoop class kan de method niet op zichzelf gebruikt worden.
        // Deze override kunnen we wel gebruiken.
        public override void LoadContent()
        {
        }
        public override void Initialise()
        {
            float buttonSmallSize = sMenuOptions.Texture.Size.X;
            float buttonLargeSize = sMenuPong.Texture.Size.X;
            float gamePreviewX = Window.Size.X / 4;
            float gamePreviewY = Window.Size.Y / 3 * 2;
            float gamePreviewExcess = Window.Size.X - buttonLargeSize * 4;

            sMenuTitle.Position = new Vector2f(Window.Size.X / 2 - sMenuTitle.Texture.Size.X / 2, gamePreviewY / 2 - sMenuTitle.Texture.Size.Y / 2);
            sMenuPong.Position = new Vector2f(gamePreviewX - buttonLargeSize - gamePreviewExcess / 8, gamePreviewY - buttonLargeSize / 2);
            sMenuGame4.Position = new Vector2f(gamePreviewX * 2 - buttonLargeSize - gamePreviewExcess / 8, gamePreviewY - buttonLargeSize / 2);
            sMenuRacing.Position = new Vector2f(gamePreviewX * 3 - buttonLargeSize - gamePreviewExcess / 8, gamePreviewY - buttonLargeSize / 2);
            sMenuBreakout.Position = new Vector2f(gamePreviewX * 4 - buttonLargeSize - gamePreviewExcess / 8, gamePreviewY - buttonLargeSize / 2);
            sMenuOptions.Position = new Vector2f(Window.Size.X / 6 - buttonSmallSize / 2, gamePreviewY / 2 - buttonSmallSize / 2);
            sMenuGallery.Position = new Vector2f(Window.Size.X / 6 * 5 - buttonSmallSize / 2, gamePreviewY / 2 - buttonSmallSize / 2);
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(GameTime gameTime)
        {
            Window.Draw(sMenuTitle);
            Window.Draw(sMenuPong);
            Window.Draw(sMenuGame4);
            Window.Draw(sMenuRacing);
            Window.Draw(sMenuBreakout);
            Window.Draw(sMenuOptions);
            Window.Draw(sMenuGallery);
        }
    }
}
