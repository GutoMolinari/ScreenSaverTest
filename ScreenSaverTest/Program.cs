using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaverTest
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ShowScreenSaver();
            Application.Run();
        }

        private static void ShowScreenSaver()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                var screenSaver = new FormScreenSaverScreenShot(screen);
                screenSaver.Show();
            }
        }
    }
}
