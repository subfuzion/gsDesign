using System;
using System.Windows;
using System.Windows.Controls;

namespace gsDesignExplorer
{
	using ViewModel;

	public partial class MainPage : UserControl
	{
		private DesignExplorerViewModel ViewModel { get; set; }

		public MainPage()
		{
			InitializeComponent();
			Loaded += MainPage_Loaded;
		}

		void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel = (DesignExplorerViewModel) Resources["ViewModel"];
		}

		private void runButton_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.RunDesign();
		}
	}
}
