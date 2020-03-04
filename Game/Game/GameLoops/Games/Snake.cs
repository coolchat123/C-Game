using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;

namespace Game
{
    public class Snake : GameLoop
    {
        static bool[,] Grid;

        public Snake() : base() { }

        public override void LoadContent()
        {
            Grid = new bool[24,20]; 

            for(int i = 0; i < 24; i++){
                for(int j = 0; j < 20; j++){
                    Grid[i,j]= false;    
                }
            } 
        }

        public override void Initialise()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void KeyInput(Keyboard.Key key)
        {
        }
    }
}
