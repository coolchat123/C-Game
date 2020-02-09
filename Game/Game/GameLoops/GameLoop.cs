using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    // The abstract class "GameLoop" provides the basis for our individual games.
    public abstract class GameLoop
    {
        protected GameLoop()
        {
        }

        public abstract void LoadContent();

        public abstract void Initialise();

        public abstract void Update(GameTime gameTime);
    }
}