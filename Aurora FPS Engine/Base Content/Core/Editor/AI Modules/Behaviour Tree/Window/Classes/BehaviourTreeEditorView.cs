/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Davleev Zinnur
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using UnityEngine.UIElements;

namespace AuroraFPSEditor.AIModules.BehaviourTree
{
    public class BehaviourTreeEditorView : VisualElement
    {
        public BehaviourTreeEditor behaviourTreeEditor;

        public void SetBehaviourTreeEditor(BehaviourTreeEditor editor)
        {
            behaviourTreeEditor = editor;
        }
    }
}