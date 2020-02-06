using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    // GameLoop is een abstracte class.
    // Een abstracte class kan niet op zichzelf gebruikt worden.
    // Het is alleen een basis voor andere, niet-abstracte classes; in dit geval de Game class.
    // GameLoop is dus de parent class van de Game class.
    public abstract class GameLoop
    {
        // Deze cijfers zijn constants.
        // Constants zijn het tegenovergestelde van variabelen; Je kunt ze namelijk niet veranderen.
        // Omdat het constants zijn, wordt de hele naam in hoofdletters gespeld.
        // Het zijn ook properties van de GameLoop class, omdat ze zich direct in de class bevinden.
        // Als ze in een method of een structure zaten, zouden het geen properties zijn.
        public const int TARGET_FPS = 60;
        public const float TIME_UNTIL_UPDATE = 1f / TARGET_FPS;

        // De property Window wordt het oppervlak waarop we textures renderen.
        // Het is een instance van de class RenderWindow.
        // RenderWindow is een class van SFML, die helpt met het renderen van plaatjes op het scherm.
        // Omdat Window een property is, is de eerste letter van elk woord een hoofdletter.
        // Dit heet PascalCase, of UpperCamelCase.
        public RenderWindow Window
        {
            get;
            protected set;
        }

        // GameTime is een instance van de class GameTime.
        // De class GameTime is een zelfgemaakte class, die helpt met het bijhouden van tijd.
        public GameTime GameTime
        {
            get;
            protected set;
        }

        // WindowClearColour is de achtergrondkleur van onze RenderWindow.
        // Het is een instance van de structure Color.
        // Een structure is een kleine verzameling verschillende variables, die samen een geheel vormen,
        // en vaak ook een aantal functions waarmee de programmeur de structure kan gebruiken.
        // Color is een structure van SFML, bestaande uit vier bytes: namelijk Red, Green, Blue en Alpha.
        public Color WindowClearColour
        {
            get;
            protected set;
        }

        // Dit is de constructor van de GameLoop class.
        // Een constructor is een speciale method, die wordt afgespeeld zodra er een instance van de class wordt aangemaakt.
        // Meestal wordt de constructor gebruikt om de properties van de class te initialiseren.
        protected GameLoop(uint windowWidth, uint windowHeight, string windowTitle, Color windowColour)
        {
            this.WindowClearColour = windowColour;
            this.Window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle);
            this.GameTime = new GameTime();
            Window.Closed += Window_Closed;
        }

        // De Run method bevat de game loop.
        public void Run()
        {
            LoadContent();
            Initialise();

            // Deze vier floats bevinden zich in een method.
            // Daarom zijn het geen properties, en is de eerste letter een kleine letter.
            // Dit heet lowerCamelCase.
            float totalTimeBeforeUpdate = 0f;
            float previousTimeElapsed = 0f;
            float deltaTime = 0f;
            float totalTimeElapsed = 0f;

            Clock clock = new Clock();

            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if(totalTimeBeforeUpdate >= TIME_UNTIL_UPDATE)
                {
                    GameTime.Update(totalTimeBeforeUpdate, clock.ElapsedTime.AsSeconds());
                    totalTimeBeforeUpdate = 0f;

                    Update(GameTime);

                    Window.Clear(WindowClearColour);

                    Draw(GameTime);

                    Window.Display();
                }
            }
        }
        private void Window_Closed(object sender, EventArgs a)
        {
            Window.Close();
        }
        public abstract void LoadContent();
        public abstract void Initialise();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}