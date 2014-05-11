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
    public abstract class Tower : Entity
    {
        public int Demage { get; set; }
        public int ReloadTime { get; set; }
        public int Range { get; set; }
        public Enemy Target { get; set; }
        public int Cost { get; set; }
        List<Bullet> bullets;
        private int remaingingReload;
        public Tower(int X, int Y, int Demage, int ReloadTime, int Range,int Cost, int Width, int Height, string bitmap)
            : base(X, Y, Width, Height, bitmap)
        {
            this.Demage = Demage;
            this.ReloadTime = ReloadTime;
            this.Range = Range;
            this.remaingingReload = 0;
            this.Cost = Cost;
            bullets = new List<Bullet>();
        }

        //returns a new bullet heading for the specified target
        private Bullet Shoot(Enemy target)
        {
            remaingingReload = ReloadTime;
            target.Fired(this.Demage);

            var bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "Bullet.png");
            return new Bullet(this.X,this.Y,this,this.Target, bitmap: bitmap);
        }

        //finds the nearest enemy and sets it as Target
        private bool ChooseTarget(List<Enemy> enemies)
        {
            double min = Range;
            Enemy currTarget = null;
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.willHeLiveToDieAnotherDay() || enemy.X < 0 || enemy.Passed)
                {
                    continue;
                }

                double dist = Math.Sqrt((enemy.X - this.X) * (enemy.X - this.X) + (enemy.Y - this.Y) * (enemy.Y - this.Y));

                if (dist < min)
                {
                    currTarget = enemy;
                    min = dist;
                }
            }
            Target = currTarget;
            return (currTarget != null);
        }

        //checks if current tower has a target
        private bool hasTarget()
        {   
            return (Target != null );
        }

        //checks if Target is in range
        private bool isInRange()
        {
            return (Math.Sqrt((Target.X - this.X) * (Target.X - this.X) + (Target.Y - this.Y) * (Target.Y - this.Y)) <= Range);
        }

        //returns a list of all bullets fired from this tower
        public List<Bullet> getBullets() 
        {
            return bullets;
        }

        //updates reloadTime and bullet positions
        public void Update(List<Enemy> enemies)
        {
            if(remaingingReload > 0)
            {
                remaingingReload--;
            }

            if (!hasTarget() || !isInRange() || !Target.isAlive() || !Target.willHeLiveToDieAnotherDay() || Target.Passed)
            {
                ChooseTarget(enemies);
            }

            if (remaingingReload == 0 && hasTarget())
            {
                bullets.Add(Shoot(Target));
                remaingingReload = ReloadTime;
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].hit)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Move();
            }
            
        }

        
    }
}
