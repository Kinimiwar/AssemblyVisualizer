using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AssemblyVisualizer.Infrastructure
{
    static class GlobalServices
    {
        private const string HomePageUri = @"http://visualizer.denismarkelov.com";

        public static void NavigateToHomePage()
        {
            Process.Start(HomePageUri);
        }
    }
}
