using System;
using System.Windows.Forms;
using TestManager.Utility.ExtendedControl;

namespace TestManager.Utility.ExtendedControl
{
    public class TreeViewEx : TreeView
    {
        private enum TVM : uint
        {
            TV_FIRST = 4352u,
            TVM_SETINSERTMARK = 4378u
        }

        public void SetInsertMarkerAfterNode(TreeNode tn, bool bAfter)
        {
            Win32Api.User32.SendMessage(base.Handle, 4378u, 0, IntPtr.Zero);
            if (tn != null)
            {
                IntPtr handle = tn.Handle;
                Win32Api.User32.SendMessage(base.Handle, 4378u, bAfter ? 1 : 0, handle);
            }
        }
    }
}
