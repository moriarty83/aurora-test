/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using UnityEngine;

namespace AuroraFPSRuntime.SystemModules.HealthModules
{
    public class DamageInfo
    {
        public readonly Transform sender;
        public readonly Vector3 point;
        public readonly Vector3 normal;

        public DamageInfo(Transform sender, Vector3 point, Vector3 normal)
        {
            this.sender = sender;
            this.point = point;
            this.normal = normal;
        }

        public override string ToString()
        {
            return $"Damage Hit: [Sender: {sender.name} | Point: {point} | Normal: {normal}]";
        }
    }
}