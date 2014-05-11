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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string getName()
        {
            return textBox1.Text;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "map11.png"));
            ((Button)sender).ForeColor = Color.Black;
            SoundPlayer sound = new SoundPlayer(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Sounds\" + "button.wav"));
            sound.Play();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = Image.FromFile(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "map.png"));
            ((Button)sender).ForeColor = ColorTranslator.FromHtml("#3399FF");
        }
    }
}
