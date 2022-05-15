/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using UnityEngine;

namespace AuroraFPSRuntime.AIModules.BehaviourTree.Variables
{
    [System.Serializable]
    public class StringVariable : TreeVariable<string>
    {
        public static implicit operator StringVariable(string value)
        {
            StringVariable variable = new StringVariable();
            variable.SetValue(value);
            return variable;
        }

        public static implicit operator string(StringVariable variable)
        {
            return variable.GetValue();
        }
    }
}