// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

#if ILSpy

using System.Linq;
using AssemblyVisualizer.AncestryBrowser;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;

namespace AssemblyVisualizer.HAL.ILSpy
{
	[ExportContextMenuEntry(Header = "Browse Ancestry")]
	internal sealed class BrowseAncestryContextMenuEntry : IContextMenuEntry
	{
		public bool IsVisible(TextViewContext context)
		{
			if (context.SelectedTreeNodes == null)
				return false;

			return context.SelectedTreeNodes.Count() == 1
			       && context.SelectedTreeNodes.Single() is TypeTreeNode;
		}

		public bool IsEnabled(TextViewContext context)
		{
			return true;
		}

		public void Execute(TextViewContext context)
		{
			var typeDefinition = context.SelectedTreeNodes
				.OfType<TypeTreeNode>()
				.Single().TypeDefinition;

			var window = new AncestryBrowserWindow(HAL.Converter.Type(typeDefinition))
			{
				Owner = MainWindow.Instance
			};
			window.Show();
		}
	}
}

#endif