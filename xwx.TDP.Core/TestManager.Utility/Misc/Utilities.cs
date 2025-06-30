using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestManager.Utility.Misc
{
    public class Utilities
    {
        public static void SetControlFont(Control control, Font font)
        {
            control.Font = font;
            foreach (Control control2 in control.Controls)
            {
                SetControlFont(control2, font);
            }
        }

        public static void SetChildControlFont(Control control, Font font)
        {
            foreach (Control control2 in control.Controls)
            {
                control2.Font = font;
                SetChildControlFont(control2, font);
            }
        }
    }
}
