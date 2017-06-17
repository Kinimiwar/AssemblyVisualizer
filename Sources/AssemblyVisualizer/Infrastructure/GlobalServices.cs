// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Diagnostics;

namespace AssemblyVisualizer.Infrastructure
{
	internal static class GlobalServices
	{
		private const string SourcesUrl = @"http://denismarkelov.github.com/AssemblyVisualizer";

		public static void NavigateToSources()
		{
			Process.Start(SourcesUrl);
		}
	}
}