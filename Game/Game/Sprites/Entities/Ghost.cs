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

        // 0 = W; 1 = A; 2 = S; 3 = D;
        public int Direction;

        public Ghost() { }

        public Ghost(Color color) : base(color, 16, 12)
        {
            MoveList = new Vector2f[5];
            Jailed = true;
            Direction = 3;
        }

        public void Move()
        {
            if (Jailed)
            {
                Random rand = new Random();

                // Every once in a while change direction
                if(rand.Next() % 30 == 0)
                {
                    Direction = rand.Next() % 4;
                }
            }

            if (Direction == 0)
            {
                if (Pacman.CheckCollision(this.Position.X - Pacman.Map.Position.X, this.Position.Y - 1f - Pacman.Map.Position.Y,
                    this.Position.X + this.Texture.Size.X - Pacman.Map.Position.X, this.Position.Y - Pacman.Map.Position.Y))
                {
                    this.SetPosition(this.Position.X, this.Position.Y - 1f);
                }
            }
            else if (Direction == 1)
            {
                if (Pacman.CheckCollision(this.Position.X - 1f - Pacman.Map.Position.X, this.Position.Y - Pacman.Map.Position.Y,
                   this.Position.X - Pacman.Map.Position.X, this.Position.Y + this.Texture.Size.Y - Pacman.Map.Position.Y))
                {
                    this.SetPosition(this.Position.X - 1f, this.Position.Y);
                }
            }
            else if (Direction == 2)
            {
                if (Pacman.CheckCollision(this.Position.X - Pacman.Map.Position.X, this.Position.Y + this.Texture.Size.Y - Pacman.Map.Position.Y,
                    this.Position.X + this.Texture.Size.X - Pacman.Map.Position.X, this.Position.Y + this.Texture.Size.Y + 1f - Pacman.Map.Position.Y))
                {
                    this.SetPosition(this.Position.X, this.Position.Y + 1f);
                }
            }
            else if (Direction == 3)
            {
                if (Pacman.CheckCollision(this.Position.X + this.Texture.Size.X - Pacman.Map.Position.X, this.Position.Y - Pacman.Map.Position.Y,
                 this.Position.X + this.Texture.Size.X + 1f - Pacman.Map.Position.X, this.Position.Y + this.Texture.Size.Y - Pacman.Map.Position.Y))
                {
                    this.SetPosition(this.Position.X + 1f, this.Position.Y);

                }
            }
        }

        public void LeaveJail()
        {
            Jailed = false;
        }
    }
}
