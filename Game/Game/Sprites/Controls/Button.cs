using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    public class Button : SSprite
    {
        public Button() { }

        public Button(RenderWindow window, Texture texture) : base(texture)
        {
            Vector2f mousePosition = window.MapPixelToCoords(Mouse.GetPosition(window));
            MouseOver = GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y);
        }

        public Button(RenderWindow window, Texture texture, float locationX, float locationY) : this(window, texture)
        {
            Position = new Vector2f(locationX, locationY);
        }

        public Button(RenderWindow window, Texture texture, Vector2f location) : this(window, texture, location.X, location.Y) { }

        // The "Click" event is called when this button is clicked.
        public event EventHandler Click;
        public void PerformClick()
        {
            Click(this, EventArgs.Empty);
        }

        // The "MouseEnter" event is called when the mouse enters this button's hitbox.
        public event EventHandler MouseEnter;
        public void PerformMouseEnter()
        {
            Scale = new Vector2f(1.2f, 1.2f);
            Position = new Vector2f(RealPosition.X + (1f - Scale.X) * Texture.Size.X / 2, RealPosition.Y + (1f - Scale.Y) * Texture.Size.Y / 2);
            MouseEnter(this, EventArgs.Empty);
        }

        // The "MouseLeave" event is called when the mouse leaves this button's hitbox.
        public event EventHandler MouseLeave;
        public void PerformMouseLeave()
        {
            Scale = new Vector2f(1f, 1f);
            Position = RealPosition;
            MouseLeave(this, EventArgs.Empty);
        }

        public bool MouseOver;
    }
}
