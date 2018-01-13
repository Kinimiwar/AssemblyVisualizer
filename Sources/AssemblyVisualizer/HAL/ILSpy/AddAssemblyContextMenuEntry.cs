// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

#if ILSpy

using System.Linq;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;

namespace AssemblyVisualizer.HAL.ILSpy
{
	[ExportContextMenuEntry(Header = "Add to Browser")]
	internal sealed class AddAssemblyContextMenuEntry : IContextMenuEntry
	{
		public bool IsVisible(TextViewContext context)
		{
			if (WindowManager.AssemblyBrowsers.Count != 1)
				return false;
			if (context.SelectedTreeNodes == null)
				return false;

			var window = WindowManager.AssemblyBrowsers.Single();
			if (!window.ViewModel.Screen.AllowAssemblyDrop)
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
				.Select(n => HAL.Converter.Assembly(n.LoadedAssembly.GetAssemblyDefinitionAsync().Result))
				.ToList();

			var window = WindowManager.AssemblyBrowsers.Single();
			window.ViewModel.AddAssemblies(assemblyDefinitions);
		}
	}
}

#endif