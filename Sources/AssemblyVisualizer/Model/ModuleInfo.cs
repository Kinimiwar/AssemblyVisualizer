﻿// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyVisualizer.Model
{
    class ModuleInfo
    {
        public AssemblyInfo Assembly { get; set; }
        public IEnumerable<TypeInfo> Types { get; set; }
    }
}
