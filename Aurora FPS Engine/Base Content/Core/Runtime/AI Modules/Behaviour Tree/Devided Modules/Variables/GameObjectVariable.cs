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
    public class GameObjectVariable : TreeVariable<GameObject>
    {
        public static implicit operator GameObjectVariable(GameObject value)
        {
            GameObjectVariable variable = new GameObjectVariable();
            variable.SetValue(value);
            return variable;
        }

        public static implicit operator GameObject(GameObjectVariable variable)
        {
            return variable.GetValue();
        }
    }
}