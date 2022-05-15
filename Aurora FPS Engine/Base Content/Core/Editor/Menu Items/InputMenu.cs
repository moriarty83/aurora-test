/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.CoreModules.InputSystem;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AuroraFPSEditor
{
    internal static class InputMenu
    {
        [MenuItem("Aurora FPS Engine/Input/Open Input Map", false, 303)]
        public static void OpenInputMap()
        {
            InputActionAsset inputMap = Resources.LoadAll<InputActionAsset>(string.Empty).FirstOrDefault();
            if (inputMap != null)
            {
                AssetDatabase.OpenAsset(inputMap);
            }
            else
            {
                EditorUtility.DisplayDialog("Aurora FPS Engine", string.Format("Input action asset not found!\nCreate or move the current InputActionAsset to resources folder in your project."), "Ok");
            }
        }
    }
}
