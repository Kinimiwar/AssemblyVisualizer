// Adopted, originally created as part of GraphSharp project
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Windows.Controls;

namespace AssemblyVisualizer.Controls.Graph.Transitions
{
	public abstract class TransitionBase : ITransition
	{
		#region ITransition Members

		public void Run(IAnimationContext context, Control control, TimeSpan duration)
		{
			Run(context, control, duration, null);
		}

		public abstract void Run(IAnimationContext context,
			Control control,
			TimeSpan duration,
			Action<Control> endMethod);

		#endregion
	}
}