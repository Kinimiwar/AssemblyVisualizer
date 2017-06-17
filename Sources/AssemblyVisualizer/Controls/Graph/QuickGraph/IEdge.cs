// Adopted, originally created as part of QuickGraph library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Diagnostics.Contracts;
using AssemblyVisualizer.Controls.Graph.QuickGraph.Contracts;

namespace AssemblyVisualizer.Controls.Graph.QuickGraph
{
	/// <summary>
	///     A directed edge
	/// </summary>
	/// <typeparam name="TVertex">type of the vertices</typeparam>
	[ContractClass(typeof(IEdgeContract<>))]
	public interface IEdge<TVertex>
	{
		/// <summary>
		///     Gets the source vertex
		/// </summary>
		TVertex Source { get; }

		/// <summary>
		///     Gets the target vertex
		/// </summary>
		TVertex Target { get; }

		bool IsTwoWay { get; }
	}
}