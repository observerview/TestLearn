using System.Drawing;
using System.Windows.Forms;

namespace TestManager.Extern.Interface
{
	public class UserFormLogData : ILogData
	{
		private Form _loggedForm;

		private bool _isShow;

		public Form LoggedForm
		{
			get
			{
				return _loggedForm;
			}
		}

		public bool IsShow
		{
			get
			{
				return _isShow;
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

		public UserFormLogData(Form loggedForm, bool isShow)
		{
			_loggedForm = loggedForm;
			_isShow = isShow;
		}
	}
}
