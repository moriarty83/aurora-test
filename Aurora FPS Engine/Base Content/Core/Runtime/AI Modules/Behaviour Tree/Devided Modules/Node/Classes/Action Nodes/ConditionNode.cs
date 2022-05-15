/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.AIModules.BehaviourTree.Attributes;
using AuroraFPSRuntime.AIModules.BehaviourTree.Variables;
using AuroraFPSRuntime.Attributes;
using UnityEngine;

namespace AuroraFPSRuntime.AIModules.BehaviourTree.Nodes
{
    [TreeNodeContent("Condition", "Conditions/Condition")]
    [HideScriptField]
    public class ConditionNode : ActionNode
    {
        [SerializeField]
        [TreeVariable(typeof(bool))]
        private string booleanVariable;

        [SerializeField]
        [TreeVariable(typeof(bool))]
        private bool boolean;

        [SerializeField]
        private bool resetOnSuccess;

        #region [Variables Toggle]
#if UNITY_EDITOR
        [SerializeField]
        [HideInInspector]
        private bool booleanToggle;
#endif
        #endregion

        protected override State OnUpdate()
        {
            if (!string.IsNullOrEmpty(booleanVariable) && tree.TryGetVariable<BoolVariable>(booleanVariable, out BoolVariable boolVariable1))
            {
                boolean = boolVariable1;
            }

            if (boolean)
            {
                if (resetOnSuccess)
                {
                    boolean = false;
                    if (!string.IsNullOrEmpty(booleanVariable) && tree.TryGetVariable<BoolVariable>(booleanVariable, out BoolVariable boolVariable2))
                    {
                        boolVariable2.SetValue(false);
                    }
                }
                return State.Success;
            }

            return State.Failure;
        }
    }
}