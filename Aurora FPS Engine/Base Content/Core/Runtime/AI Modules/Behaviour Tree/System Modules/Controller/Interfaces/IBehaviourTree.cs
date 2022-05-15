/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.AIModules.BehaviourTree;

namespace AuroraFPSRuntime.AIModules
{
    public interface IBehaviourTree
    {
        /// <summary>
        /// Returns the behavior tree used.
        /// </summary>
        BehaviourTreeAsset GetBehaviourTree();
    }
}