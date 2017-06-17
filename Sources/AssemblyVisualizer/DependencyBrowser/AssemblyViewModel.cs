// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Collections.Generic;
using System.Windows.Input;
using AssemblyVisualizer.Infrastructure;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.DependencyBrowser
{
	internal class AssemblyViewModel : ViewModelBase
	{
		private static readonly Dictionary<AssemblyInfo, AssemblyViewModel> _correspondence =
			new Dictionary<AssemblyInfo, AssemblyViewModel>();

		private bool _isFound;
		private bool _isSelected;
		private readonly IList<AssemblyViewModel> _referencedAssemblies = new List<AssemblyViewModel>();

		private AssemblyViewModel(AssemblyInfo assembly)
		{
			AssemblyInfo = assembly;
			_correspondence.Add(assembly, this);
			foreach (var assemblyInfo in AssemblyInfo.ReferencedAssemblies)
				_referencedAssemblies.Add(Create(assemblyInfo));

			ToggleSelectionCommand = new DelegateCommand(ToggleSelectionCommandHandler);
		}

		public ICommand ToggleSelectionCommand { get; private set; }

		public bool IsProcessed { get; set; }
		public bool IsMarked { get; set; }
		public bool IsRemoved { get; set; }
		public bool IsRoot { get; set; }

		public string Name
		{
			get { return AssemblyInfo.Name; }
		}

		public string FullName
		{
			get { return AssemblyInfo.FullName; }
		}

		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				_isSelected = value;
				OnPropertyChanged("IsSelected");
			}
		}

		public bool IsFound
		{
			get { return _isFound; }
			set
			{
				_isFound = value;
				OnPropertyChanged("IsFound");
			}
		}

		public IEnumerable<AssemblyViewModel> ReferencedAssemblies
		{
			get { return _referencedAssemblies; }
		}

		public AssemblyInfo AssemblyInfo { get; private set; }

		public static AssemblyViewModel Create(AssemblyInfo assemblyInfo)
		{
			if (_correspondence.ContainsKey(assemblyInfo))
				return _correspondence[assemblyInfo];
			return new AssemblyViewModel(assemblyInfo);
		}

		public static void ClearCache()
		{
			_correspondence.Clear();
		}

		private void ToggleSelectionCommandHandler()
		{
			IsSelected = !IsSelected;
		}
	}
}