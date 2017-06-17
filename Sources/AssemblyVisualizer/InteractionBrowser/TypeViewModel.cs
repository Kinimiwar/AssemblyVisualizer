// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Collections.Generic;
using System.Windows.Media;
using AssemblyVisualizer.Infrastructure;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.InteractionBrowser
{
	internal class TypeViewModel : ViewModelBase
	{
		private SolidColorBrush _background;
		private SolidColorBrush _foreground = Brushes.Gray;
		private bool _isSelected;
		private bool _showInternals = true;
		private readonly InteractionBrowserWindowViewModel _windowViewModel;

		public TypeViewModel(TypeInfo typeInfo, InteractionBrowserWindowViewModel windowViewModel)
		{
			TypeInfo = typeInfo;
			_windowViewModel = windowViewModel;

			Hierarchies = new List<HierarchyViewModel>();
		}

		public string Name
		{
			get { return TypeInfo.Name; }
		}

		public string FullName
		{
			get { return TypeInfo.FullName; }
		}

		public TypeInfo TypeInfo { get; private set; }

		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				DefineIsSelected(value);
				NotifyHierarchiesSelectionChanged();
			}
		}

		public bool ShowInternals
		{
			get { return _showInternals; }
			set
			{
				_showInternals = value;
				OnPropertyChanged("ShowInternals");
				_windowViewModel.ReportSelectionChanged();
			}
		}

		public IList<HierarchyViewModel> Hierarchies { get; private set; }

		public SolidColorBrush Foreground
		{
			get { return _foreground; }
			set
			{
				_foreground = value;
				OnPropertyChanged("Foreground");
			}
		}

		public SolidColorBrush Background
		{
			get { return _background; }
			set
			{
				_background = value;
				OnPropertyChanged("Background");
				Foreground = GetForeground(value);
			}
		}

		public void DefineIsSelected(bool isSelected)
		{
			_isSelected = isSelected;
			OnPropertyChanged("IsSelected");
			_windowViewModel.ReportSelectionChanged();
		}

		private void NotifyHierarchiesSelectionChanged()
		{
			if (Hierarchies.Count > 0)
				foreach (var hierarchy in Hierarchies)
					hierarchy.NotifySelectionChanged();
		}

		private static SolidColorBrush GetForeground(SolidColorBrush background)
		{
			if (background == null)
				return Brushes.Gray;
			var backgroundColor = background.Color;
			var foregroundColor = new Color
			{
				A = 255,
				R = (byte) (backgroundColor.R / 2.5),
				G = (byte) (backgroundColor.G / 2.5),
				B = (byte) (backgroundColor.B / 2.5)
			};
			return new SolidColorBrush(foregroundColor);
		}
	}
}