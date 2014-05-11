using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerDefence
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Game game;
        HighScores scores;
        Bitmap doubleBuffer;
        Tower tower;
        WMPLib.WindowsMediaPlayer player;
        WMPLib.WindowsMediaPlayer player1;

        public Form1()
        {

            InitializeComponent();

            //changing cursor
            string kursPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "Cursor.png");
            Bitmap kurs = new Bitmap(kursPath);
            kurs.SetResolution(10, 10);
            this.Cursor = CreateCursor(kurs, 0, 0);
            button1.Cursor = this.Cursor;
            button2.Cursor = this.Cursor;
            button3.Cursor = this.Cursor;
            button4.Cursor = this.Cursor;
            button5.Cursor = this.Cursor;
            button6.Cursor = this.Cursor;
            
            //setting up buttons
            button1.Text = "Basic" + Environment.NewLine + "6";
            button2.Text = "Rapid Fire" + Environment.NewLine + "25";
            button3.Text = "Smasher" + Environment.NewLine + "65";
            button4.Text = "Sniper" + Environment.NewLine + "150";
            button5.Text = "Destroyer" + Environment.NewLine + "300";

            //setting up music player
            player = new WMPLib.WindowsMediaPlayer();
            player1 = new WMPLib.WindowsMediaPlayer();
            player.settings.autoStart = true;
            player.settings.setMode("loop", true);
            player.URL = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "ambient.mp3");
            
            tower = null;
            scores = new HighScores();
            
            //setting up graphics
            this.DoubleBuffered = true;
            doubleBuffer = new Bitmap(panel1.Width, panel1.Height);
            graphics = panel1.CreateGraphics();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Show();
            
        }

        //game loop
        private void timer1_Tick(object sender, EventArgs e)
        {
          
            //printing stats
            lives.Text = game.GetLives().ToString();
            score.Text = game.GetScore().ToString();
            coins.Text = game.GetCoins().ToString();
            
            DateTime currTime = DateTime.Now;
            long seconds = (long)currTime.Subtract(game.staringTime).TotalSeconds;
            time.Text = String.Format("{0,2:D2}:{1,2:D2}", seconds / 60  , seconds % 60);
            

            var relativePoint = this.PointToClient(Cursor.Position);

            if (tower != null)
            {
                tower.X = relativePoint.X - 10;
                tower.Y = relativePoint.Y - 10;
                game.drawRangeCircle(relativePoint.X - 10, relativePoint.Y - 10, tower);
                
            }
                
            game.Update();

            //enabling only affordable towers
            if (game.GetCoins() < 6)
                button1.Enabled = false;
            else
                button1.Enabled = true;
            if (game.GetCoins() < 25)
                button2.Enabled = false;
            else
                button2.Enabled = true;
            if (game.GetCoins() < 65)
                button3.Enabled = false;
            else
                button3.Enabled = true;
            if (game.GetCoins() < 150)
                button4.Enabled = false;
            else
                button4.Enabled = true;
            if (game.GetCoins() < 300)
                button5.Enabled = false;
            else
                button5.Enabled = true;


            if (game.GetLives() <= 0 || game.finished)
            {
                timer1.Stop();
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Show();
                button7.Show();
                button8.Show();
                scores.addScore(new Score(game.getName(), game.GetScore()));

                if (game.finished)
                { 
                    timer1.Stop();
                    game.GameFinish();
                }
                else
                    game.EndGame();

            }

        }

        //functionalities of tower choosing buttons
        private void button1_Click(object sender, EventArgs e)
        {
            var bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "BasicTower.png");
            label2.Text = "Click on the map to build the tower or press ESC to cancel.";
            tower = new BasicTower(0, 0, bitmap: bitmap);
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "dead.wav"));
            sound.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "RapidFireTower.png");
            label2.Text = "Click on the map to build the tower or press ESC to cancel.";
            tower = new RapidFireTower(0, 0, bitmap: bitmap);
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "dead.wav"));
            sound.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "SmasherTower.png");
            label2.Text = "Click on the map to build the tower or press ESC to cancel.";
            tower = new SmasherTower(0, 0, bitmap: bitmap);
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "dead.wav"));
            sound.Play();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "SniperTower.png");
            label2.Text = "Click on the map to build the tower or press ESC to cancel.";
            tower = new SniperTower(0, 0, bitmap: bitmap);
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "dead.wav"));
            sound.Play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "DestroyerTower.png");
            label2.Text = "Click on the map to build the tower or press ESC to cancel.";
            tower = new DestroyerTower(0, 0, bitmap: bitmap);
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "dead.wav"));
            sound.Play();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            bool ok = true;
            if(tower != null)
            {
                tower.X = e.X;
                tower.Y = e.Y;
                //attempting to add tower
                ok = game.addTower(tower);
            }
            if (ok)
            {
                if (tower != null)
                {
                    player1.URL = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "build.wav");
                }

                label2.Text = "Choose a tower to build"; 
                tower = null;
            }
            else
                label2.Text = "Cannot build tower there";
        }
        
        //hover effect on buttons
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Cyan;
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "button.wav"));
            sound.Play();
   
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Black;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //canceling your chosen tower
                if (tower != null)
                {
                    label2.Text = "Choose a tower to build";
                    tower = null;
                }
            }
            //pause
            if (e.KeyCode == Keys.P)
            {
                if (timer1.Enabled)
                {
                    tower = null;
                    game.Update();
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;

                    timer1.Stop();
                    label2.Text = "Press P to continue";

                    Image img = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "pause.png"));
                    img = Game.SetImageOpacity(img, 0.7f);
                    graphics.DrawImageUnscaled(img, 0, 0);
                    img = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "pause1.png"));
                    graphics.DrawImage(img, 0, 0);

                }
                else 
                {
                    if (game == null)
                        return;
                    timer1.Start();
                    label2.Text = "Choose a tower to build";
                }
            }
        }
        //startButton
        private void button6_Click(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "start.wav"));
            sound.Play();

            game = new Game(graphics, doubleBuffer);
            timer1.Start();
            button6.Hide();
            button7.Hide();
            button8.Hide();
            label2.Text = "Choose a tower to build";
            txtScores.Text = "";
        }

        //code for creating custom cursor
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            return new Cursor(ptr);
        }

        //hover effect of main menu buttons
        private void button6_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "map11.png"));
            ((Button)sender).ForeColor = Color.Black;
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "button.wav"));
            sound.Play();
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "map.png"));
            ((Button)sender).ForeColor = ColorTranslator.FromHtml("#3399FF");
        }
        
        //exit button
        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //show high scores
        private void button7_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(scores.ToString(),this.Cursor);
            frm.ShowDialog();
        }
    }
}