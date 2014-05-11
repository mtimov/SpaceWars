using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerDefence
{
    public class Game
    {
        Level level;
        List<Tower> towers;
        List<Enemy> enemies;
        List<Enemy> fastEnemies;
        List<Wave> waves;
        Player player;
        public DateTime staringTime;
        private Graphics g;
        private Bitmap doubleBuffer;
        private Tower towerToBuild;
        private RangeCircle rc;
        public Boolean finished;
        private WMPLib.WindowsMediaPlayer mediaPlayer;

        private int currWave;
        public Game(Graphics g, Bitmap doubleBuffer)
        {
            this.g = g;
            this.doubleBuffer = doubleBuffer;
            rc = null;
            finished = false;
            level = new Level();
            enemies = new List<Enemy>();
            fastEnemies = new List<Enemy>();
            towers = new List<Tower>();
            waves = new List<Wave>();
            player = new Player("Name");
            mediaPlayer = new WMPLib.WindowsMediaPlayer();
            initWaves();
            currWave = 0;
            staringTime = DateTime.Now;
            StartWave();
        }

        //initializing the waves of enemies
        private void initWaves()
        {
            waves.Add(new Wave(3, 0, 0, 0));
            waves.Add(new Wave(20, 0, 0,1));
            waves.Add(new Wave(30, 0, 0,3));
            waves.Add(new Wave(20, 10, 0,4));
            waves.Add(new Wave(15, 15, 0,5));
            waves.Add(new Wave(20, 20, 0,8));
            waves.Add(new Wave(5, 0, 1,7));
            waves.Add(new Wave(10, 30, 0,12));
            waves.Add(new Wave(20, 30, 2,12));
            waves.Add(new Wave(30, 50, 4,15));
            waves.Add(new Wave(10, 20, 10,18));
            waves.Add(new Wave(0, 0, 5,75)); 
            }

        //starts the current wave
        private void StartWave()
        {
            if(currWave == waves.Count)
            {
                
                GameFinish();
                return;
            }
            enemies = waves[currWave].getSlowEnemies();
            fastEnemies = waves[currWave].getFastEnemies();
            currWave++;
            Draw();
        }

       
        //player completed the game
        public void GameFinish()
        {
            Image img = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "pause.png"));
            img = Game.SetImageOpacity(img, 0.7f);
            g.DrawImageUnscaled(img, 0, 0);
            //img = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "victory.png"));
           // g.DrawImage(img, 0, 0);
            mediaPlayer.URL = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "victory.wav");
            //SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "victory.wav"));
            //sound.Play();
            
            playerName();
  
        }

        private void playerName()
        {
            if (finished)
                return;
            finished = true;
            Form3 frm = new Form3();
            frm.ShowDialog();
            player.Name = frm.getName();
            Console.WriteLine(player.Name);
            frm.Close();

        }

        //player lost the game
        public void EndGame()
        {
            Image img = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "pause.png"));
            img = Game.SetImageOpacity(img, 0.7f);
            g.DrawImageUnscaled(img, 0, 0);
            img = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "gameOver.png"));
            g.DrawImage(img, 0, 0);
            mediaPlayer.URL = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "gameOver.wav");
            //SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "gameOver.wav"));
            //sound.Play();
            playerName();
            
        }

        //checks if position is valid and adds tower
        public bool addTower(Tower tower)
        {
            foreach (Tower t in towers)
            {
                if( Math.Sqrt((t.X - tower.X)*(t.X - tower.X) + (t.Y - tower.Y)*(t.Y - tower.Y) ) < tower.Width) 
                {
                    return false;
                }
            }

            foreach (Point t in level.midLane)
            {
                if( Math.Sqrt((t.X - tower.X)*(t.X - tower.X) + (t.Y - tower.Y)*(t.Y - tower.Y) ) < tower.Width) 
                {
                    return false;
                }
            }
            foreach (Point t in level.rightLane)
            {
                if (Math.Sqrt((t.X - tower.X) * (t.X - tower.X) + (t.Y - tower.Y) * (t.Y - tower.Y)) < tower.Width)
                {
                    return false;
                }
            }

            player.BuiltTower(tower.Cost);
            towers.Add(tower);
            return true;
        }


        public void Update() 
        {
            if(enemies.Count == 0 && fastEnemies.Count == 0 && !finished)
            {
                StartWave();
            }

            //updating position of enemies
            foreach (Enemy en in enemies)
            {
                if (en.checkpoint >= 95)
                {
                    player.LostLife();
                    en.ReachedGoal();
                    continue;
                }
                en.target = level.midLane[en.checkpoint];
                en.Move();
            }

            foreach (Enemy en in fastEnemies)
            {
                if (en.checkpoint >= 95)
                {
                    player.LostLife();
                    en.ReachedGoal();
                    continue;
                }
                en.target = level.rightLane[en.checkpoint];
                en.Move();
            }


            //removing dead enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].isAlive() || enemies[i].Passed)
                {
                    if(!enemies[i].Passed)
                        player.GotKill(enemies[i].getReward());

                    enemies.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < fastEnemies.Count; i++)
            {
                if (!fastEnemies[i].isAlive() || fastEnemies[i].Passed)
                {
                    if (!fastEnemies[i].Passed)
                    {
                        player.GotKill(fastEnemies[i].getReward());
                    }
                    fastEnemies.RemoveAt(i);
                    i--;
                }
            }

            List<Enemy> allEnemies = new List<Enemy>();

            foreach (Enemy enemy in enemies)
            {
                allEnemies.Add(enemy);
            }
            foreach (Enemy enemy in fastEnemies)
            {
                allEnemies.Add(enemy);
            }
                
            //updating towers
            foreach (Tower tower in towers)
            {
                tower.Update(allEnemies);
            }


            Draw();
        }

        public void Draw()
        {

            Graphics bufferedGraphics = Graphics.FromImage(doubleBuffer);
            string mapa = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "map.png");
            Bitmap map = new Bitmap(mapa);
            bufferedGraphics.Clear(Color.Black);
            bufferedGraphics.DrawImageUnscaled(map, 0, 0);
            

            foreach (Enemy en in enemies)
            {
                en.Draw(bufferedGraphics);
            }
            foreach (Enemy en in fastEnemies)
            {
                en.Draw(bufferedGraphics);
            }
            foreach (Tower tower in towers)
            {
                tower.Draw(bufferedGraphics);
                foreach (Bullet bullet in tower.getBullets())
                {
                    bullet.Draw(bufferedGraphics);
                }
            }
            if (rc!=null)
            {
                Image a = SetImageOpacity((Image)towerToBuild.bitmap, 0.5f);
                bufferedGraphics.DrawImage(a, towerToBuild.X - towerToBuild.Width / 2 - 5, towerToBuild.Y - towerToBuild.Height / 2 - 5, towerToBuild.Width, towerToBuild.Height);
                rc.Draw(bufferedGraphics);
                rc = null;
                towerToBuild = null;
            }

            g.DrawImageUnscaled(doubleBuffer, 0, 0);
        }
        public void drawRangeCircle(int X, int Y, Tower tower)
        {
            var bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "rangeCircle.png");
            rc = new RangeCircle(X - 5, Y - 5, tower.Range * 2, tower.Range * 2, bitmap);
            this.towerToBuild = tower;
        }

        public static Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        } 

        public int GetScore()
        {
            return player.Score;
        }

        public int GetLives()
        {
            return player.Lives;
        }
        public int GetCoins()
        {
            return player.Coins;
        }
        public string getName()
        {
            return player.Name;
        }
    }
}
