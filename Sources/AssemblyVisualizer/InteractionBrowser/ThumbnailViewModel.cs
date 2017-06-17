// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Windows.Input;
using System.Windows.Media;
using AssemblyVisualizer.Infrastructure;

namespace AssemblyVisualizer.InteractionBrowser
{
	internal class ThumbnailViewModel : ViewModelBase
	{
		public ThumbnailViewModel(Visual thumbnail, string tooltip, Action selectAction)
		{
			Thumbnail = thumbnail;
			Tooltip = tooltip;
			SelectCommand = new DelegateCommand(selectAction);
		}

		public Visual Thumbnail { get; private set; }
		public string Tooltip { get; private set; }
		public ICommand SelectCommand { get; private set; }

		public bool IsNewWindow
		{
			get { return Thumbnail == null; }
		}
	}
}