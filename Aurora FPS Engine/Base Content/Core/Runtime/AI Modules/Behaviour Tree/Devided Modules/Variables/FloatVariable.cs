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
    public class FloatVariable : TreeVariable<float>
    {
        public static implicit operator FloatVariable(float value)
        {
            FloatVariable variable = new FloatVariable();
            variable.SetValue(value);
            return variable;
        }

        public static implicit operator float(FloatVariable variable)
        {
            return variable.GetValue();
        }
    }
}