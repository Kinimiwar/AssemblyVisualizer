// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

#if ILSpy

using System.Linq;
using ICSharpCode.ILSpy;
using ICSharpCode.ILSpy.TreeNodes;

namespace AssemblyVisualizer.HAL.ILSpy
{
	[ExportContextMenuEntry(Header = "Browse Interactions")]
	internal sealed class BrowseInteractionsContextMenuEntry : IContextMenuEntry
	{
		public void Execute(TextViewContext context)
		{
			var types = context.SelectedTreeNodes
				.OfType<TypeTreeNode>()
				.Select(n => HAL.Converter.Type(n.TypeDefinition))
				.ToArray();

			Services.BrowseInteractions(types, true);
		}

		public bool IsEnabled(TextViewContext context)
		{
			return true;
		}

		public bool IsVisible(TextViewContext context)
		{
			if (context.SelectedTreeNodes == null)
				return false;

			return context.SelectedTreeNodes.All(n => n is TypeTreeNode);
		}
	}
}

#endif