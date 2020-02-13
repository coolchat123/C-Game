using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    // SSprite is onze zelfgemaakte child class van de SFML Sprite class.
    // Het voordeel van een SSprite is dat hij automatisch aan Program's lijst van Sprites wordt toegevoegd.
    // Al onze Controls en Entities zullen child classes zijn van SSprite.
    public class SSprite : Sprite
    {
        public enum Pin : int
        {
            TopLeft = 0,
            TopMiddle = 1,
            TopRight = 2,
            MiddleLeft = 3,
            Middle = 4,
            MiddleRight = 5,
            BottomLeft = 6,
            BottomMiddle = 7,
            BottomRight = 8
        }

        public Vector2f RealPosition;

        public SSprite() : base() { }

        public SSprite(Texture texture) : base(texture)
        {
            Program.Sprites.Add(this);
            RealPosition = Position;
        }

        public SSprite(Texture texture, float locationX, float locationY) : this(texture)
        {
            Position = new Vector2f(locationX, locationY);
        }

        public SSprite(Texture texture, Vector2f location) : this(texture, location.X, location.Y) { }

        // SetScale changes the size of the sprite by a set percentage.
        // An enumerator is provided to define where the sprite will center at.
        public void SetScale(float scale, Pin pin)
        {
            if((int)pin % 3 == 1)
            {
                Position = new Vector2f(Position.X + (Scale.X - scale) * Texture.Size.X / 2, Position.Y);
            }
            else if((int)pin % 3 == 2)
            {
                Position = new Vector2f(Position.X + (Scale.X - scale) * Texture.Size.X, Position.Y);
            }

            if((int)pin >= 3 && (int)pin <= 5)
            {
                Position = new Vector2f(Position.X, Position.Y + (Scale.Y - scale) * Texture.Size.Y / 2);
            }
            else if((int)pin >= 6 && (int)pin <= 8)
            {
                Position = new Vector2f(Position.X, Position.Y + (Scale.Y - scale) * Texture.Size.Y);
            }

            Scale = new Vector2f(scale, scale);
        }

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
