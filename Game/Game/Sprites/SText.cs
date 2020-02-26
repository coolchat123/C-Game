using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    // SText is our own child class of the SFML Sprite class.
    // The advantage of creating an SText instance instead of a Text one is that it is automatically added to the Strings list in Program.
    public class SText : Text
    {
        public Vector2f RealPosition;

        public SText() : base() { }

        public SText(string text, uint size) : base(text, Program.MyFont, size)
        {
            Program.Strings.Add(this);
            RealPosition = Position;
        }

        public SText(string text, float locationX, float locationY, uint size) : this(text, size)
        {
            Position = new Vector2f(locationX, locationY);
        }

        public SText(string text, Vector2f location, uint size) : this(text, location.X, location.Y, size) { }

        // SetPosition changes the position of a sprite and also updates the RealPosition field.
        public void SetPosition(Vector2f position)
        {
            Position = position;
            RealPosition = position;
        }

        public void SetPosition(float positionX, float positionY)
        {
            SetPosition(new Vector2f(positionX, positionY));
        }
    }
}
