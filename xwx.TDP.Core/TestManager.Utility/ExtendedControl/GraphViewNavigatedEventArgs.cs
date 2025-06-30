namespace TestManager.Utility.ExtendedControl
{
	public class GraphViewNavigatedEventArgs
	{
		private GraphViewNavigation _navigation;

		public GraphViewNavigation Navigation
		{
			get
			{
				return _navigation;
			}
		}

		public GraphViewNavigatedEventArgs(GraphViewNavigation navigation)
		{
			_navigation = navigation;
		}
	}
}
