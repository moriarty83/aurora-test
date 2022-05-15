/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.AIModules.BehaviourTree.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace AuroraFPSRuntime.AIModules.BehaviourTree
{
    public class TreeGroup : ScriptableObject
    {
        #region [Editor Section]
#if UNITY_EDITOR
        [HideInInspector]
        public string title;

        [SerializeReference]
        [HideInInspector]
        public List<TreeNode> nodes = new List<TreeNode>();

        [HideInInspector]
        public BehaviourTreeAsset tree;
#endif
        #endregion

    }
}