// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using AssemblyVisualizer.Controls.Graph;
using AssemblyVisualizer.Controls.Graph.QuickGraph;

namespace AssemblyVisualizer.DependencyBrowser
{
	internal class AssemblyGraph : BidirectionalGraph<AssemblyViewModel, Edge<AssemblyViewModel>>
	{
		public AssemblyGraph(bool allowParallelEdges)
			: base(allowParallelEdges)
		{
		}
	}

	internal class AssemblyGraphLayout : GraphLayout<AssemblyViewModel, Edge<AssemblyViewModel>, AssemblyGraph>
	{
		public event Action LayoutFinished;

		protected override void OnLayoutFinished()
		{
			base.OnLayoutFinished();

			var handler = LayoutFinished;
			if (handler != null)
				handler();
		}
	}
}