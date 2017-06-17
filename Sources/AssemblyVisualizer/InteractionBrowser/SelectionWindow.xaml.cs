// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.InteractionBrowser
{
	internal partial class SelectionWindow : Window
	{
		public SelectionWindow(IEnumerable<TypeInfo> types, bool drawGraph)
		{
			InitializeComponent();
			DataContext = new SelectionWindowViewModel(types, drawGraph, this);

			WindowManager.InteractionBrowsersChanged += InteractionBrowsersChangedHandler;

			var animation = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.5)));
			brd.BeginAnimation(OpacityProperty, animation);
		}

		public SelectionWindowViewModel ViewModel
		{
			get { return DataContext as SelectionWindowViewModel; }
			set { DataContext = value; }
		}

		private void InteractionBrowsersChangedHandler()
		{
			ViewModel.Refresh();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			WindowManager.InteractionBrowsersChanged -= InteractionBrowsersChangedHandler;
		}
	}
}