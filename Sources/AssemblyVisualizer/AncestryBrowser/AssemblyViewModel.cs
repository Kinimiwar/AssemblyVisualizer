﻿// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.AncestryBrowser
{
    class AssemblyViewModel
    {
        private AssemblyInfo _assemblyInfo;
        private IEnumerable<TypeViewModel> _types;

        public AssemblyViewModel(AssemblyInfo assemblyInfo, IEnumerable<TypeViewModel> types)
        {
            _assemblyInfo = assemblyInfo;
            _types = types;

            foreach (var type in _types)
            {
                type.Assembly = this;
            }
        }

        public string Name { get { return _assemblyInfo.Name; } }
        public string FullName { get { return _assemblyInfo.FullName; } }
        public IEnumerable<TypeViewModel> Types { get { return _types; } }
        public Brush BackgroundBrush { get; set; }
        public Brush CaptionBrush { get; set; }
    }
}
