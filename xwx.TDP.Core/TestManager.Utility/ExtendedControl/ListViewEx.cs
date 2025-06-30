using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestManager.Utility.ExtendedControl
{
    public class ListViewEx : ListView
    {
        public ListViewEx()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
    }
}
