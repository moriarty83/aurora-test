/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.CoreModules;
using AuroraFPSRuntime.CoreModules.Serialization.Collections;
using AuroraFPSRuntime.SystemModules.ControllerSystems;
using UnityEngine;

namespace AuroraFPSRuntime.WeaponModules
{
    [System.Serializable]
    [System.Obsolete("Use Recoil Config instead.")]
    public class RecoilDictionary : SerializableDictionary<ControllerState, RecoilPattern>
    {
        [SerializeField]
        private ControllerState[] keys;

        [SerializeField]
        private RecoilPattern[] values;

        protected override ControllerState[] GetKeys()
        {
            return keys;
        }

        protected override RecoilPattern[] GetValues()
        {
            return values;
        }

        protected override void SetKeys(ControllerState[] keys)
        {
            this.keys = keys;
        }

        protected override void SetValues(RecoilPattern[] values)
        {
            this.values = values;
        }
    }

    [System.Obsolete("Use Recoil Config instead.")]
    public class RecoilMapping : ScriptableMappingDictionary<RecoilDictionary, ControllerState, RecoilPattern> { }
}