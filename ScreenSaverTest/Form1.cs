using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaverTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.Click += Form1_Click;
            this.KeyDown += Form1_Click;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = GetScreenPicture();

            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }

        }

        public static Image GetScreenPicture()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);

            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            bitmap.Save(Environment.CurrentDirectory + @"\test.jpg", ImageFormat.Jpeg);

            return bitmap;
        }
    }
}
