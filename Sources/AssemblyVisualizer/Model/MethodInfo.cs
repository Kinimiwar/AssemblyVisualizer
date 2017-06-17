// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

namespace AssemblyVisualizer.Model
{
	internal class MethodInfo : MemberInfo
	{
		public bool IsVirtual { get; set; }
		public bool IsOverride { get; set; }
		public bool IsSpecialName { get; set; }
		public bool IsFinal { get; set; }
	}
}