using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    public class PacmCharacter : SSprite
    {
        //0 = none; 1 = left; 2 = right; 3 = top; 4 = down; 5 = bg screen gone;
        int Direction = 0;
        
        public PacmCharacter(Texture texture) : base(texture)
        {
          
        }

        public void ChangeDirection(int direction)
        {
            Direction = direction;
        }

        public int GetDirection()
        {
            return Direction;
        }
    }
}
