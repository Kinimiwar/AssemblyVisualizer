// Adopted, originally created as part of GraphSharp library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using AssemblyVisualizer.Controls.Graph.QuickGraph;

namespace AssemblyVisualizer.Controls.Graph.GraphSharp
{
	public class WeightedEdge<Vertex> : Edge<Vertex>
	{
		public WeightedEdge(Vertex source, Vertex target)
			: this(source, target, 1)
		{
		}

		public WeightedEdge(Vertex source, Vertex target, double weight)
			: base(source, target)
		{
			Weight = weight;
		}

		public double Weight { get; private set; }
	}
}