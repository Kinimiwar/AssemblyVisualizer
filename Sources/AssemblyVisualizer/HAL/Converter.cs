﻿// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.HAL
{
    class Converter
    {
        private static IConverter _converter;

        static Converter()
        {
            #if ILSpy
            _converter = new ILSpy.Converter();
            #endif
            #if Reflector
            _converter = new Reflector.Converter();
            #endif
        }

        public static AssemblyInfo Assembly(object assembly)
        {
            return _converter.Assembly(assembly);
        }

        public static TypeInfo Type(object type)
        {
            return _converter.Type(type);
        }

        public static MethodInfo Method(object method)
        {
            return _converter.Method(method);
        }

        public static FieldInfo Field(object field)
        {
            return _converter.Field(field);
        }

        public static PropertyInfo Property(object property)
        {
            return _converter.Property(property);
        }

        public static EventInfo Event(object ev)
        {
            return _converter.Event(ev);
        }

        public static void ClearCache()
        {
            _converter.ClearCache();
        }
    }
}
