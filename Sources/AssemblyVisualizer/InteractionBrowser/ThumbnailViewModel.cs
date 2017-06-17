// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyVisualizer.Infrastructure;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace AssemblyVisualizer.InteractionBrowser
{
    class ThumbnailViewModel : ViewModelBase
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
        public bool IsNewWindow { get { return Thumbnail == null; } }        
    }
}
