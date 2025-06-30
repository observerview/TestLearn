using System.Drawing;
using System.Windows.Forms;

namespace TestManager.Utility.GenericForm
{
	public class MessageBoxEx
	{
		private delegate DialogResult _showDialogDelegate(Form shownForm, Form owner);

		public static DialogResult Show(string text)
		{
			return ShowCore(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static DialogResult Show(IWin32Window owner, string text)
		{
			return ShowCore(owner, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static DialogResult Show(string text, string caption)
		{
			return ShowCore(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption)
		{
			return ShowCore(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			return ShowCore(null, text, caption, buttons, MessageBoxIcon.Asterisk);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
		{
			return ShowCore(owner, text, caption, buttons, MessageBoxIcon.Asterisk);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return ShowCore(null, text, caption, buttons, icon);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return ShowCore(owner, text, caption, buttons, icon);
		}

		public static DialogResult ShowPrompt(string text, string caption, ref string promptText)
		{
			return ShowPromptCore(null, text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, ref promptText);
		}

		public static DialogResult ShowPrompt(IWin32Window owner, string text, string caption, ref string promptText)
		{
			return ShowPromptCore(owner, text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, ref promptText);
		}

		private static DialogResult ShowPromptCore(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, ref string promptText)
		{
			MessageBoxExCore messageBoxExCore = new MessageBoxExCore();
			messageBoxExCore.MessageTitle = caption;
			messageBoxExCore.MessageText = text;
			messageBoxExCore.MessageIcon = icon;
			messageBoxExCore.Buttons = buttons;
			messageBoxExCore.PromptText = promptText;
			if (owner == null)
			{
				messageBoxExCore.ShowDialog();
			}
			else if (owner is Form)
			{
				ShowDialog(messageBoxExCore, owner as Form);
			}
			else
			{
				messageBoxExCore.ShowDialog(owner);
			}
			promptText = messageBoxExCore.PromptText;
			return messageBoxExCore.DialogResult;
		}

		private static DialogResult ShowCore(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			MessageBoxExCore messageBoxExCore = new MessageBoxExCore();
			messageBoxExCore.MessageTitle = caption;
			messageBoxExCore.MessageText = text;
			messageBoxExCore.Buttons = buttons;
			messageBoxExCore.MessageIcon = icon;
			if (owner == null)
			{
				messageBoxExCore.ShowDialog();
			}
			else if (owner is Form)
			{
				ShowDialog(messageBoxExCore, owner as Form);
			}
			else
			{
				messageBoxExCore.ShowDialog(owner);
			}
			return messageBoxExCore.DialogResult;
		}

		public static DialogResult ShowDialog(Form shownForm, Form owner)
		{
			if (owner == null)
			{
				return shownForm.ShowDialog();
			}
			if (!owner.InvokeRequired)
			{
				shownForm.StartPosition = FormStartPosition.Manual;
				shownForm.Location = ValidateChildFormLocation(owner, shownForm.Size, owner.PointToScreen(new Point((owner.Width - shownForm.Width) / 2, (owner.Height - shownForm.Height) / 3)));
				return shownForm.ShowDialog(owner);
			}
			_showDialogDelegate method = ShowDialog;
			return (DialogResult)owner.Invoke(method, shownForm, owner);
		}

		public static Point ValidateChildFormLocation(Form ownerForm, Size childFormSize, Point childFormOrignalLocation)
		{
			int num = childFormOrignalLocation.X;
			int num2 = childFormOrignalLocation.Y;
			if (ownerForm.IsMdiContainer)
			{
				if (num + childFormSize.Width + 10 > ownerForm.Location.X + ownerForm.Width)
				{
					num = ownerForm.Location.X + ownerForm.Width - childFormSize.Width - 10;
				}
				if (num2 + childFormSize.Height + 10 > ownerForm.Location.Y + ownerForm.Height)
				{
					num2 = ownerForm.Location.Y + ownerForm.Height - childFormSize.Height - 10;
				}
				if (num < ownerForm.Location.X + 10)
				{
					num = ownerForm.Location.X + 10;
				}
				if (num2 < ownerForm.Location.Y + 10)
				{
					num2 = ownerForm.Location.Y + 10;
				}
			}
			if (num + childFormSize.Width > SystemInformation.PrimaryMonitorSize.Width - 10)
			{
				num = SystemInformation.PrimaryMonitorSize.Width - childFormSize.Width - 10;
			}
			if (num2 + childFormSize.Height > SystemInformation.PrimaryMonitorSize.Height - 10)
			{
				num2 = SystemInformation.PrimaryMonitorSize.Height - childFormSize.Height - 10;
			}
			if (num < 20)
			{
				num = 20;
			}
			if (num2 < 20)
			{
				num2 = 20;
			}
			return new Point(num, num2);
		}
	}
}
