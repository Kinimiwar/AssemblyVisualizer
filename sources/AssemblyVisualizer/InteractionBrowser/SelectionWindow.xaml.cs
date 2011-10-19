// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.InteractionBrowser
{    
    partial class SelectionWindow : Window
    {
        public SelectionWindow(IEnumerable<TypeInfo> types, bool drawGraph)
        {
            InitializeComponent();
            DataContext = new SelectionWindowViewModel(types, drawGraph, this);

            WindowManager.InteractionBrowsersChanged += InteractionBrowsersChangedHandler;
        }

        public SelectionWindowViewModel ViewModel
        {
            get
            {
                return DataContext as SelectionWindowViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        private void InteractionBrowsersChangedHandler()
        {
            ViewModel.Refresh();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            WindowManager.InteractionBrowsersChanged -= InteractionBrowsersChangedHandler;
        }
    }
}
