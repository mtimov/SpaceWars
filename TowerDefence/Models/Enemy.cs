using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public abstract class Enemy : Entity, Movable
    {
        public int Health { get; set; }
        public int Speed { get; set; }
        public  int checkpoint { get; set; }
        public Point target { get; set; }
        public int Reward { get; set; }
        public bool Passed;
        private int maxHealth;
        private int maxWidth;
        private int maxHeight;
        private int currHealth;
        public Enemy(int X, int Y, int Health, int Speed ,int Reward, int Width, int Height, string bitmap)
            : base(X, Y, Width, Height, bitmap)
        {
            this.Health = Health;
            this.Speed = Speed;
            this.Reward = Reward;
            this.target = new Point(X, Y);
            this.maxHealth = Health;
            this.maxHeight = Height;
            this.maxWidth = Width;
            this.currHealth = Health;
            this.checkpoint = 1;
            this.Passed = false;
        }


        //moves enemy towards the target
        //distance of move depends on Speed
        public void Move()
        {
            double dist = Math.Sqrt( (this.X - target.X) * (this.X - target.X) + (this.Y - target.Y) * (this.Y - target.Y));
            if (dist < Speed)
            {
                this.X = target.X;
                this.Y = target.Y;
                nextTarget();
                return;
            }
            
            double odnos = Speed / dist;

            this.X += (int)((target.X - this.X) * odnos);
            this.Y += (int)((target.Y - this.Y) * odnos);
           
        }
        //moving to next checkpoing in lane
        public void nextTarget()
        {
            checkpoint++;
        }
        //enemy is dead and player gets reward
        public int getReward()
        {
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "enemyKill.wav"));
            sound.Play();
            return Reward;
        }
        //enemy has reached the end of the path 
        public void ReachedGoal()
        {
            Passed = true;
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "dead.wav"));
            sound.Play();

        }
        //enemy is hit
        public void Hit(int dmg)
        {
            this.Health -= dmg;
            //decreases size
            this.Width = this.maxWidth / 2 + (int)(this.maxWidth/2 * ((double)Health/maxHealth));
            this.Height = this.maxHeight/ 2 + (int)(this.maxHeight/ 2 * ((double)Health / maxHealth));
            if (this.Width < 0)
            {
                this.Width = 0;
                this.Height = 0;
            }
        }

        //enemy is fired at
        public void Fired(int dmg)
        {
            currHealth -= dmg;
        }
        //will the bullets currently fired at this enemy be enough to kill it
        public bool willHeLiveToDieAnotherDay()
        {
            return (currHealth > 0);
        }
        public bool isAlive()
        {
            return (Health > 0);
        }
    }
}
