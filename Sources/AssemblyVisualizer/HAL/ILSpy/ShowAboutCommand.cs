#if ILSpy

using System;
using System.Windows.Input;
using AssemblyVisualizer.About;
using ICSharpCode.ILSpy;

namespace AssemblyVisualizer.HAL.ILSpy
{
	[ExportMainMenuCommand(Menu = "_Visualizer", Header = "_About", MenuOrder = 1.5)]
	public class ShowAboutCommand : ICommand
	{
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			var aboutWindow = new AboutWindow();
			aboutWindow.Show();
		}
	}
}

#endif