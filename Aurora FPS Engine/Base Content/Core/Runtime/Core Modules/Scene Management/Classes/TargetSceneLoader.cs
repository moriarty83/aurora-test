/* ================================================================
  ----------------------------------------------------------------
  Project   :   Aurora FPS Engine
  Publisher :   Infinite Dawn
  Developer :   Tamerlan Shakirov
  ----------------------------------------------------------------
  Copyright © 2017 Tamerlan Shakirov All rights reserved.
  ================================================================ */

using AuroraFPSRuntime.Attributes;
using AuroraFPSRuntime.CoreModules.Coroutines;
using AuroraFPSRuntime.UIModules.UIElements.Animation;
using System.Collections;
using UnityEngine;

namespace AuroraFPSRuntime.CoreModules.SceneManagement
{
    [HideScriptField]
    [AddComponentMenu("Aurora FPS Engine/Core Modules/Scene Management/Target Scene Loader")]
    [DisallowMultipleComponent]
    public sealed class TargetSceneLoader : MonoBehaviour
    {
        [SerializeField]
        [Order(501)]
        private Transition transition = null;

        [SerializeField]
        [Slider(0.001f, 1.0f)]
        [Order(601)]
        private float timeMultiplier = 0.25f;

        // Stored required properties.
        private CoroutineObject coroutineObject;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            coroutineObject = new CoroutineObject(this);
            coroutineObject.Start(LoadProcessing);
        }

        /// <summary>
        /// Loading processing with transition.
        /// </summary>
        private IEnumerator LoadProcessing()
        {
            yield return SceneUtility.LoadTargetSceneAsync(timeMultiplier, transition);
        }

        #region [Getter / Setter]
        public Transition GetTransition()
        {
            return transition;
        }

        public void SetTransition(Transition value)
        {
            transition = value;
        }

        public float GetTimeMultiplier()
        {
            return timeMultiplier;
        }

        public void SetTimeMultiplier(float value)
        {
            timeMultiplier = value;
        }
        #endregion
    }
}
