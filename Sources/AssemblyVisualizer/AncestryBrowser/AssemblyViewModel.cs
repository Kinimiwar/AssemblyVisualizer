// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Collections.Generic;
using System.Windows.Media;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.AncestryBrowser
{
	internal class AssemblyViewModel
	{
		private readonly AssemblyInfo _assemblyInfo;

		public AssemblyViewModel(AssemblyInfo assemblyInfo, IEnumerable<TypeViewModel> types)
		{
			_assemblyInfo = assemblyInfo;
			Types = types;

			foreach (var type in Types)
				type.Assembly = this;
		}

		public string Name
		{
			get { return _assemblyInfo.Name; }
		}

		public string FullName
		{
			get { return _assemblyInfo.FullName; }
		}

		public IEnumerable<TypeViewModel> Types { get; private set; }
		public Brush BackgroundBrush { get; set; }
		public Brush CaptionBrush { get; set; }
	}
}