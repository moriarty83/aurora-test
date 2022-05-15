/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

namespace AuroraFPSRuntime.SystemModules.HealthModules
{
    public interface IDamageable
    {
        /// <summary>
        /// Take damage to the health.
        /// </summary>
        /// <param name="amount">Damage amount.</param>
        /// <param name="damageInfo">Additional damage information.</param>
        void TakeDamage(float amount, DamageInfo damageInfo = null);
    }
}