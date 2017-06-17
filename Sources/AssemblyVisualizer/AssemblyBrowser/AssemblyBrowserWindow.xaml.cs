// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AssemblyVisualizer.AssemblyBrowser.ViewModels;
using AssemblyVisualizer.HAL;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.AssemblyBrowser
{
	/// <summary>
	///     Interaction logic for AssemblyBrowserWindow.xaml
	/// </summary>
	internal partial class AssemblyBrowserWindow : Window
	{
		public AssemblyBrowserWindow(IEnumerable<AssemblyInfo> assemblies)
			: this(assemblies, null)
		{
		}

		public AssemblyBrowserWindow(IEnumerable<AssemblyInfo> assemblies, TypeInfo typeInfo)
		{
			InitializeComponent();

			ViewModel = new AssemblyBrowserWindowViewModel(assemblies, typeInfo, Dispatcher);

			CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseForward,
				(s, e) => ViewModel.NavigateForwardCommand.Execute(null)));
			CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseBack,
				(s, e) => ViewModel.NavigateBackCommand.Execute(null)));

			WindowManager.AddAssemblyBrowser(this);
		}

		public AssemblyBrowserWindowViewModel ViewModel
		{
			get { return DataContext as AssemblyBrowserWindowViewModel; }
			set { DataContext = value; }
		}

		private void WindowDrop(object sender, DragEventArgs e)
		{
#if ILSpy

			var assemblyFilePaths = e.Data.GetData("ILSpyAssemblies") as string[];
			foreach (var assemblyFilePath in assemblyFilePaths)
			{
				var loadedAssembly =
					Services.MainWindow.CurrentAssemblyList.OpenAssembly(assemblyFilePath);

				ViewModel.AddAssembly(Converter.Assembly(loadedAssembly.AssemblyDefinition));
			}

#endif
		}

		private void SearchExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			ViewModel.ShowSearch();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			WindowManager.RemoveAssemblyBrowser(this);
		}
	}
}