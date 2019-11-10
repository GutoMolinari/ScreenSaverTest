using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ScreenSaverTest.Forms
{
    public partial class FormScreenSaverScreenShot : Form
    {
        private Rectangle screenBounds;

        private FormScreenSaverScreenShot()
        {
            InitializeComponent();

            this.KeyDown += FormScreenSaverScreenShot_KeyDown;
            this.Load += FormScreenSaverScreenShot_Load;
            this.FormClosing += FormScreenSaverScreenShot_FormClosing;
        }

        public FormScreenSaverScreenShot(Screen screen) : this()
        {
            this.screenBounds = screen.Bounds;
        }

        private void FormScreenSaverScreenShot_Load(object sender, EventArgs e)
        {
            this.pbScreenShot.Image = GetScreenPicture(this.screenBounds);
            this.Location = this.screenBounds.Location;
            this.WindowState = FormWindowState.Maximized;

            if (!Debugger.IsAttached)
            {
                this.TopMost = true;
            }
        }

        private void FormScreenSaverScreenShot_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing ||
                e.CloseReason == CloseReason.TaskManagerClosing)
            {
                e.Cancel = true;
            }
        }

        private void FormScreenSaverScreenShot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control
                && e.KeyCode == Keys.W)
            {
                Application.Exit();
            }
        }

        public Image GetScreenPicture(Rectangle bounds)
        {
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            //bitmap.Save(Environment.CurrentDirectory + @"\test.jpg", ImageFormat.Jpeg);

            return bitmap;
        }
    }
}