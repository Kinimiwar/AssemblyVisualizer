﻿// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyVisualizer.AssemblyBrowser;
using AssemblyVisualizer.AncestryBrowser;
using AssemblyVisualizer.DependencyBrowser;
using AssemblyVisualizer.HAL;
using System.Windows;
using AssemblyVisualizer.InteractionBrowser;

namespace AssemblyVisualizer
{
	static class WindowManager
	{
		private static readonly IList<AssemblyBrowserWindow> _assemblyBrowsers = 
			new List<AssemblyBrowserWindow>();
        private static readonly IList<AncestryBrowserWindow> _ancestryBrowsers =
            new List<AncestryBrowserWindow>();
        private static readonly IList<DependencyBrowserWindow> _dependencyBrowsers =
            new List<DependencyBrowserWindow>();
        private static readonly IList<InteractionBrowserWindow> _interactionBrowsers =
            new List<InteractionBrowserWindow>();

        public static event Action InteractionBrowsersChanged;

		public static IList<AssemblyBrowserWindow> AssemblyBrowsers
		{
			get { return _assemblyBrowsers; }
		}

        public static IList<AncestryBrowserWindow> AncestryBrowsers
        {
            get { return _ancestryBrowsers; }
        }

        public static IList<DependencyBrowserWindow> DependencyBrowsers
        {
            get { return _dependencyBrowsers; }
        }

        public static IList<InteractionBrowserWindow> InteractionBrowsers
        {
            get { return _interactionBrowsers; }
        }

		public static void AddAssemblyBrowser(AssemblyBrowserWindow window)
		{
			_assemblyBrowsers.Add(window);            
		}

		public static void RemoveAssemblyBrowser(AssemblyBrowserWindow window)
		{
			_assemblyBrowsers.Remove(window);
            ClearCacheIfPossible();
            GC.Collect();
		}

        public static void AddAncestryBrowser(AncestryBrowserWindow window)
        {
            _ancestryBrowsers.Add(window);            
        }

        public static void RemoveAncestryBrowser(AncestryBrowserWindow window)
        {
            _ancestryBrowsers.Remove(window);
            ClearCacheIfPossible();
            GC.Collect();
        }

        public static void AddDependencyBrowser(DependencyBrowserWindow window)
        {
            _dependencyBrowsers.Add(window);            
        }

        public static void RemoveDependencyBrowser(DependencyBrowserWindow window)
        {
            _dependencyBrowsers.Remove(window);
            ClearCacheIfPossible();
            GC.Collect();
        }

        public static void AddInteractionBrowser(InteractionBrowserWindow window)
        {
            _interactionBrowsers.Add(window);
            OnInteractionBrowsersChanged();
        }

        public static void RemoveInteractionBrowser(InteractionBrowserWindow window)
        {
            _interactionBrowsers.Remove(window);
            ClearCacheIfPossible();
            GC.Collect();
            OnInteractionBrowsersChanged();
        }

        private static void ClearCacheIfPossible()
        {
            if (AssemblyBrowsers.Count == 0
                && AncestryBrowsers.Count == 0
                && DependencyBrowsers.Count == 0
                && InteractionBrowsers.Count == 0)
            {
                Converter.ClearCache();
            }
        }

        private static void OnInteractionBrowsersChanged()
        {
            var handler = InteractionBrowsersChanged;
            if (handler != null)
            {
                handler();
            }
        }
	}
}
