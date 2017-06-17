// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AssemblyVisualizer.Infrastructure;

namespace AssemblyVisualizer.Common.CommandsGroup
{
	/// <summary>
	///     Interaction logic for CommandsGroupView.xaml
	/// </summary>
	internal partial class CommandsGroupView : UserControl
	{
		// Using a DependencyProperty as the backing store for Commands.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandsProperty =
			DependencyProperty.Register("Commands", typeof(IEnumerable<UserCommand>), typeof(CommandsGroupView),
				new UIPropertyMetadata(null));

		// Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HeaderProperty =
			DependencyProperty.Register("Header", typeof(string), typeof(CommandsGroupView), new UIPropertyMetadata(""));

		public CommandsGroupView()
		{
			InitializeComponent();
		}

		public IEnumerable<UserCommand> Commands
		{
			get { return (IEnumerable<UserCommand>) GetValue(CommandsProperty); }
			set { SetValue(CommandsProperty, value); }
		}

		public string Header
		{
			get { return (string) GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}
	}
}