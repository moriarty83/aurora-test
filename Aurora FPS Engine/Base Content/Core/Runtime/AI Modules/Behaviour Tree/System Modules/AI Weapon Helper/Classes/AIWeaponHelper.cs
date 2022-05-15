using AuroraFPSRuntime.Attributes;
using UnityEngine;

namespace AuroraFPSRuntime.AIModules
{
    [HideScriptField]
    [AddComponentMenu("Aurora FPS Engine/AI Modules/Common/Weapon/AI Weapon Helper")]
    public sealed class AIWeaponHelper : MonoBehaviour
    {
        [SerializeField]
        [NotNull]
        private Transform weaponObject;

        // Stored required properties.
        private Vector3 startPosition;
        private Quaternion startRotation;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            startPosition = weaponObject.transform.localPosition;
            startRotation = weaponObject.transform.localRotation;
        }

        /// <summary>
        /// Reset weapon to start position.
        /// </summary>
        public void ResetPosition()
        {
            weaponObject.localPosition = startPosition;
            weaponObject.localRotation = startRotation;
        }
    }
}