// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

namespace AssemblyVisualizer.AncestryBrowser
{
	internal class MemberOptions
	{
		public bool ShowFields { get; set; }
		public bool ShowProperties { get; set; }
		public bool ShowEvents { get; set; }
		public bool ShowMethods { get; set; }
		public bool ShowPublic { get; set; }
		public bool ShowInternal { get; set; }
		public bool ShowProtected { get; set; }
		public bool ShowPrivate { get; set; }
		public bool ShowProtectedInternal { get; set; }
		public string SearchTerm { get; set; }
		public MemberKind MemberKind { get; set; }
	}
}