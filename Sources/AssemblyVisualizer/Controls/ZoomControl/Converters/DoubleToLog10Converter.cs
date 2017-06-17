﻿// Adopted, originally created as part of WPFExtensions library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Globalization;
using System.Windows.Data;

namespace AssemblyVisualizer.Controls.ZoomControl.Converters
{
	public class DoubleToLog10Converter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = (double) value;
			return Math.Log10(val);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = (double) value;
			return Math.Pow(10, val);
		}

		#endregion
	}
}