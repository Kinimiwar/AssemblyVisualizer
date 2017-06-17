// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Windows.Input;
using System.Windows.Media;
using AssemblyVisualizer.HAL;
using AssemblyVisualizer.Infrastructure;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.Common
{
	internal abstract class MemberViewModel : ViewModelBase
	{
		private SolidColorBrush _background;
		private SolidColorBrush _foreground;
		private bool _isMarked;
		private string _toolTip;

		public MemberViewModel(MemberInfo memberInfo)
		{
			MemberInfo = memberInfo;

			JumpCommand = new DelegateCommand(JumpCommandHandler);
		}

		public bool IsMarked
		{
			get { return _isMarked; }
			set
			{
				_isMarked = value;
				OnPropertyChanged("IsMarked");
			}
		}

		public ICommand JumpCommand { get; private set; }

		public virtual string ToolTip
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_toolTip))
					return Text;
				return _toolTip;
			}
			set { _toolTip = value; }
		}

		public virtual string Text
		{
			get { return MemberInfo.Text; }
		}

		public virtual ImageSource Icon
		{
			get { return MemberInfo.Icon; }
		}

		public MemberInfo MemberInfo { get; private set; }

		public object MemberReference
		{
			get { return MemberInfo.MemberReference; }
		}

		public bool IsVisibleOutsideFamily
		{
			get { return MemberInfo.IsPublic || MemberInfo.IsInternal || MemberInfo.IsProtectedOrInternal; }
		}

		public bool IsPublic
		{
			get { return MemberInfo.IsPublic; }
		}

		public bool IsProtected
		{
			get { return MemberInfo.IsProtected; }
		}

		public bool IsInternal
		{
			get { return MemberInfo.IsInternal; }
		}

		public bool IsPrivate
		{
			get { return MemberInfo.IsPrivate; }
		}

		public bool IsProtectedInternal
		{
			get { return MemberInfo.IsProtectedOrInternal; }
		}

		public SolidColorBrush Background
		{
			get { return _background; }
			set
			{
				_background = value;
				OnPropertyChanged("Background");
			}
		}

		public SolidColorBrush Foreground
		{
			get { return _foreground; }
			set
			{
				_foreground = value;
				OnPropertyChanged("Foreground");
			}
		}

		private void JumpCommandHandler()
		{
			Services.JumpTo(MemberReference);
		}
	}
}