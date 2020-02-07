using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    public class Button
    {
        public Button() { }

        public Button(RenderWindow window, Texture texture, float locationX, float locationY)
        {
            Sprite = new Sprite(texture);
            Sprite.Position = new Vector2f(locationX, locationY);

            Vector2f mousePosition = window.MapPixelToCoords(Mouse.GetPosition(window));
            MouseOver = (Sprite.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y));
        }

        public Button(RenderWindow window, Texture texture, Vector2f location)
        {
            Sprite = new Sprite(texture);
            Sprite.Position = location;
        }

        // De "Click" function wordt gecalld wanneer er op de button geklikt wordt.
        public event EventHandler Click;
        public void PerformClick()
        {
            Click(this, EventArgs.Empty);
        }

        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;

        bool MouseOver;

        // Er is een sprite verbonden aan de button.
        // Deze bepaalt hoe de button eruitziet, hoe groot de button is, en waar hij staat.
        Sprite Sprite;
        public void setSprite(Texture texture)
        {
            Sprite = new Sprite(texture);
        }
        public void setSprite(Sprite sprite)
        {
            Sprite = sprite;
        }
        public Sprite getSprite()
        {
            return Sprite;
        }

        // De "setLocation" en "getLocation" functions worden gebruikt om via de Button contact te maken met de "Position" field van de Sprite.
        public void setLocation(float locationX, float locationY)
        {
            Sprite.Position = new Vector2f(locationX, locationY);
        }
        public void setLocation(Vector2f location)
        {
            Sprite.Position = location;
        }
        public Vector2f getLocation()
        {
            return Sprite.Position;
        }

        /* Handleiding om een button toe te voegen.
         * 1. Maak hem aan
         *      Button voorbeeldButton;
         *      
         * 2. Geef hem een waarde
         *      voorbeeldButton = new Button(Window, new Texture("Content/Menu/NaamVanTexture.png"), PositieX, PositieY);
         *      
         * 3. Verbind er dan een function aan waarin staat wat er gebeurd als de button geklikt wordt
         *      voorbeeldButton.Click += new EventHandler( NaamVanFunctie );
         *      
         * 4. Maak de functie, met de argumenten die in het voorbeeld staan:
         *      public void NaamVanFunctie(object sender, EventArgs e)
         *      {
         *          // code hier
         *      }
         *      
         * 5. De Click functie kan op de volgende manier gecalld worden:
         *      voorbeeldButton.PerformClick();
         *    
         * 6. In een toekomstige versie zal het programma, in de Update() functie, automatisch kijken of er op buttons geklikt wordt.
         *    Dit zal gebeuren met behulp van de MouseEnter en MouseLeave events, die op het moment ongebruikt zijn.
         */
    }
}
