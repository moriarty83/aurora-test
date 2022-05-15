/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSEditor.Attributes;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace AuroraFPSEditor.Internal.Integrations
{
    [Integration("Emerald AI 3.0")]
    public sealed class EmeraldAIIntegrationEditor : IntegrationEditor
    {
        private readonly string PackageRelativePath = "Base Content/Core/Editor/Editor Resources/Library Assets/Integrations/Emerald AI 3.0.unitypackage";
        private readonly string InstalledRelativePath = "Integrations/Emerald AI 3.0";

        private bool hasPackage;
        private bool isInstalled;

        public EmeraldAIIntegrationEditor()
        {
            ApexSettings settings = ApexSettings.Current;
            string packagePath = Path.Combine(settings.GetRootPath(), PackageRelativePath);
            string installedPath = Path.Combine(settings.GetRootPath(), InstalledRelativePath);

            hasPackage = File.Exists(packagePath);
            isInstalled = Directory.Exists(installedPath);
        }

        public override void OnGUI(Rect position)
        {
            if (!isInstalled)
            {
                EditorGUI.BeginDisabledGroup(!hasPackage);
                if (RightButton(ref position, "Install"))
                {
                    if (EditorUtility.DisplayDialog("Attention", "Before installing the add-on, make sure that Emerald AI is installed in your project. " +
                "Otherwise, you will get a compilation error.", "Continue", "Cancel"))
                    {
                        ApexSettings settings = ApexSettings.Current;
                        string packagePath = Path.Combine(settings.GetRootPath(), PackageRelativePath);
                        AssetDatabase.ImportPackage(packagePath, false);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                }
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                if (RightButton(ref position, "Uninstall"))
                {
                    if (EditorUtility.DisplayDialog("Attention", "Are you really want to delete Emerald AI 3.0 add-on?", "Yes", "No"))
                    {
                        if (Directory.Exists(InstalledRelativePath))
                        {
                            ApexSettings settings = ApexSettings.Current;
                            string installedPath = Path.Combine(settings.GetRootPath(), InstalledRelativePath);
                            Directory.Delete(installedPath, true);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                    }
                }
            }
        }
    }
}