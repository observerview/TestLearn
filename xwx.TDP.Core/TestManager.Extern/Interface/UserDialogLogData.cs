using System.Drawing;
using System.Windows.Forms;

namespace TestManager.Extern.Interface
{
	public class UserDialogLogData : ILogData
	{
		private Form _loggedDialog;

		private Form _onwerForm;

		public Form LoggedDialog
		{
			get
			{
				return _loggedDialog;
			}
		}

		public Form OnwerForm
		{
			get
			{
				return _onwerForm;
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

		public UserDialogLogData(Form loggedDialog, Form onwerForm)
		{
			_loggedDialog = loggedDialog;
			_onwerForm = onwerForm;
		}
	}
}
