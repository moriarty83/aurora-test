/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.Attributes;
using System;
using UnityEngine;
using UnityEngine.Events;


namespace AuroraFPSRuntime.SystemModules.HealthModules
{
    [HideScriptField]
    [AddComponentMenu("Aurora FPS Engine/System Modules/Health/Object Health")]
    [DisallowMultipleComponent]
    public class ObjectHealth : HealthComponent
    {
        [Serializable]
        public class OnTakeDamageEvent : UnityEvent<float> { }

        // Base object health properties.
        [SerializeField]
        [Slider("minHealth", "maxHealth")]
        private float health = 100;

        [SerializeField]
        [MinValue(0)]
        private float minHealth = 0;

        [SerializeField]
        private float maxHealth = 100;

        [SerializeField]
        [Foldout("Event Callbacks", Style = "Header")]
        [Order(500)]
        protected OnTakeDamageEvent onTakeDamageEvent;

        [SerializeField]
        [Foldout("Event Callbacks", Style = "Header")]
        [Order(501)]
        protected UnityEvent onDeadEvent;

        [SerializeField]
        [Foldout("Event Callbacks", Style = "Header")]
        [Order(502)]
        protected UnityEvent onReviveEvent;

        /// <summary>
        /// Called when the script instance is being loaded
        /// </summary>
        protected virtual void Awake()
        {
            OnTakeDamageCallback += onTakeDamageEvent.Invoke;
            OnDeadCallback += onDeadEvent.Invoke;
            OnReviveCallback += onReviveEvent.Invoke;
        }

        /// <summary>
        /// Called once when object health become zero.
        /// Implement this method to make custom death logic.
        /// </summary>
        protected virtual void OnDead()
        {

        }

        /// <summary>
        /// Called when object health become more then zero.
        /// Implement this method to make revive logic.
        /// </summary>
        protected virtual void OnRevive()
        {

        }

        /// <summary>
        /// Apply new health points.
        /// </summary>
        /// <param name="amount">Health amount.</param>
        public virtual void ApplyHealth(float amount)
        {
            SetHealth(health + Mathf.Abs(amount));
        }

        #region [Private Internal Methods]
        /// <summary>
        /// Internal OnDead method to call addition event callback.
        /// </summary>
        private void Internal_OnDead()
        {
            OnDead();
            OnDeadCallback?.Invoke();
        }

        /// <summary>
        /// Internal OnRevive method to call addition event callback.
        /// </summary>
        private void Internal_OnRevive()
        {
            OnRevive();
            OnReviveCallback?.Invoke();
        }
        #endregion

        #region [IHealth Implemetation]
        /// <summary>
        /// Get current health point.
        /// </summary>
        public override float GetHealth()
        {
            return health;
        }

        /// <summary>
        /// Alive state of health object.
        /// </summary>
        /// <returns>
        /// True if health > 0.
        /// Otherwise false.
        /// </returns>
        public override bool IsAlive()
        {
            return health > 0;
        }
        #endregion

        #region [IDamageable Implementation]
        /// <summary>
        /// Take damage to the health.
        /// </summary>
        /// <param name="amount">Damage amount.</param>
        /// <param name="damageInfo">Additional damage info.</param>
        public override void TakeDamage(float amount, DamageInfo damageInfo = null)
        {
            SetHealth(health - amount);
            OnTakeDamageCallback?.Invoke(amount);
            if(damageInfo != null)
            {
                OnTakeDamageInfoCallback?.Invoke(amount, damageInfo);
            }
        }
        #endregion

        #region [Event Callback Functions]
        /// <summary>
        /// Called when an object takes damage.
        /// </summary>
        public event Action<float> OnTakeDamageCallback;

        /// <summary>
        /// Called when an object takes damage.
        /// </summary>
        public event Action<float, DamageInfo> OnTakeDamageInfoCallback;

        /// <summary>
        /// Called when object is die.
        /// </summary>
        public event Action OnDeadCallback;

        /// <summary>
        /// Called when object is revive.
        /// </summary>
        public event Action OnReviveCallback;
        #endregion

        #region [Getter / Setter]
        public void SetHealth(float value)
        {
            float previousHealth = health;
            health = Mathf.Clamp(value, minHealth, maxHealth);
            if (previousHealth > 0 && health == 0)
            {
                Internal_OnDead();
            }
            else if(previousHealth == 0 && health > 0)
            {
                Internal_OnRevive();
            }
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public void SetMaxHealth(float value)
        {
            maxHealth = value;
        }

        public float GetMinHealth()
        {
            return minHealth;
        }

        public void SetMinHealth(float value)
        {
            minHealth = value >= 0 ? value : 0;
        }
        #endregion
    }
}