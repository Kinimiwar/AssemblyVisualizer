#if ILSpy

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.ILSpy;
using System.Windows.Input;
using AssemblyVisualizer.About;

namespace AssemblyVisualizer.HAL.ILSpy
{
    [ExportMainMenuCommand(Menu = "_Visualizer", Header = "_About", MenuOrder = 1.5)]
    public class ShowAboutCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }
    }
}

#endif