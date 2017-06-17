﻿// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyVisualizer.Infrastructure;
using AssemblyVisualizer.Model;
using AssemblyVisualizer.Properties;
using System.Collections.ObjectModel;
using AssemblyVisualizer.Controls.Graph.QuickGraph;
using AssemblyVisualizer.AssemblyBrowser;
using AssemblyVisualizer.HAL;
using System.Windows.Input;

namespace AssemblyVisualizer.DependencyBrowser
{
    class DependencyBrowserWindowViewModel : ViewModelBase
    {
        private IEnumerable<AssemblyInfo> _assemblies;
        private IEnumerable<AssemblyViewModel> _assemblyViewModels;
        private AssemblyGraph _assemblyGraph;
        private string _searchTerm;
        private bool _isSearchVisible;

        public DependencyBrowserWindowViewModel(IEnumerable<AssemblyInfo> assemblies)
        {
            _assemblies = assemblies.ToList();
            var inputAssemblyViewModels = assemblies
                .Select(a => AssemblyViewModel.Create(a))
                .ToList();
            foreach (var vm in inputAssemblyViewModels)
            {
                vm.IsMarked = true;
                vm.IsRoot = true;
            }
            _assemblyGraph = CreateGraph(inputAssemblyViewModels);
            _assemblyViewModels = _assemblyGraph.Vertices.ToList();

            HideSearchCommand = new DelegateCommand(HideSearchCommandHandler);
            ShowSearchCommand = new DelegateCommand(ShowSearchCommandHandler);
            BrowseSelectedCommand = new DelegateCommand(BrowseCommandHandler);
            ClearSelectionCommand = new DelegateCommand(ClearSelectionCommandHandler);
            RemoveSelectedCommand = new DelegateCommand(RemoveSelectedCommandHandler);
            Commands = new ObservableCollection<UserCommand>
			           	{
			           		new UserCommand(Resources.FillGraph, OnFillGraphRequest),
			           		new UserCommand(Resources.OriginalSize, OnOriginalSizeRequest),	
                            new UserCommand(Resources.SearchInGraph, ShowSearchCommand),
                            new UserCommand(Resources.BrowseSelected, BrowseSelectedCommand),
                            new UserCommand(Resources.RemoveSelected, RemoveSelectedCommand),
                            new UserCommand(Resources.ClearSelection, ClearSelectionCommand)                            
			           	};
        }

        public event Action FillGraphRequest;
        public event Action OriginalSizeRequest;
        public event Action FocusSearchRequest;

        public IEnumerable<UserCommand> Commands { get; private set; }

        public ICommand HideSearchCommand { get; private set; }
        public ICommand ShowSearchCommand { get; private set; }
        public ICommand BrowseSelectedCommand { get; private set; }
        public ICommand ClearSelectionCommand { get; private set; }
        public ICommand RemoveSelectedCommand { get; private set; }

        public AssemblyGraph Graph
        {
            get
            {
                return _assemblyGraph;
            }
        }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged("SearchTerm");
                OnPropertyChanged("IsSearchTermFilled");
                PerformSearch();
            }
        }

        public bool IsSearchVisible
        {
            get { return _isSearchVisible; }
            set
            {
                _isSearchVisible = value;
                OnPropertyChanged("IsSearchVisible");
            }
        }

        public bool IsSearchTermFilled { get { return !string.IsNullOrWhiteSpace(SearchTerm); } }

        public void SelectFoundAssemblies()
        {
            foreach (var assembly in _assemblyViewModels
                .Where(a => a.IsFound))
            {
                assembly.IsSelected = true;
            }
        }

        private void PerformSearch()
        {
            if (string.IsNullOrEmpty(SearchTerm) || string.IsNullOrEmpty(SearchTerm.Trim()))
            {
                ClearSearch();
                return;
            }

            foreach (var assemblyViewModel in _assemblyViewModels)
            {
                assemblyViewModel.IsFound = assemblyViewModel.Name
                    .IndexOf(SearchTerm, StringComparison.InvariantCultureIgnoreCase) >= 0;
            }
        }

        private void ClearSearch()
        {
            foreach (var assemblyViewModel in _assemblyViewModels)
            {
                assemblyViewModel.IsFound = false;
            }
        }

        private void OnFillGraphRequest()
        {
            var handler = FillGraphRequest;

            if (handler != null)
            {
                handler();
            }
        }

        private void OnOriginalSizeRequest()
        {
            var handler = OriginalSizeRequest;

            if (handler != null)
            {
                handler();
            }
        }

        private void BrowseCommandHandler()
        {
            var selectedAssemblies = _assemblyViewModels
                .Where(a => a.IsSelected)
                .Select(a => a.AssemblyInfo);
            Services.BrowseAssemblies(selectedAssemblies);
        }

        private void ClearSelectionCommandHandler()
        {
            foreach (var assemblyViewModel in _assemblyViewModels)
            {
                assemblyViewModel.IsSelected = false;
            }
        }

        private void RemoveSelectedCommandHandler()
        {
            SelectUnreachableAssemblies();
            var assembliesToRemove = _assemblyViewModels.Where(a => a.IsSelected).ToArray();
            Graph.RemoveVertexIf(a => a.IsSelected);

            foreach (var assembly in assembliesToRemove)
            {
                assembly.IsSelected = false;
                assembly.IsRemoved = true;
            }
        }

        private void HideSearchCommandHandler()
        {
            IsSearchVisible = false;
            SearchTerm = string.Empty;
        }

        private void ShowSearchCommandHandler()
        {
            IsSearchVisible = true;
            OnFocusSearchRequest();
        }

        private void OnFocusSearchRequest()
        {
            var handler = FocusSearchRequest;

            if (handler != null)
            {
                handler();
            }
        }

        private static AssemblyGraph CreateGraph(IEnumerable<AssemblyViewModel> assemblies)
        {
            var graph = new AssemblyGraph(true);

            foreach (var assembly in assemblies)
            {
                if (!graph.ContainsVertex(assembly))
                {
                    graph.AddVertex(assembly);
                }
                AddReferencesRecursive(graph, assembly);
            }

            AssemblyViewModel.ClearCache();
            return graph;
        }

        private static void AddReferencesRecursive(AssemblyGraph graph, AssemblyViewModel assembly)
        {
            assembly.IsProcessed = true;
            foreach (var refAssembly in assembly.ReferencedAssemblies)
            {
                if (!graph.ContainsVertex(refAssembly))
                {
                    graph.AddVertex(refAssembly);
                }

                var edge = new Edge<AssemblyViewModel>(assembly, refAssembly);

                Edge<AssemblyViewModel> reverseEdge;
                var result = graph.TryGetEdge(refAssembly, assembly, out reverseEdge);
                if (result)
                {
                    reverseEdge.IsTwoWay = true;
                    edge.IsTwoWay = true;
                }

                graph.AddEdge(edge);
                if (!refAssembly.IsProcessed)
                {
                    AddReferencesRecursive(graph, refAssembly);
                }
            }
        }

        private void SelectUnreachableAssemblies()
        {
            foreach (var assembly in _assemblyViewModels)
            {
                assembly.IsProcessed = false;
            }

            var roots = _assemblyViewModels.Where(
                a => a.IsRoot && !a.IsSelected && !a.IsRemoved).ToList();
            foreach (var root in roots)
            {
                ProcessRec(root);
            }
            foreach (var assembly in _assemblyViewModels)
            {
                if (!assembly.IsRemoved && !assembly.IsProcessed
                    && !assembly.IsRoot && !assembly.IsSelected)
                {
                    assembly.IsSelected = true;
                }
            }
        }

        private void ProcessRec(AssemblyViewModel assembly)
        {
            assembly.IsProcessed = true;
            foreach (var refAssembly in assembly.ReferencedAssemblies)
            {
                if (!refAssembly.IsRemoved && !refAssembly.IsProcessed
                    && !refAssembly.IsRoot && !refAssembly.IsSelected)
                {
                    ProcessRec(refAssembly);
                }
            }
        }
    }
}
