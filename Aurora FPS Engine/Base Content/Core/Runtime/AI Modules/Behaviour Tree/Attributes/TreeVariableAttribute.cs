/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.Attributes;
using System;

namespace AuroraFPSRuntime.AIModules.BehaviourTree.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class TreeVariableAttribute : ViewAttribute
    {
        public readonly Type[] Types;

        public TreeVariableAttribute()
        {
            Types = null;
            Variable = string.Empty;
        }

        public TreeVariableAttribute(params Type[] types) : base()
        {
            Types = types;
        }

        #region [Optional parameters]
        public string Variable { get; set; }
        #endregion
    }
}