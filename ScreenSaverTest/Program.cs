using System;
using System.Linq;
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

            if (args.Any())
            {
                string firstArgument = args[0].ToLower().Trim();
                string secondArgument = string.Empty;

                // Handle cases where arguments are separated by colon.
                // Examples: /c:1234567 or /P:1234567
                if (firstArgument.Length > 2)
                {
                    secondArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }

                switch (firstArgument)
                {
                    // Configuration mode
                    case "/c":
                        MessageBox.Show($"Mode not implemented yet: Configuration ({firstArgument}).");
                        break;

                    // Preview mode
                    case "/p":
                        MessageBox.Show($"Mode not implemented yet: Preview ({firstArgument}).");
                        break;

                    // Fullscreen mode
                    case "/s":
                        ShowScreenSaver();
                        Application.Run();
                        break;

                    default:
                        MessageBox.Show($"Invalid argument: {firstArgument}.");
                        break;
                }
            }
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