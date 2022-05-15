/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.AIModules.BehaviourTree.Attributes;
using AuroraFPSRuntime.Attributes;

namespace AuroraFPSRuntime.AIModules.BehaviourTree.Nodes
{
    [TreeNodeContent("Succeeder", "Decorators /Succeeder")]
    [HideScriptField]
    public class SucceederNode : DecoratorNode
    {
        protected override State OnUpdate()
        {
            child.Update();
            return State.Success;
        }
    }
}