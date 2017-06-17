using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AssemblyVisualizer.Behaviors;
// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

namespace AssemblyVisualizer.AssemblyBrowser.Screens
{
	/// <summary>
	///     Interaction logic for SearchScreenView.xaml
	/// </summary>
	internal partial class SearchScreenView : UserControl
	{
		private bool _isInitialized;

		public SearchScreenView()
		{
			InitializeComponent();
			Loaded += LoadedHandler;
			Unloaded += UnloadedHandler;
		}

		private SearchScreen ViewModel
		{
			get { return DataContext as SearchScreen; }
		}

		private void LoadedHandler(object sender, RoutedEventArgs e)
		{
			if (ViewModel == null)
			{
				DataContextChanged += SearchScreenView_DataContextChanged;
			}
			else
			{
				if (!_isInitialized)
				{
					ViewModel.SearchFocusRequested += SearchFocusRequestedHandler;
					_isInitialized = true;
				}
			}
			txtSearch.Focus();
			txtClearSearch.SetValue(VisibilityAnimation.AnimationTypeProperty, VisibilityAnimation.AnimationType.Fade);
		}

		private void UnloadedHandler(object sender, RoutedEventArgs e)
		{
			txtClearSearch.SetValue(VisibilityAnimation.AnimationTypeProperty, VisibilityAnimation.AnimationType.None);
		}

		private void SearchScreenView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!_isInitialized)
			{
				ViewModel.SearchFocusRequested += SearchFocusRequestedHandler;

				_isInitialized = true;
			}
		}

		private void SearchFocusRequestedHandler()
		{
			txtSearch.Focus();
		}

		private void SearchPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				txtSearch.Text = string.Empty;
			}
			else if (e.Key == Key.Right && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
			{
				NavigationCommands.BrowseForward.Execute(null, this);
				e.Handled = true;
			}
			else if (e.Key == Key.Left && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
			{
				NavigationCommands.BrowseBack.Execute(null, this);
				e.Handled = true;
			}
		}
	}
}