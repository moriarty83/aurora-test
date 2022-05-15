/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.UIModules.UIElements.Animation;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AuroraFPSRuntime.CoreModules.SceneManagement
{
    public static class SceneUtility
    {
        private static string TargetScene = string.Empty;
        private static float _LoadingProgress = 0.0f;
        public static float LoadingProgress
        {
            get
            {
                return _LoadingProgress;
            }
        }

        /// <summary>
        /// Loads the target scene asynchronously in the background
        /// and activate it as soon as it is ready with transition.
        /// </summary>
        /// <param name="timeMultiplier">Artificially increase the loading time of the scene.</param>
        /// <param name="transition">Use animation transition effect.</param>
        public static IEnumerator LoadTargetSceneAsync(float timeMultiplier = 1.0f, Transition transition = null)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(TargetScene, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;

            while (_LoadingProgress < 1.0f)
            {
                _LoadingProgress = Mathf.MoveTowards(_LoadingProgress, asyncOperation.progress / 0.9f, timeMultiplier * Time.deltaTime);
                yield return null;
            }

            if (transition != null)
            {
                yield return transition.WaitForFadeIn();
            }

            asyncOperation.allowSceneActivation = true;
            TargetScene = string.Empty;
            _LoadingProgress = 0.0f;
        }

        /// <summary>
        /// Loads the scene asynchronously by specified name in the background. 
        /// Activate it as soon as it is ready with transition.
        /// </summary>
        /// <param name="targetScene">The scene to target upload.</param>
        /// <param name="loadingScene">
        /// Intermediate scene before uploading target scene.
        /// <br><b>Note: Loading scene must manually upload target scene by LoadTargetSceneAsync method.</b></br>
        /// <br><i>For that can be used <b>Target Scene Loader</b> component, which must be already added in loading scene.</i></br>
        /// </param>
        /// <param name="timeMultiplier">Artificially increase the loading time of the scene.</param>
        /// <param name="transition">Use animation transition effect.</param>
        public static IEnumerator LoadSceneAsync(string targetScene, string loadingScene = null, float timeMultiplier = 1.0f, Transition transition = null)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(!string.IsNullOrEmpty(loadingScene) ? loadingScene : targetScene, LoadSceneMode.Single);
            if(!string.IsNullOrEmpty(loadingScene))
            {
                TargetScene = targetScene;
            }

            asyncOperation.allowSceneActivation = false;

            while (_LoadingProgress < 1.0f)
            {
                _LoadingProgress = Mathf.MoveTowards(_LoadingProgress, asyncOperation.progress / 0.9f, timeMultiplier * Time.deltaTime);
                yield return null;
            }

            if (transition != null)
            {
                yield return transition.WaitForFadeIn();
            }

            asyncOperation.allowSceneActivation = true;
            _LoadingProgress = 0.0f;
        }
    }
}
