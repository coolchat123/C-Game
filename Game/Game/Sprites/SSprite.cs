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
    }
}
