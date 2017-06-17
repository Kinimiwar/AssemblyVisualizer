// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

namespace AssemblyVisualizer.Model
{
	internal class FieldInfo : MemberInfo
	{
		public bool IsInitOnly { get; set; }
		public bool IsSpecialName { get; set; }
		public bool IsLiteral { get; set; }
	}
}