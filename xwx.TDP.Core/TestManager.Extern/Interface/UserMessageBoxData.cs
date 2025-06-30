using System.Drawing;
using System.Windows.Forms;

namespace TestManager.Extern.Interface
{
	public class UserMessageBoxData : ILogData
	{
		private string _messageBoxText;

		private string _messageBoxCaption;

		private MessageBoxButtons _messageBoxButtons;

		private MessageBoxIcon _messageBoxIcon;

		public string MessageBoxText
		{
			get
			{
				return _messageBoxText;
			}
		}

		public string MessageBoxCaption
		{
			get
			{
				return _messageBoxCaption;
			}
		}

		public MessageBoxButtons MessageBoxButtons
		{
			get
			{
				return _messageBoxButtons;
			}
		}

		public MessageBoxIcon MessageBoxIcon
		{
			get
			{
				return _messageBoxIcon;
			}
		}

		public string Message
		{
			get
			{
				return string.Empty;
			}
		}

		public string Data
		{
			get
			{
				return string.Empty;
			}
		}

		public Image Icon
		{
			get
			{
				return null;
			}
		}

		public bool IsSaveLog
		{
			get
			{
				return false;
			}
		}

		public UserMessageBoxData(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			_messageBoxText = text;
			_messageBoxCaption = caption;
			_messageBoxButtons = buttons;
			_messageBoxIcon = icon;
		}
	}
}
