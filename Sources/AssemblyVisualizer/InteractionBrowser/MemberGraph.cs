// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using AssemblyVisualizer.Common;
using AssemblyVisualizer.Controls.Graph;
using AssemblyVisualizer.Controls.Graph.QuickGraph;

namespace AssemblyVisualizer.InteractionBrowser
{
	internal class MemberGraph : BidirectionalGraph<MemberViewModel, Edge<MemberViewModel>>
	{
		public MemberGraph(bool allowParallelEdges)
			: base(allowParallelEdges)
		{
		}
	}

	internal class MemberGraphLayout : GraphLayout<MemberViewModel, Edge<MemberViewModel>, MemberGraph>
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