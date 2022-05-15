/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.Attributes;
using AuroraFPSRuntime.CoreModules.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace AuroraFPSRuntime.UIModules.UIElements
{
    [HideScriptField]
    [AddComponentMenu("Aurora FPS Engine/UI Modules/UI Elements/Scene Management/Loading Progress/Text Loading Progress")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public sealed class TextLoadingProgress : MonoBehaviour
    {
        [SerializeField]
        private string format = "F0";

        // Stored required components.
        private Text text;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            text = GetComponent<Text>();
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        private void Update()
        {
            text.text = (SceneUtility.LoadingProgress * 100).ToString(format);
        }
    }
}
