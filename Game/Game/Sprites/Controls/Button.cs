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

        public Button(Texture texture, string group1 = null, string group2 = null) : base(texture, group1, group2)
        {
            MouseOver = false;
            JustClicked = 0;
        }

        public Button(Texture texture, float locationX, float locationY, string group1 = null, string group2 = null) : this(texture, group1, group2)
        {
            Position = new Vector2f(locationX, locationY);
        }

        public Button(Texture texture, Vector2f location, string group1 = null, string group2 = null) : this(texture, location.X, location.Y, group1, group2) { }

        // The "Click" event is called by Program when this button is clicked.
        public event EventHandler Click;
        public void PerformClick()
        {
            Click(this, EventArgs.Empty);
            JustClicked = 10;
        }

        // The "MouseEnter" event is called by Program when the mouse enters this button's hitbox.
        public event EventHandler MouseEnter;
        public void PerformMouseEnter()
        {
            MouseEnter(this, EventArgs.Empty);
        }

        // The "MouseLeave" event is called by Program when the mouse leaves this button's hitbox.
        public event EventHandler MouseLeave;
        public void PerformMouseLeave()
        {
            MouseLeave(this, EventArgs.Empty);
        }

        public bool MouseOver;
        public byte JustClicked;
    }
}
