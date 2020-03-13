using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Ghost : SSprite
    {
        public Vector2f[] MoveList;

        public bool Jailed;

        public Ghost() { }

        public Ghost(Color color) : base(color, 16, 12)
        {
            MoveList = new Vector2f[5];
            Jailed = true;
        }

        public void Move()
        {

        }

        public void LeaveJail()
        {

        }
    }
}
