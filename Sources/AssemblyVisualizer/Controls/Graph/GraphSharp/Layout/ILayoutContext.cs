// Adopted, originally created as part of GraphSharp library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Collections.Generic;
using System.Windows;
using AssemblyVisualizer.Controls.Graph.QuickGraph;

namespace AssemblyVisualizer.Controls.Graph.GraphSharp.Layout
{
	public interface ILayoutContext<TVertex, TEdge, TGraph>
		where TEdge : IEdge<TVertex>
		where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
	{
		IDictionary<TVertex, Point> Positions { get; }
		IDictionary<TVertex, Size> Sizes { get; }

		TGraph Graph { get; }

		LayoutMode Mode { get; }
	}
}