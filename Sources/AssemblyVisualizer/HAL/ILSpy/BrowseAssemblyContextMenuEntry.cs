// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

#if ILSpy
using System.Linq;
using AssemblyVisualizer.HAL;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;

namespace AssemblyVisualizer.AssemblyBrowser
{
	[ExportContextMenuEntry(Header = "Browse Assembly")]
	internal sealed class BrowseAssemblyContextMenuEntry : IContextMenuEntry
	{
		public bool IsVisible(TextViewContext context)
		{
			if (context.SelectedTreeNodes == null)
				return false;

			return context.SelectedTreeNodes.All(n => n is AssemblyTreeNode);
		}

		public bool IsEnabled(TextViewContext context)
		{
			return true;
		}

		public void Execute(TextViewContext context)
		{
			var assemblyDefinitions = context.SelectedTreeNodes
				.OfType<AssemblyTreeNode>()
				.Select(n => Converter.Assembly(n.LoadedAssembly.GetAssemblyDefinitionAsync().Result))
				.ToList();

			Services.BrowseAssemblies(assemblyDefinitions);
		}
	}
}

#endif