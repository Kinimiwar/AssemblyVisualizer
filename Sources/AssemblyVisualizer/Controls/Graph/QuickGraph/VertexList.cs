// Adopted, originally created as part of QuickGraph library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;

namespace AssemblyVisualizer.Controls.Graph.QuickGraph
{
#if !SILVERLIGHT
	[Serializable]
#endif
	public sealed class VertexList<TVertex>
		: List<TVertex>
#if !SILVERLIGHT
			, ICloneable
#endif
	{
		public VertexList()
		{
		}

		public VertexList(int capacity)
			: base(capacity)
		{
		}

		public VertexList(VertexList<TVertex> other)
			: base(other)
		{
		}

#if !SILVERLIGHT
		object ICloneable.Clone()
		{
			return Clone();
		}
#endif

		public VertexList<TVertex> Clone()
		{
			return new VertexList<TVertex>(this);
		}
	}
}