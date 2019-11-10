using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaverTest
{
    public partial class UpdateScreen : Form
    {
        private readonly Color updateScreenBackgroundColor = Color.FromArgb(59, 110, 165);

        public UpdateScreen()
        {
            InitializeComponent();
            this.BackColor = updateScreenBackgroundColor;
        }
    }
}