﻿// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

#if ILSpy

using System.Linq;
using AssemblyVisualizer.AssemblyBrowser;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;

namespace AssemblyVisualizer.HAL.ILSpy
{
	[ExportContextMenuEntry(Header = "Visualize Descendants")]
	internal sealed class VisualizeDescendantsContextMenuEntry : IContextMenuEntry
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
			var type = HAL.Converter.Type(typeDefinition);
			var assembly = type.Module.Assembly;

			var window = new AssemblyBrowserWindow(new[] {assembly}, type)
			{
				Owner = MainWindow.Instance
			};
			window.Show();
		}
	}
}

#endif