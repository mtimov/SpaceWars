using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public abstract class Entity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Bitmap bitmap { get; set; }

        public Entity(int X, int Y, int Width, int Height, string bitmap)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.bitmap = new Bitmap(bitmap);
        }


        public void Draw(Graphics g)
        {
            g.DrawImage(bitmap, X - Width / 2, Y - Height/ 2,Width,Height);
        }

    }
}
