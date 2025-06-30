using System.Drawing;

namespace TestManager.Extern.Interface
{
	public interface ILogData
	{
		string Message { get; }

		string Data { get; }

		Image Icon { get; }

		bool IsSaveLog { get; }
	}
}
