// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AssemblyVisualizer.HAL;
using AssemblyVisualizer.Infrastructure;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.InteractionBrowser
{
	internal class SelectionWindowViewModel : ViewModelBase
	{
		private const int HeightOverhead = 62;
		private const int WidthOverhead = 44;
		private const int OverheadPerPiece = 10;
		private const int WindowsInRow = 3;
		private readonly bool _drawGraph;
		private int _height;
		private int _pieceHeight;
		private int _pieceWidth;
		private IEnumerable<ThumbnailViewModel> _thumbnails;
		private readonly IEnumerable<TypeInfo> _types;
		private int _width;

		private readonly SelectionWindow _window;

		public SelectionWindowViewModel(IEnumerable<TypeInfo> types, bool drawGraph, SelectionWindow window)
		{
			_window = window;
			_types = types;
			_drawGraph = drawGraph;

			_pieceHeight = 250;
			_pieceWidth = 250;

			Refresh();

			CancelCommand = new DelegateCommand(CancelCommandHandler);
		}

		public ICommand CancelCommand { get; private set; }

		public IEnumerable<ThumbnailViewModel> Thumbnails
		{
			get { return _thumbnails; }
			private set
			{
				_thumbnails = value;
				OnPropertyChanged("Thumbnails");
			}
		}

		public int Height
		{
			get { return _height; }
			set
			{
				_height = value;
				OnPropertyChanged("Height");
			}
		}

		public int Width
		{
			get { return _width; }
			set
			{
				_width = value;
				OnPropertyChanged("Width");
			}
		}

		public int PieceHeight
		{
			get { return _pieceHeight; }
			set
			{
				_pieceHeight = value;
				OnPropertyChanged("PieceHeight");
			}
		}

		public int PieceWidth
		{
			get { return _pieceWidth; }
			set
			{
				_pieceWidth = value;
				OnPropertyChanged("PieceWidth");
			}
		}

		public void Refresh()
		{
			var thumbnails =
				WindowManager.InteractionBrowsers.Select(ib => new ThumbnailViewModel(ib.Thumbnail, ib.ThumbnailTooltip, () =>
				{
					_window.Close();
					ib.AddTypes(_types, _drawGraph);
				})).ToList();
			var newWindowThumbnail = new ThumbnailViewModel(
				null,
				null,
				() =>
				{
					_window.Close();
					Services.BrowseInteractions(_types, _drawGraph, true);
				});
			thumbnails.Insert(0, newWindowThumbnail);
			Thumbnails = thumbnails;

			_window.Width = Math.Min(WindowsInRow, thumbnails.Count) * (PieceWidth + OverheadPerPiece) + WidthOverhead;

			var rowsNumber = thumbnails.Count / WindowsInRow;
			if (thumbnails.Count % WindowsInRow != 0)
				rowsNumber++;
			if (rowsNumber > 3)
				rowsNumber = 3;
			_window.Height = rowsNumber * (PieceHeight + OverheadPerPiece) + HeightOverhead;
		}

		private void CancelCommandHandler()
		{
			_window.Close();
		}
	}
}