/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using System;

namespace AuroraFPSRuntime.AIModules.BehaviourTree.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TreeNodeContentAttribute : Attribute
    {
        public readonly string Name;
        public readonly string Path;

        public TreeNodeContentAttribute(string name, string path)
        {
            Name = name;
            Path = path;
            Hide = false;
        }

        #region [Optional Parameters]
        public bool Hide { get; set; }
        #endregion
    }
}