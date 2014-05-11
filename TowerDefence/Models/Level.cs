using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace TowerDefence
{
    public class Level
    {

        public List<Point> leftLane { get; set; }
        public List<Point> midLane { get; set; } //slow lane
        public List<Point> rightLane { get; set; } // fast lane

        //generates lists of points which will be used by enemies to navigate through the map
        public Level()
        {
            leftLane = new List<Point>();
            midLane = new List<Point>();
            rightLane = new List<Point>();

            for (int i = 0; i < 150; i++)
            {
                //Math
                int x = (i * 10);
                int y = (int)(Math.Sin((double)x / 100) * 130) + 200;
                
                double currA = 1.3 * Math.Cos((double)x/100);
                //More math
                double b = y - currA * x;
                double x2 = x + 100;
                double y2 = (1.3 * Math.Cos((double)x/100) * x2 + b);
                
                double norA = -1 / currA;
                double norB = y - norA * x;

                for (int j = 0; j < 5000; j++)
                {
                    x2 = x + (double)j/100;
                    y2 = norA * x2 + norB;
                    //Once again, math
                    if (Math.Abs(Math.Sqrt(Math.Pow((x2 - x), 2) + Math.Pow((y2 - y), 2))) > 25)
                    {
                        if (norA < 0)
                            leftLane.Add(new Point((int)x2, (int)y2));
                        else
                            rightLane.Add(new Point((int)x2, (int)y2));
                            
                        break;
                    }

                }
                //And last but not least, Math!
                for (int j = 0; j < 5000; j++)
                {
                    x2 = x - (double)j/100;
                    y2 = norA * x2 + norB;

                    if (Math.Abs(Math.Sqrt(Math.Pow((x2 - x), 2) + Math.Pow((y2 - y), 2))) > 25)
                    {
                        if (norA > 0)
                            leftLane.Add(new Point((int)x2, (int)y2));
                        else
                            rightLane.Add(new Point((int)x2, (int)y2));

                        break;
                    }

                }

                midLane.Add(new Point((int)(i * 10), y));
            }
        }
        
    }
}
