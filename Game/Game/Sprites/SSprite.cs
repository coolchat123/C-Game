﻿using System;
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
        public string[] Groups = new string[2];
        
        public static Texture[] ParseSpritesheet(Image image, int amount, int width, int padding)
        {
            Texture[] result = new Texture[amount];

            for(int i = 0; i < amount; i++)
            {
                result[i] = new Texture(image, new IntRect(i * (width + padding), 0, width, (int)image.Size.Y));
            }

            return result;
        }

        public static SSprite[] DrawSpritesheet(Texture[] spritesheet)
        {
            SSprite[] result = new SSprite[spritesheet.Length];

            for(int i = 0; i < spritesheet.Length; i++)
            {
                result[i] = new SSprite(spritesheet[i]);
                result[i].SetPosition(i * (5 + result[i].Texture.Size.X), 0);
            }

            return result;
        }

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

        public SSprite(Color color, uint sizeX, uint sizeY, string group1 = null, string group2 = null) : base()
        {
            Program.Sprites.Add(this);
            Groups[0] = group1;
            Groups[1] = group2;

            Image image = new Image(sizeX, sizeY);

            for(uint i = 0; i < sizeX; i++)
            {
                for(uint j = 0; j < sizeY; j++)
                {
                    image.SetPixel(i, j, color);
                }
            }

            this.Texture = new Texture(image);
        }

        public SSprite(Texture texture, string group1 = null, string group2 = null) : base(texture)
        {
            Program.Sprites.Add(this);
            RealPosition = Position;
            Groups[0] = group1;
            Groups[1] = group2;
        }

        public SSprite(Texture texture, float locationX, float locationY, string group1 = null, string group2 = null) : this(texture, group1, group2)
        {
            Position = new Vector2f(locationX, locationY);
        }

        public SSprite(Texture texture, Vector2f location, string group1 = null, string group2 = null) : this(texture, location.X, location.Y, group1, group2) { }

        // SetScale changes the size of the sprite by a set percentage.
        // An enumerator is provided to define where the sprite will center at.
        public void SetScale(float scale, Pin pin)
        {
            if((int)pin % 3 == 1)
            {
                //    RealPosition = new Vector2f(RealPosition.X + (Scale.X - scale) * Texture.Size.X / 2, RealPosition.Y);
                SetPosition(new Vector2f(RealPosition.X + (Scale.X - scale) * Texture.Size.X / 2, RealPosition.Y));
            }
            else if((int)pin % 3 == 2)
            {
            //    RealPosition = new Vector2f(RealPosition.X + (Scale.X - scale) * Texture.Size.X, RealPosition.Y);
                SetPosition(new Vector2f(RealPosition.X + (Scale.X - scale) * Texture.Size.X, RealPosition.Y));
            }

            if((int)pin >= 3 && (int)pin <= 5)
            {
                //    RealPosition = new Vector2f(RealPosition.X, RealPosition.Y + (Scale.Y - scale) * Texture.Size.Y / 2);
                SetPosition(new Vector2f(RealPosition.X, RealPosition.Y + (Scale.Y - scale) * Texture.Size.Y / 2));
            }
            else if((int)pin >= 6 && (int)pin <= 8)
            {
                //    RealPosition = new Vector2f(RealPosition.X, RealPosition.Y + (Scale.Y - scale) * Texture.Size.Y);
                SetPosition(new Vector2f(RealPosition.X, RealPosition.Y + (Scale.Y - scale) * Texture.Size.Y));
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
