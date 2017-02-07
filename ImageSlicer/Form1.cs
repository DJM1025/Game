using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageSlicer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            image = new Bitmap(dialog.FileName);
        }

        Bitmap image;

        private void button1_Click(object sender, EventArgs e)
        {
            Int32 x = image.Width/32;
            Int32 y = image.Height/32;
            for(int i = 0; i < y; i++)
            {
                for(int j = 0; j < x; j++)
                {
                    Bitmap next = new Bitmap(32, 32);
                    Graphics g = Graphics.FromImage(next);
                    g.DrawImage(image, 0, 0, new Rectangle(j * 32, i * 32, 32, 32), GraphicsUnit.Pixel);
                    next.Save(textBox1.Text + i.ToString() + j.ToString() + ".png");
                }
            }
        }
    }
}
