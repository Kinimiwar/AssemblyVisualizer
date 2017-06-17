// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.Common
{
	internal class FieldViewModel : MemberViewModel
	{
		private readonly FieldInfo _fieldInfo;

		public FieldViewModel(FieldInfo fieldInfo) : base(fieldInfo)
		{
			_fieldInfo = fieldInfo;
		}
	}
}