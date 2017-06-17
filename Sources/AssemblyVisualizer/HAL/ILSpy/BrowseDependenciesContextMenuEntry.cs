// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

#if ILSpy

using System.Linq;
using AssemblyVisualizer.DependencyBrowser;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;

namespace AssemblyVisualizer.HAL.ILSpy
{
	[ExportContextMenuEntry(Header = "Browse Dependencies")]
	internal sealed class BrowseDependenciesContextMenuEntry : IContextMenuEntry
	{
		public bool IsVisible(TextViewContext context)
		{
			if (context.SelectedTreeNodes == null)
				return false;

			return context.SelectedTreeNodes.OfType<AssemblyTreeNode>().Count() > 0;
		}

		public bool IsEnabled(TextViewContext context)
		{
			return true;
		}

		public void Execute(TextViewContext context)
		{
			var assemblyDefinitions = context.SelectedTreeNodes
				.OfType<AssemblyTreeNode>()
				.Select(n => n.LoadedAssembly.AssemblyDefinition);

			var window = new DependencyBrowserWindow(assemblyDefinitions.Select(HAL.Converter.Assembly))
			{
				Owner = Services.MainWindow
			};
			window.Show();
		}
	}
}

#endif