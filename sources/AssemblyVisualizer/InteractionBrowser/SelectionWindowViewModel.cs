// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyVisualizer.Infrastructure;
using System.Windows.Media;
using System.Windows.Controls;
using AssemblyVisualizer.Model;
using System.Windows;
using AssemblyVisualizer.HAL;

namespace AssemblyVisualizer.InteractionBrowser
{
    class SelectionWindowViewModel : ViewModelBase
    {
        public SelectionWindowViewModel(IEnumerable<TypeInfo> types, bool drawGraph, SelectionWindow window)
        {
            var thumbnails = WindowManager.InteractionBrowsers.Select(ib => new ThumbnailViewModel(ib.Thumbnail, ib.ThumbnailTooltip, () => { ib.AddTypes(types, drawGraph); window.Close(); })).ToList();
            var newWindowThumbnail = new ThumbnailViewModel(
                null, 
                null,
                () =>
                {
                    Services.BrowseInteractions(types, drawGraph, true);
                    window.Close();
                });
            thumbnails.Insert(0, newWindowThumbnail);
            Thumbnails = thumbnails;
        }

        public IEnumerable<ThumbnailViewModel> Thumbnails { get; private set; }
    }
}
