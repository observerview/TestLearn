using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xwx.TDP.Editor.Misc
{
    public partial class SplashWindow : Form
    {
        public SplashWindow()
        {
            InitializeComponent();
        }
        public string InfoText
        {
            set
            {
                this.label_Info.Text = value;
                this.label_Info.Update();
            }
        }
        public string VersionText
        {
            set
            {
                this.label_Version.Text = string.Format("Version: {0}", value);
                this.label_Version.Update();
            }
        }
        public string SplashText
        {
            set
            {
                this.label_Status.Text = value;
                this.label_Status.Update();
            }
        }
    }
}
