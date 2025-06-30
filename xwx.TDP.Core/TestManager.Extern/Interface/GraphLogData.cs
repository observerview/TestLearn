using System.Drawing;
using TestManager.Extern.Properties;
using TestManager.Utility.ExtendedControl;
using ZedGraph;

namespace TestManager.Extern.Interface
{
	public class GraphLogData : ILogData
	{
		private Image _image;

		private MasterPane _masterPane;

		private string _fileName = string.Empty;

		private bool _isSaveToFile;

		private string _graphTitle = string.Empty;

		private GraphViewType _graphType;

		public MasterPane MasterPane
		{
			get
			{
				return _masterPane;
			}
		}

		public string FileName
		{
			get
			{
				return _fileName;
			}
		}

		public bool IsSaveToFile
		{
			get
			{
				return _isSaveToFile;
			}
		}

		public string GraphTitle
		{
			get
			{
				return _graphTitle;
			}
		}

		public GraphViewType GraphType
		{
			get
			{
				return _graphType;
			}
		}

		public Image Image
		{
			get
			{
				return _image;
			}
		}

		public Image Icon
		{
			get
			{
				return Resources.Graph;
			}
		}

		public string Data
		{
			get
			{
				return _fileName;
			}
		}

		public string Message
		{
			get
			{
				return _graphTitle;
			}
		}

		public bool IsSaveLog
		{
			get
			{
				return _isSaveToFile;
			}
		}

		public GraphLogData(MasterPane masterPane, string graphTitle, string fileName, bool isSaveToFile)
		{
			_graphType = GraphViewType.ZedGraph;
			_masterPane = masterPane;
			_graphTitle = graphTitle;
			_fileName = fileName;
			_isSaveToFile = isSaveToFile;
		}

		public GraphLogData(MasterPane masterPane, string graphTitle)
			: this(masterPane, graphTitle, string.Empty, false)
		{
		}

		public GraphLogData(Image image, string graphTitle, string fileName, bool isSaveToFile)
		{
			_graphType = GraphViewType.Image;
			_image = image;
			_graphTitle = graphTitle;
			_fileName = fileName;
			_isSaveToFile = isSaveToFile;
		}

		public GraphLogData(Image image, string graphTitle)
			: this(image, graphTitle, string.Empty, false)
		{
		}

		public GraphLogData()
		{
		}
	}
}
