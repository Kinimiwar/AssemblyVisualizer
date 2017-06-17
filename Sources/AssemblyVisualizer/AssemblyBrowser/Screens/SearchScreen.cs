﻿// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using AssemblyVisualizer.AssemblyBrowser.ViewModels;
using AssemblyVisualizer.Infrastructure;
using System.Windows.Input;
using AssemblyVisualizer.Common.CommandsGroup;
using AssemblyVisualizer.Properties;

namespace AssemblyVisualizer.AssemblyBrowser.Screens
{
	class SearchScreen : Screen
	{
		private enum SearchMode
		{
			All,
			Interfaces,
			ValueTypes,
			Enums
		}

		private enum TypeVisibility
		{
			Any,
			Public,
			Internal
		}

		private enum SortingMode
		{
			Name,
			DescendantsCount,
			MembersCount
		}		

		private string _searchTerm = string.Empty;
		private bool _isSearchPerformed = true;
		private DispatcherTimer _searchTimer;
		private SearchMode _searchMode = SearchMode.All;
		private SortingMode _sortingMode = SortingMode.DescendantsCount;
		private TypeVisibility _typeVisibilityFilter = TypeVisibility.Any;
        private bool _showAnonymousMethodTypes;
        private IList<TypeViewModel> _searchResults;
		
		#region // .ctor

		public SearchScreen(AssemblyBrowserWindowViewModel windowViewModel)
			: base(windowViewModel)
		{
			InitializeSearchTimer();
			
            ClearSearchCommand = new DelegateCommand(ClearSearchCommandHandler);

			InitializeSearchControl();
		}

		private void InitializeSearchTimer()
		{
			_searchTimer = new DispatcherTimer(DispatcherPriority.Normal, WindowViewModel.Dispatcher)
			{
				Interval = TimeSpan.FromMilliseconds(400)
			};
			_searchTimer.Tick += SearchTimerTick;
		}

		private void InitializeSearchControl()
		{
			var sortingGroup = new CommandsGroupViewModel(
					Resources.SortBy,
				    new List<GroupedUserCommand>
				    	{
				    		new GroupedUserCommand(Resources.Name, SortByName),
				    		new GroupedUserCommand(Resources.DescendantsCount, SortByDescendantsCount, true),
							new GroupedUserCommand(Resources.MembersCount, SortByMembersCount)
				    	});

			var filteringByTypeGroup = new CommandsGroupViewModel(
					Resources.Types,
					new List<GroupedUserCommand>
			         	{
			            	new GroupedUserCommand(Resources.All, ShowAllTypes, true),
			            	new GroupedUserCommand(Resources.Interfaces, ShowInterfaces),
							new GroupedUserCommand(Resources.ValueTypes, ShowValueTypes),
							new GroupedUserCommand(Resources.Enums, ShowEnums)
			            });

			var filteringByVisibilityGroup = new CommandsGroupViewModel(
					Resources.Visibility,
					new List<GroupedUserCommand>
			         	{
			            	new GroupedUserCommand(Resources.Any, ShowAnyVisibility, true),
			            	new GroupedUserCommand(Resources.Public, ShowPublicTypes),
							new GroupedUserCommand(Resources.Internal, ShowInternalTypes)
			            });

			SearchControlGroups = new ObservableCollection<CommandsGroupViewModel>
			                      	{
			                      		sortingGroup,
										filteringByTypeGroup,
										filteringByVisibilityGroup
			                      	};
		}

		#endregion

		public event Action SearchFocusRequested;
		
        public ICommand ClearSearchCommand { get; private set; }

		public bool IsSearchPerformed
		{
			get { return _isSearchPerformed; }
			set
			{
				_isSearchPerformed = value;
				OnPropertyChanged("IsSearchPerformed");
			}
		}

		public string SearchTerm
		{
			get { return _searchTerm; }
			set
			{
				_searchTerm = value;
				if (_searchTimer.IsEnabled)
				{
					_searchTimer.Stop();
				}
				_searchTimer.Start();
				IsSearchPerformed = false;

                OnPropertyChanged("SearchTerm");
				OnPropertyChanged("IsSearchTermEmpty");
                OnPropertyChanged("IsSearchTermFilled");
			}
		}

		public bool IsSearchTermEmpty
		{
			get { return string.IsNullOrEmpty(SearchTerm); }
		}

        public bool IsSearchTermFilled
        {
            get { return !IsSearchTermEmpty; }
        }

        public bool ShowAnonymousMethodTypes
        {
            get
            {
                return _showAnonymousMethodTypes;
            }
            set
            {
                _showAnonymousMethodTypes = value;
                OnPropertyChanged("ShowAnonymousMethodTypes");
                TriggerSearch();
            }
        }

		public ObservableCollection<CommandsGroupViewModel> SearchControlGroups { get; private set; }

		public IEnumerable<TypeViewModel> SearchResults
		{
			get
			{
                if (_searchResults != null)
                {
                    return _searchResults;
                }

				var results = WindowViewModel.TypesForSearch;

				if (!string.IsNullOrWhiteSpace(SearchTerm))
				{
					results = results.Where(SatisfiesSearchTerm);                    
				}

				switch (_searchMode)
				{
					case SearchMode.Interfaces:
						results = results.Where(t => t.TypeInfo.IsInterface);
						break;
					case SearchMode.ValueTypes:
						results = results.Where(t => t.TypeInfo.IsValueType);
						break;
					case SearchMode.Enums:
						results = results.Where(t => t.TypeInfo.IsEnum);
						break;
				}

				switch (_typeVisibilityFilter)
				{
					case TypeVisibility.Internal:
						results = results.Where(t => t.TypeInfo.IsInternal);
						break;
					case TypeVisibility.Public:
						results = results.Where(t => t.TypeInfo.IsPublic);
						break;
				}

                if (!_showAnonymousMethodTypes)
                {
                    results = results.Where(
                        t => t.Name.IndexOf("<>c__DisplayClass", StringComparison.InvariantCulture) == -1);
                }

				switch (_sortingMode)
				{
					case SortingMode.DescendantsCount:
						results = results.OrderByDescending(t => t.DescendantsCount);
						break;
					case SortingMode.MembersCount:
						results = results.OrderByDescending(t => t.MembersCount);
						break;
					case SortingMode.Name:
						results = results.OrderBy(t => t.Name);
						break;
				}

                if (!string.IsNullOrWhiteSpace(SearchTerm))
                {
                    results = results.Select(MarkSearchTerm);
                }
                else
                {
                    results = results.Select(ClearSearchTerm);
                }

                _searchResults = results.ToList();
                return _searchResults;
			}
		}

        public int ItemsCount
        {
            get
            {
                return SearchResults.Count();
            }
        }

		#region // Public methods

		public override void NotifyAssembliesChanged()
		{
			TriggerSearch();
		}

        public void NotifyAssemblySelectionChanged()
        {
            TriggerSearch();
        }

		public void FocusSearchField()
		{
			OnSearchFocusRequested();
		}        

		#endregion

		#region // Private methods

		public override void ShowInnerSearch()
		{
			OnSearchFocusRequested();
		}

		private bool SatisfiesSearchTerm(TypeViewModel typeViewModel)
		{
			return typeViewModel
				.Name.IndexOf(SearchTerm, StringComparison.InvariantCultureIgnoreCase) >= 0;

		}

        private TypeViewModel MarkSearchTerm(TypeViewModel type)
        {            
            var index = type.Name.IndexOf(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
            type.NameStart = type.Name.Substring(0, index);
            type.NameMiddle = type.Name.Substring(index, SearchTerm.Length);
            type.NameEnd = type.Name.Substring(index + SearchTerm.Length);
            
            return type;
        }

        public TypeViewModel ClearSearchTerm(TypeViewModel type)
        {
            type.ResetName();
            return type;
        }

		private void SearchTimerTick(object sender, EventArgs e)
		{
			_searchTimer.Stop();
			TriggerSearch();
			IsSearchPerformed = true;
		}

		private void TriggerSearch()
		{
            _searchResults = null;
			OnPropertyChanged("SearchResults");
            OnPropertyChanged("ItemsCount");
		}

		private void OnSearchFocusRequested()
		{
			var handler = SearchFocusRequested;
			if (handler != null)
			{
				handler();
			}
		}

		#endregion

		#region // Command handlers

        private void ClearSearchCommandHandler()
        {
            SearchTerm = string.Empty;
        }

		private void SortByName()
		{
			_sortingMode = SortingMode.Name;
			TriggerSearch();
		}

		private void SortByDescendantsCount()
		{
			_sortingMode = SortingMode.DescendantsCount;
			TriggerSearch();
		}

		private void SortByMembersCount()
		{
			_sortingMode = SortingMode.MembersCount;
			TriggerSearch();
		}

		private void ShowInterfaces()
		{
			_searchMode = SearchMode.Interfaces;
			TriggerSearch();
		}

		private void ShowValueTypes()
		{
			_searchMode = SearchMode.ValueTypes;
			TriggerSearch();
		}

		private void ShowEnums()
		{
			_searchMode = SearchMode.Enums;
			TriggerSearch();
		}

		private void ShowAllTypes()
		{
			_searchMode = SearchMode.All;
			TriggerSearch();
		}

		private void ShowAnyVisibility()
		{
			_typeVisibilityFilter = TypeVisibility.Any;
			TriggerSearch();
		}

		private void ShowPublicTypes()
		{
			_typeVisibilityFilter = TypeVisibility.Public;
			TriggerSearch();
		}

		private void ShowInternalTypes()
		{
			_typeVisibilityFilter = TypeVisibility.Internal;
			TriggerSearch();
		}

		#endregion
	}
}
