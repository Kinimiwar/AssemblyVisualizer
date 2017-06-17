// Adopted, originally created as part of GraphSharp library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.ComponentModel;

namespace AssemblyVisualizer.Controls.Graph.GraphSharp.OverlapRemoval
{
	public class OverlapRemovalParameters : IOverlapRemovalParameters
	{
		private float horizontalGap = 10;
		private float verticalGap = 10;

		public float VerticalGap
		{
			get { return verticalGap; }
			set
			{
				if (verticalGap != value)
				{
					verticalGap = value;
					NotifyChanged("VerticalGap");
				}
			}
		}

		public float HorizontalGap
		{
			get { return horizontalGap; }
			set
			{
				if (horizontalGap != value)
				{
					horizontalGap = value;
					NotifyChanged("HorizontalGap");
				}
			}
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}