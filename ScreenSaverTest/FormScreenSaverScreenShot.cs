using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaverTest
{
    public partial class FormScreenSaverScreenShot : Form
    {
        public FormScreenSaverScreenShot()
        {
            InitializeComponent();
            this.KeyDown += Form1_KeyDown;
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pbScreenShot.Image = GetScreenPicture();
            WindowState = FormWindowState.Maximized;
            KeyboardHandler.CreateHookKeyboard();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing ||
                e.CloseReason == CloseReason.TaskManagerClosing)
            {
                e.Cancel = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control
                && e.KeyCode == Keys.W)
            {
                Application.Exit();
            }
        }

        public static Image GetScreenPicture()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);

            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            //bitmap.Save(Environment.CurrentDirectory + @"\test.jpg", ImageFormat.Jpeg);

            return bitmap;
        }
    }
}
