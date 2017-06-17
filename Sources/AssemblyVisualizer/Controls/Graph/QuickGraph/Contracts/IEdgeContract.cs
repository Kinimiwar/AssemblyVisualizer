// Adopted, originally created as part of QuickGraph library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Diagnostics.Contracts;

namespace AssemblyVisualizer.Controls.Graph.QuickGraph.Contracts
{
	[ContractClassFor(typeof(IEdge<>))]
	internal abstract class IEdgeContract<TVertex>
		: IEdge<TVertex>
	{
		TVertex IEdge<TVertex>.Source
		{
			get
			{
				Contract.Ensures(Contract.Result<TVertex>() != null);
				return default(TVertex);
			}
		}

		TVertex IEdge<TVertex>.Target
		{
			get
			{
				Contract.Ensures(Contract.Result<TVertex>() != null);
				return default(TVertex);
			}
		}

		public bool IsTwoWay { get; set; }

		[ContractInvariantMethod]
		private void IEdgeInvariant()
		{
			IEdge<TVertex> ithis = this;
			Contract.Invariant(ithis.Source != null);
			Contract.Invariant(ithis.Target != null);
		}
	}
}