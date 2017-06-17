// Adopted, originally created as part of GraphSharp library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Collections.Generic;
using System.Windows;
using AssemblyVisualizer.Controls.Graph.QuickGraph;

namespace AssemblyVisualizer.Controls.Graph.GraphSharp.Layout
{
	public class ContextualLayoutContext<TVertex, TEdge, TGraph> : LayoutContext<TVertex, TEdge, TGraph>
		where TEdge : IEdge<TVertex>
		where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
	{
		public ContextualLayoutContext(TGraph graph, TVertex selectedVertex, IDictionary<TVertex, Point> positions,
			IDictionary<TVertex, Size> sizes)
			: base(graph, positions, sizes, LayoutMode.Simple)
		{
			SelectedVertex = selectedVertex;
		}

		public TVertex SelectedVertex { get; private set; }
	}
}