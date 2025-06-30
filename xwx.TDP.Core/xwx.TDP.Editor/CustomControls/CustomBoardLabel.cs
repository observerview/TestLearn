using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xwx.TDP.Editor.CustomControls
{
    public partial class CustomBoardLabel : UserControl
    {
        public CustomBoardLabel()
        {
            InitializeComponent();
        }
        public new Font Font
        {
            get
            {
                return this.label.Font;
            }
            set
            {
                this.label.Font = value;
            }
        }
        public new Color ForeColor
        {
            set
            {
                this.label.ForeColor = value;
            }
        }
        public ContentAlignment TextAlign
        {
            set
            {
                this.label.TextAlign = value;
            }
        }
        public new string Text
        {
            get
            {
                return this.label.Text;
            }
            set
            {
                this.label.Text = value;
            }
        }
    }
}
