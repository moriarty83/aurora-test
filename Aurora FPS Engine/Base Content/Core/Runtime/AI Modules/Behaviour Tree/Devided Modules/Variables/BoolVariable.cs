/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

namespace AuroraFPSRuntime.AIModules.BehaviourTree.Variables
{
    [System.Serializable]
    public class BoolVariable : TreeVariable<bool>
    {
        public static implicit operator BoolVariable(bool value)
        {
            BoolVariable variable = new BoolVariable();
            variable.SetValue(value);
            return variable;
        }

        public static implicit operator bool(BoolVariable variable)
        {
            return variable.GetValue();
        }
    }
}