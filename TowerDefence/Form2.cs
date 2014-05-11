using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerDefence
{
    public partial class Form2 : Form
    {
        public Form2(string lbl, Cursor curs)
        {
            InitializeComponent();
            label6.Text = lbl;
            this.Cursor = curs;
            button1.Cursor = curs;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
    }
}
