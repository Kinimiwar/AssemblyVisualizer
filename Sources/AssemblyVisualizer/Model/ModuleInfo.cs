// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Collections.Generic;

namespace AssemblyVisualizer.Model
{
	internal class ModuleInfo
	{
		public AssemblyInfo Assembly { get; set; }
		public IEnumerable<TypeInfo> Types { get; set; }
	}
}