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
    public class QuaternionVariable : TreeVariable<Quaternion>
    {
        public static implicit operator QuaternionVariable(Quaternion value)
        {
            QuaternionVariable variable = new QuaternionVariable();
            variable.SetValue(value);
            return variable;
        }

        public static implicit operator Quaternion(QuaternionVariable variable)
        {
            return variable.GetValue();
        }
    }
}