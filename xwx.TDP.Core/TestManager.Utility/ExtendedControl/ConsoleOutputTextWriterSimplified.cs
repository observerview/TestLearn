using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TestManager.Utility.ExtendedControl
{
	public class ConsoleOutputTextWriterSimplified : TextWriter
	{
		private delegate void _richTextBoxAppendTextDelegate(string text);

		private delegate void _richTextBoxClearDelegate();

		private delegate void _richTextBoxScrollToCaretDelegate();

		private delegate void _richTextBoxGetTextLengthDelegate(out int length);

		private const string TraceNewLine = "\n";

		private RichTextBox _rtb;

		private bool _isAutoScroll = true;

		private string _indent = string.Empty;

		private int RichTextBoxTextLength
		{
			get
			{
				int length = 0;
				_richTextBoxGetTextLength(out length);
				return length;
			}
		}

		public bool IsAutoScroll
		{
			get
			{
				return _isAutoScroll;
			}
			set
			{
				_isAutoScroll = value;
			}
		}

		public override Encoding Encoding
		{
			get
			{
				return Encoding.Unicode;
			}
		}

		public string Indent
		{
			get
			{
				return _indent;
			}
			set
			{
				_indent = value;
			}
		}

		private void RichTextBoxAppendText(string text)
		{
			_richTextBoxAppendText(text);
		}

		private void RichTextBoxScrollToCaret()
		{
			_richTextBoxScrollToCaret();
		}

		private void RichTextBoxClear()
		{
			_richTextBoxClear();
		}

		private void _richTextBoxAppendText(string text)
		{
			if (!_rtb.InvokeRequired)
			{
				_rtb.AppendText(text);
				_rtb.Update();
			}
			else
			{
				_richTextBoxAppendTextDelegate method = _richTextBoxAppendText;
				_rtb.Invoke(method, text);
			}
		}

		private void _richTextBoxClear()
		{
			if (!_rtb.InvokeRequired)
			{
				_rtb.Clear();
				_rtb.Update();
			}
			else
			{
				_richTextBoxClearDelegate method = _richTextBoxClear;
				_rtb.Invoke(method, new object[0]);
			}
		}

		private void _richTextBoxScrollToCaret()
		{
			if (!_rtb.InvokeRequired)
			{
				_rtb.ScrollToCaret();
				_rtb.Update();
			}
			else
			{
				_richTextBoxScrollToCaretDelegate method = _richTextBoxScrollToCaret;
				_rtb.Invoke(method, new object[0]);
			}
		}

		private void _richTextBoxGetTextLength(out int length)
		{
			if (!_rtb.InvokeRequired)
			{
				length = _rtb.Text.Length;
				return;
			}
			length = 0;
			_richTextBoxGetTextLengthDelegate method = _richTextBoxGetTextLength;
			_rtb.Invoke(method, length);
		}

		public ConsoleOutputTextWriterSimplified(RichTextBox rtb)
		{
			NewLine = "\n";
			_rtb = rtb;
		}

		public override void Write(char value)
		{
			RichTextBoxAppendText(value.ToString(FormatProvider).Replace("\n", "\n" + _indent));
		}

		public override void Write(string format, params object[] arg)
		{
			RichTextBoxAppendText(string.Format(FormatProvider, format, arg).Replace("\n", "\n" + _indent));
		}

		public override void Write(string format, object arg0)
		{
			RichTextBoxAppendText(string.Format(FormatProvider, format, arg0).Replace("\n", "\n" + _indent));
		}

		public override void Write(string format, object arg0, object arg1)
		{
			RichTextBoxAppendText(string.Format(FormatProvider, format, arg0, arg1).Replace("\n", "\n" + _indent));
		}

		public override void Write(string format, object arg0, object arg1, object arg2)
		{
			RichTextBoxAppendText(string.Format(FormatProvider, format, arg0, arg1, arg2).Replace("\n", "\n" + _indent));
		}

		public override void Write(string value)
		{
			RichTextBoxAppendText(value.Replace("\n", "\n" + _indent));
		}

		public override void WriteLine(string format, params object[] arg)
		{
			RichTextBoxAppendText(_indent + string.Format(FormatProvider, format, arg).Replace("\n", "\n" + _indent) + NewLine);
		}

		public override void WriteLine(string format, object arg0)
		{
			RichTextBoxAppendText(_indent + string.Format(FormatProvider, format, arg0).Replace("\n", "\n" + _indent) + NewLine);
		}

		public override void WriteLine(string format, object arg0, object arg1)
		{
			RichTextBoxAppendText(_indent + string.Format(FormatProvider, format, arg0, arg1).Replace("\n", "\n" + _indent) + NewLine);
		}

		public override void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			RichTextBoxAppendText(_indent + string.Format(FormatProvider, format, arg0, arg1, arg2).Replace("\n", "\n" + _indent) + NewLine);
		}

		public override void WriteLine(string value)
		{
			RichTextBoxAppendText(_indent + value.Replace("\n", "\n" + _indent) + NewLine);
		}
	}
}
