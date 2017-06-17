﻿// Adopted, originally created as part of WPFExtensions library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

namespace AssemblyVisualizer.Controls.ZoomControl
{
    public enum ZoomViewModifierMode
    {
        /// <summary>
        /// It does nothing at all.
        /// </summary>
        None,

        /// <summary>
        /// You can pan the view with the mouse in this mode.
        /// </summary>
        Pan,

        /// <summary>
        /// You can zoom in with the mouse in this mode.
        /// </summary>
        ZoomIn, 

        /// <summary>
        /// You can zoom out with the mouse in this mode.
        /// </summary>
        ZoomOut,

        /// <summary>
        /// Zooming after the user has been selected the zooming box.
        /// </summary>
        ZoomBox
    }
}
