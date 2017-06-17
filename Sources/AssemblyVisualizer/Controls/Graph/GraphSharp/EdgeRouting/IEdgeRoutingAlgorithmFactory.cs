﻿// Adopted, originally created as part of GraphSharp library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Collections.Generic;
using AssemblyVisualizer.Controls.Graph.GraphSharp.Layout;
using AssemblyVisualizer.Controls.Graph.QuickGraph;

namespace AssemblyVisualizer.Controls.Graph.GraphSharp.EdgeRouting
{
	public interface IEdgeRoutingAlgorithmFactory<TVertex, TEdge, TGraph>
		where TVertex : class
		where TEdge : IEdge<TVertex>
		where TGraph : class, IBidirectionalGraph<TVertex, TEdge>
	{
		/// <summary>
		/// List of the available algorithms.
		/// </summary>
		IEnumerable<string> AlgorithmTypes { get; }

		IEdgeRoutingAlgorithm<TVertex, TEdge, TGraph> CreateAlgorithm( string newAlgorithmType, ILayoutContext<TVertex, TEdge, TGraph> context, IEdgeRoutingParameters parameters);

		IEdgeRoutingParameters CreateParameters( string algorithmType, IEdgeRoutingParameters oldParameters );

		bool IsValidAlgorithm( string algorithmType );

		string GetAlgorithmType( IEdgeRoutingAlgorithm<TVertex, TEdge, TGraph> algorithm );
	}
}