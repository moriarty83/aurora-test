﻿/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using System;

namespace AuroraFPSRuntime.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public abstract class LayoutAttribute : ApexBaseAttribute
    {
        public readonly string name;

        protected LayoutAttribute(string name)
        {
            this.name = name;
        }
    }
}
