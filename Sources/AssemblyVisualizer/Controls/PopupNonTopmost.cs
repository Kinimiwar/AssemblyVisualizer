﻿// Adopted, originally created by Chris Cavanagh
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Animation;

namespace AssemblyVisualizer.Controls
{
	public class PopupNonTopmost : System.Windows.Controls.Primitives.Popup
	{
		public static readonly DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner(
			typeof(PopupNonTopmost),
			new FrameworkPropertyMetadata(false, OnTopmostChanged));

		public bool Topmost
		{
			get { return (bool)GetValue(TopmostProperty); }
			set { SetValue(TopmostProperty, value); }
		}

		private static void OnTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			(obj as PopupNonTopmost).UpdateWindow();
		}        

		protected override void OnOpened(EventArgs e)
		{
			UpdateWindow();            
        }

		private void UpdateWindow()
		{
			var hwnd = ((HwndSource)PresentationSource.FromVisual(this.Child)).Handle;
			RECT rect;

			if (NativeMethods.GetWindowRect(hwnd, out rect))
			{
				var result = NativeMethods.SetWindowPos(hwnd, Topmost ? -1 : -2, rect.Left, rect.Top, (int)Width, (int)Height, 0);
				if (result != 1)
				{
					throw new InvalidOperationException("Cannot show popup.");
				}
			}
		}

		#region P/Invoke imports & definitions

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			private int _left;
			private int _top;
			private int _right;
			private int _bottom;

			public int Left
			{
				get { return _left; }
			}

			public int Top
			{
				get { return _top; }
			}
		}

		private static class NativeMethods
		{
			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

			[DllImport("user32", EntryPoint = "SetWindowPos")]
			public static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);
		}
		#endregion
	}

}
