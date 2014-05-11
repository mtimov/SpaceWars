using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class Bullet : Entity, Movable
    {
        public Tower Shooter { get; set; }
        public Enemy target { get; set; }
        public int Speed { get; set; }
        public bool hit;
        public Bullet(int X, int Y, Tower Shooter, Enemy Target, int Widht = 10, int Height = 10, string bitmap = "")
            : base(X, Y, Widht, Height, bitmap)
        {
            this.Shooter = Shooter;
            this.target = Target;
            hit = false;
            Speed = 8;
        }

        public void Move()
        {

            double dist = Math.Sqrt((this.X - target.X) * (this.X - target.X) + (this.Y - target.Y) * (this.Y - target.Y));
            if (dist < Speed)
            {
                this.X = target.X;
                this.Y = target.Y;
                target.Hit(Shooter.Demage);
                hit = true;
                return;
            }
            double odnos = Speed / dist;

            this.X += (int)((target.X - this.X) * odnos);
            this.Y += (int)((target.Y - this.Y) * odnos);

        }

    }
}
