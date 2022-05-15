/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.CoreModules.Serialization.Collections;
using UnityEngine;

namespace AuroraFPSRuntime.AIModules.BehaviourTree.Variables
{
    [System.Serializable]
    public class VariablesContainer : SerializableDictionary<string, TreeVariable>
    {
        [SerializeField]
        private string[] keys;

        [SerializeReference]
        private TreeVariable[] values;

        protected override string[] GetKeys()
        {
            return keys;
        }

        protected override TreeVariable[] GetValues()
        {
            return values;
        }

        protected override void SetKeys(string[] keys)
        {
            this.keys = keys;
        }

        protected override void SetValues(TreeVariable[] values)
        {
            this.values = values;
        }
    }
}