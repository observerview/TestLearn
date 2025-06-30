using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TestManager.Utility.ExtendedControl
{
	public class ConsoleOutputTextWriter : TextWriter
	{
		private delegate void _richTextBoxAppendTextDelegate(string text);

		private delegate void _richTextBoxClearDelegate();

		private delegate void _richTextBoxScrollToCaretDelegate();

		private delegate void _richTextBoxGetTextLengthDelegate(out int length);

		private const char TraceNewLine = '\n';

		private RichTextBox _rtb;

		private bool _isPause;

		private bool _isLockView;

		private StringBuilder _sbBuffer = new StringBuilder();

		private bool _isAutoScroll = true;

		private bool _isSaveToFile = true;

		private string _fileName = string.Empty;

		private StreamWriter _sw;

		private bool _isClearRichTextBoxBeforeBeginWriteFile;

		private int RichTextBoxTextLength
		{
			get
			{
				int length = 0;
				_richTextBoxGetTextLength(out length);
				return length;
			}
		}

		public bool IsSaveToFile
		{
			get
			{
				return _isSaveToFile;
			}
			set
			{
				_isSaveToFile = value;
			}
		}

		public string SaveFileName
		{
			get
			{
				return _fileName;
			}
			set
			{
				_fileName = value;
			}
		}

		public bool IsClearRichTextBoxBeforeBeginWriteFile
		{
			get
			{
				return _isClearRichTextBoxBeforeBeginWriteFile;
			}
			set
			{
				_isClearRichTextBoxBeforeBeginWriteFile = value;
			}
		}

		public bool IsPause
		{
			get
			{
				return _isPause;
			}
			set
			{
				_isPause = value;
			}
		}

		public bool IsLockView
		{
			get
			{
				return _isLockView;
			}
			set
			{
				if (!value)
				{
					RichTextBoxAppendText(_sbBuffer.ToString());
				}
				_sbBuffer = new StringBuilder();
				_isLockView = value;
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

		public ConsoleOutputTextWriter(RichTextBox rtb)
		{
			NewLine = '\n'.ToString();
			_rtb = rtb;
		}

		public override void Flush()
		{
			RichTextBoxAppendText(_sbBuffer.ToString());
			_sbBuffer = new StringBuilder();
		}

		public override void Write(char value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.ToString(FormatProvider));
			if (_isLockView)
			{
				if (!_isPause)
				{
					_sbBuffer.Append(stringBuilder.ToString());
				}
			}
			else if (!_isPause && _rtb != null)
			{
				RichTextBoxAppendText(stringBuilder.ToString());
				if (value == '\n' && _isAutoScroll)
				{
					RichTextBoxScrollToCaret();
				}
			}
			if (_sw != null)
			{
				_sw.Write(stringBuilder.ToString());
			}
		}

		public void BeginWriteFile()
		{
			if (_isClearRichTextBoxBeforeBeginWriteFile)
			{
				RichTextBoxClear();
				_sbBuffer = new StringBuilder();
			}
			if (_isSaveToFile)
			{
				EndWriteFile();
				try
				{
					_sw = File.CreateText(_fileName);
				}
				catch
				{
				}
			}
		}

		public void EndWriteFile()
		{
			if (_sw != null)
			{
				_sw.Flush();
				_sw.Close();
				_sw = null;
			}
		}
	}
}
