﻿// Adopted, originally created as part of GraphSharp project
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Windows;
using System.Windows.Controls;
using AssemblyVisualizer.Controls.Graph.Helpers;

namespace AssemblyVisualizer.Controls.Graph
{
	/// <summary>
	///     Logical representation of a vertex.
	/// </summary>
	public class VertexControl : Control, IPoolObject, IDisposable
	{
		public static readonly DependencyProperty VertexProperty =
			DependencyProperty.Register("Vertex", typeof(object), typeof(VertexControl), new UIPropertyMetadata(null));

		public static readonly DependencyProperty RootCanvasProperty =
			DependencyProperty.Register("RootCanvas", typeof(GraphCanvas), typeof(VertexControl), new UIPropertyMetadata(null));

		static VertexControl()
		{
			//override the StyleKey Property
			DefaultStyleKeyProperty.OverrideMetadata(typeof(VertexControl), new FrameworkPropertyMetadata(typeof(VertexControl)));
		}

		public object Vertex
		{
			get { return GetValue(VertexProperty); }
			set { SetValue(VertexProperty, value); }
		}


		public GraphCanvas RootCanvas
		{
			get { return (GraphCanvas) GetValue(RootCanvasProperty); }
			set { SetValue(RootCanvasProperty, value); }
		}

		#region IPoolObject Members

		public void Reset()
		{
			Vertex = null;
		}

		public void Terminate()
		{
			//nothing to do, there are no unmanaged resources
		}

		public event DisposingHandler Disposing;

		public void Dispose()
		{
			if (Disposing != null)
				Disposing(this);
		}

		#endregion
	}
}