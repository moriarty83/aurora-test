/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.Attributes;
using AuroraFPSRuntime.SystemModules.HealthModules;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AuroraFPSRuntime.WeaponModules
{
    [HideScriptField]
    [AddComponentMenu("Aurora FPS Engine/Weapon Modules/Shooting/Raycast Shooting System")]
    [DisallowMultipleComponent]
    public class WeaponRayShootingSystem : WeaponShootingSystem
    {
        /// <summary>
        /// Store of instance id's which has been already damaged by player.
        /// </summary>
        protected static readonly HashSet<int> DamagedInstanceIDs = new HashSet<int>();

        /// <summary>
        /// Store of instance id's which already killed by player and OnKillCallback has been called for them.
        /// </summary>
        protected static readonly HashSet<int> KilledInstanceIDs = new HashSet<int>();

        [SerializeField]
        [NotNull]
        private BulletItem bulletItem;

        [SerializeField] 
        [MinValue(0.0f)]
        private float fireRange = 500.0f;

        [SerializeField] 
        private LayerMask cullingLayer = Physics.AllLayers;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Debug.Assert(bulletItem != null, $"<b><color=#FF0000>.Bullet item reference is required!\nAttach reference of bullet item to {gameObject.name}<i>(gameobject)</i> -> Ray Shotgun Shooting System <i>(component)</i> -> Bullet Item<i>(field)</i>.</color></b>");
        }

        /// <summary>
        /// Implement this method to make logic of shooting. 
        /// </summary>
        /// <param name="origin">Origin vection of shoot.</param>
        /// <param name="direction">Direction vector of shoot.</param>
        protected override void MakeShoot(Vector3 origin, Vector3 direction)
        {
            if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, fireRange, cullingLayer, QueryTriggerInteraction.Ignore))
            {
                Decal.Spawn(bulletItem.GetDecalMapping(), hitInfo);
                SendDamage(hitInfo);
                AddImpulseForce(hitInfo.transform, direction);
                OnFireRayCallback?.Invoke(hitInfo);
            }
        }

        /// <summary>
        /// Maximum range of calculation shoot direction.
        /// </summary>
        protected override float GetDirectionRange()
        {
            return fireRange;
        }

        /// <summary>
        /// Culling layer of calculation shoot direction.
        /// </summary>
        protected override LayerMask GetDirectionLayer()
        {
            return cullingLayer;
        }

        /// <summary>
        /// Send damage to transform containing health component.
        /// </summary>
        private void SendDamage(RaycastHit hitInfo)
        {
            Transform other = hitInfo.transform;
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(bulletItem.GetDamage(), new DamageInfo(transform.root, hitInfo.point, hitInfo.normal));

                if (other.TryGetComponent<IHealth>(out IHealth health))
                {
                    int instanceID = other.root.GetInstanceID();
                    if (health.IsAlive())
                    {
                        DamagedInstanceIDs.Add(instanceID);
                        KilledInstanceIDs.Remove(instanceID);
                        OnDamageCallback?.Invoke(hitInfo.transform);
                    }
                    else if (DamagedInstanceIDs.Remove(instanceID) &&
                            KilledInstanceIDs.Add(instanceID))
                    {
                        OnKillCallback?.Invoke(hitInfo.transform);
                    }
                }
            }
        }

        /// <summary>
        /// Add impulse force to rigidbody transform.
        /// </summary>
        private void AddImpulseForce(Transform other, Vector3 direction)
        {
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(direction * bulletItem.GetImpactImpulse(), ForceMode.Impulse);
            }
        }

        #region [Event Callback Functions]
        /// <summary>
        /// Called when ray receive hit info result.
        /// </summary>
        /// <param name="RaycastHit">Fire raycast hit info.</param>
        public event Action<RaycastHit> OnFireRayCallback;

        /// <summary>
        /// Called when ray being damage any alive object with health component.
        /// </summary>
        /// <param name="Transform">The Transform data associated with health.</param>
        public event Action<Transform> OnDamageCallback;

        /// <summary>
        /// Called when ray being kill any object with health component.
        /// </summary>
        /// <param name="Transform">The Transform data associated with health.</param>
        public event Action<Transform> OnKillCallback;
        #endregion

        #region [Getter / Setter]
        public BulletItem GetBulletItem()
        {
            return bulletItem;
        }

        public void SetBulletItem(BulletItem value)
        {
            bulletItem = value;
        }

        public float GetFireRange()
        {
            return fireRange;
        }

        public void SetFireRange(float value)
        {
            fireRange = value;
        }

        public LayerMask GetCullingLayer()
        {
            return cullingLayer;
        }

        public void SetCullingLayer(LayerMask value)
        {
            cullingLayer = value;
        }
        #endregion
    }
}