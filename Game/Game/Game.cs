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
        public const uint DEFAULT_WINDOW_WIDTH = 640;
        public const uint DEFAULT_WINDOW_HEIGHT = 480;
        public const string DEFAULT_WINDOW_TITLE = "Steenboy Color";

        // Dit is de constructor van de Game class.
        // Omdat Game een child class is van GameLoop, roepen we hier de constructor van GameLoop aan.
        // Dat doen we met behulp van het keyword "base".
        // We spelen de constant properties DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT en DEFAULT_WINDOW_TITLE door
        // naar de GameLoop contructor als parameters.
        // Ook geven we Color.Blue door als achtergrondkleur; dit is een van de kleuren die SFML bij naam kent.
        public Game() : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, DEFAULT_WINDOW_TITLE, Color.Red)
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
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(GameTime gameTime)
        {
        }
    }
}
