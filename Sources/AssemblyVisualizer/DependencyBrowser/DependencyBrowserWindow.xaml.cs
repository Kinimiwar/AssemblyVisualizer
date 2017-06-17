﻿// Copyright 2011 Denis Markelov
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
using System.Windows.Media.Animation;
using AssemblyVisualizer.Model;
using AssemblyVisualizer.Controls.ZoomControl;
using AssemblyVisualizer.Behaviors;

namespace AssemblyVisualizer.DependencyBrowser
{    
    partial class DependencyBrowserWindow : Window
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

        private void LoadedHandler(object sender, RoutedEventArgs e)
        {
            brdSearch.SetValue(VisibilityAnimation.AnimationTypeProperty, VisibilityAnimation.AnimationType.Fade);
        }

        private void UnloadedHandler(object sender, RoutedEventArgs e)
        {
            brdSearch.SetValue(VisibilityAnimation.AnimationTypeProperty, VisibilityAnimation.AnimationType.None);
            txtSearchHint.SetValue(VisibilityAnimation.AnimationTypeProperty, VisibilityAnimation.AnimationType.None);
        }

        public DependencyBrowserWindowViewModel ViewModel
        {
            get
            {
                return DataContext as DependencyBrowserWindowViewModel;
            }
            set
            {
                DataContext = value;
            }
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
