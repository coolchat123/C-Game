using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Game
{
    // SSprite is onze zelfgemaakte child class van de SFML Sprite class.
    // Het voordeel van een SSprite is dat hij automatisch aan Program's lijst van Sprites wordt toegevoegd.
    // Al onze Controls en Entities zullen child classes zijn van SSprite.
    public class SSprite : Sprite
    {
        public SSprite() : base() { }

        public SSprite(Texture texture) : base(texture)
        {
            Program.Sprites.Add(this);
        }
    }
}
