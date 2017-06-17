// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using AssemblyVisualizer.Behaviors;
using AssemblyVisualizer.Controls.ZoomControl;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.DependencyBrowser
{
	internal partial class DependencyBrowserWindow : Window
	{
		public DependencyBrowserWindow(IEnumerable<AssemblyInfo> assemblies)
		{
			InitializeComponent();

			ViewModel = new DependencyBrowserWindowViewModel(assemblies);

			ViewModel.FillGraphRequest += FillGraphRequestHandler;
			ViewModel.OriginalSizeRequest += OriginalSizeRequestHandler;
			ViewModel.FocusSearchRequest += FocusSearchRequestHandler;

			Loaded += LoadedHandler;
			Unloaded += UnloadedHandler;

			WindowManager.AddDependencyBrowser(this);
		}

		public DependencyBrowserWindowViewModel ViewModel
		{
			get { return DataContext as DependencyBrowserWindowViewModel; }
			set { DataContext = value; }
		}

		private void LoadedHandler(object sender, RoutedEventArgs e)
		{
			brdSearch.SetValue(VisibilityAnimation.AnimationTypeProperty, VisibilityAnimation.AnimationType.Fade);
		}

		private void UnloadedHandler(object sender, RoutedEventArgs e)
		{
			brdSearch.SetValue(VisibilityAnimation.AnimationTypeProperty, VisibilityAnimation.AnimationType.None);
			txtSearchHint.SetValue(VisibilityAnimation.AnimationTypeProperty, VisibilityAnimation.AnimationType.None);
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			WindowManager.RemoveDependencyBrowser(this);
		}

		private void FillGraphRequestHandler()
		{
			zoomControl.ZoomToFill();
		}

		private void OriginalSizeRequestHandler()
		{
			var animation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
			zoomControl.BeginAnimation(ZoomControl.ZoomProperty, animation);
		}

		private void FocusSearchRequestHandler()
		{
			txtSearch.Focus();
		}

		private void SearchPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				e.Handled = true;
				ViewModel.HideSearchCommand.Execute(null);
			}
			if (e.Key == Key.Enter)
			{
				e.Handled = true;
				ViewModel.SelectFoundAssemblies();
				ViewModel.HideSearchCommand.Execute(null);
			}
		}
	}
}