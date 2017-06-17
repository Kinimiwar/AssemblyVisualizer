// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Reflection;
using System.Windows;
using AssemblyVisualizer.Infrastructure;

namespace AssemblyVisualizer.About
{
	internal partial class AboutWindow : Window
	{
		public AboutWindow()
		{
			InitializeComponent();
			var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			txtVersion.Text = version;

			escapeBinding.Command = new DelegateCommand(Close);
		}

		private void SourcesClickHandler(object sender, RoutedEventArgs e)
		{
			GlobalServices.NavigateToSources();
		}
	}
}