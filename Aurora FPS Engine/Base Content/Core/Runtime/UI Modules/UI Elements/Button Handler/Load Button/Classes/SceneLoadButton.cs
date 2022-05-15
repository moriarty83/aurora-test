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
using AuroraFPSRuntime.CoreModules.SceneManagement;
using UnityEngine;

namespace AuroraFPSRuntime.UIModules.UIElements
{
    [HideScriptField]
    [AddComponentMenu("Aurora FPS Engine/UI Modules/UI Elements/Scene Management/Button/Scene Load Button")]
    [DisallowMultipleComponent]
    public sealed class SceneLoadButton : UnityButton
    {
        [SerializeField]
        [SceneSelecter]
        private string targetScene = string.Empty;

        [SerializeField]
        [SceneSelecter]
        private string loadingScene = string.Empty;

        [SerializeField]
        private Animation.Transition fadeTransition;

        // Stored required properties.
        private CoroutineObject<string, string, float, Animation.Transition> coroutineObject;

        protected override void Awake()
        {
            base.Awake();
            coroutineObject = new CoroutineObject<string, string, float, Animation.Transition>(this);
        }

        /// <summary>
        /// Called before invoking OnClick events.
        /// </summary>
        protected override void OnBeforeClick()
        {
            coroutineObject.Start(SceneUtility.LoadSceneAsync, targetScene, loadingScene, 1.0f, fadeTransition);
        }

        #region [Unity Editor]
#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/Aurora FPS Engine/UI/Scene Management/Scene Load Button", false, 30)]
        private static void CreateButton()
        {
            GameObject button = UnityEngine.UI.DefaultControls.CreateButton(new UnityEngine.UI.DefaultControls.Resources());
            button.name = "Scene Load Button";

            DestroyImmediate(button.GetComponent<UnityEngine.UI.Button>());
            button.AddComponent<SceneLoadButton>();

            button.GetComponent<UnityEngine.UI.Image>().sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Load";

            Canvas canvas = FindObjectOfType<Canvas>();
            if(canvas == null)
            {
                GameObject canvasGO = new GameObject();
                canvasGO.name = "Canvas";
                canvasGO.AddComponent<Canvas>();

                canvas = canvasGO.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGO.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasGO.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }

            button.transform.SetParent(canvas.transform);
            button.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

            UnityEditor.Selection.activeGameObject = button;
            UnityEditor.EditorGUIUtility.PingObject(button);
        }

#endif
        #endregion

        #region [Getter / Setter]
        public string GetTargetScene()
        {
            return targetScene;
        }

        public void SetTargetScene(string value)
        {
            targetScene = value;
        }

        public string GetLoadingScene()
        {
            return loadingScene;
        }

        public void SetLoadingScene(string value)
        {
            loadingScene = value;
        }

        public Animation.Transition GetFadeTransition()
        {
            return fadeTransition;
        }

        public void SetFadeTransition(Animation.Transition value)
        {
            fadeTransition = value;
        }
        #endregion
    }
}
