using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    public class Pacman : GameLoop
    {
        public Pacman() : base() { }
        int Life = 2;
        int Score = 0;
        static SSprite Map;


        public override void LoadContent()
        {
            Map = new SSprite(new Texture("Content/Pacman/Map.png"));
        }

        public override void Initialise()
        {
            Map.Position = new Vector2f(120,-25);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
