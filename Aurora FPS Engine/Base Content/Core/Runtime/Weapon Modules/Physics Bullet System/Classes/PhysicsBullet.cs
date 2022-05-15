/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.Attributes;
using AuroraFPSRuntime.SystemModules;
using AuroraFPSRuntime.SystemModules.HealthModules;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AuroraFPSRuntime.WeaponModules
{
    [HideScriptField]
    [AddComponentMenu("Aurora FPS Engine/Weapon Modules/Physics Shell/Physics Bullet")]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [DisallowMultipleComponent]
    public class PhysicsBullet : PoolObject
    {
        /// <summary>
        /// Store of instance id's which has been already damaged by player.
        /// </summary>
        protected static readonly HashSet<int> DamagedInstanceIDs = new HashSet<int>();

        /// <summary>
        /// Store of instance id's which already killed by player and OnKillCallback has been called for them.
        /// </summary>
        protected static readonly HashSet<int> KilledInstanceIDs = new HashSet<int>();

        // Base physics shell properties.
        [SerializeField]
        [NotNull]
        private BulletItem bulletItem;

        [SerializeField]
        private float bulletSpeed = 50;

        // Stored required components.
        private Transform owner;
        private new Rigidbody rigidbody;
        private new Collider collider;


        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
        }

        public virtual void ApplySpeed(Vector3 direction)
        {
            rigidbody.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }

        public virtual void ApplySpeed(Vector3 direction, float speedMultiplier)
        {
            rigidbody.AddForce(direction * (bulletSpeed + speedMultiplier), ForceMode.Impulse);
        }

        /// <summary>
        /// Called before pushing object to pool.
        /// </summary>
        protected override void OnBeforePush()
        {
            base.OnBeforePush();
            rigidbody.velocity = Vector3.zero;
        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        protected virtual void OnCollisionEnter(Collision other)
        {
            Transform otherTransform = other.transform;
            SendDamage(other, bulletItem.GetDamage());
            SendImpulse(otherTransform, bulletItem.GetImpactImpulse());
            Decal.Spawn(bulletItem.GetDecalMapping(), other.contacts[0]);
            Push();
            OnHitCallback?.Invoke(otherTransform);
        }

        /// <summary>
        /// Calculate damage relative collision.
        /// </summary>
        protected virtual void SendDamage(Collision collision, float damage)
        {
            Transform other = collision.transform;
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                ContactPoint contact = collision.GetContact(0);
                damageable.TakeDamage(bulletItem.GetDamage(), new DamageInfo(owner, contact.point, contact.normal));

                if (other.TryGetComponent<IHealth>(out IHealth health))
                {
                    int instanceID = other.root.GetInstanceID();
                    if (health.IsAlive())
                    {
                        DamagedInstanceIDs.Add(instanceID);
                        KilledInstanceIDs.Remove(instanceID);
                        OnDamageCallback?.Invoke(other);
                    }
                    else if (DamagedInstanceIDs.Remove(instanceID) &&
                            KilledInstanceIDs.Add(instanceID))
                    {
                        OnKillCallback?.Invoke(other);
                    }
                }
            }
        }

        /// <summary>
        /// Send damage to specified transform.
        /// </summary>
        protected virtual void SendDamage(Transform other, float damage)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(bulletItem.GetDamage());

                if (other.TryGetComponent<IHealth>(out IHealth health))
                {
                    int instanceID = other.root.GetInstanceID();
                    if (health.IsAlive())
                    {
                        DamagedInstanceIDs.Add(instanceID);
                        KilledInstanceIDs.Remove(instanceID);
                        OnDamageCallback?.Invoke(other);
                    }
                    else if (DamagedInstanceIDs.Remove(instanceID) &&
                            KilledInstanceIDs.Add(instanceID))
                    {
                        OnKillCallback?.Invoke(other);
                    }
                }
            }
        }

        /// <summary>
        /// Trying to send physics impulse force to transform.
        /// </summary>
        protected virtual void SendImpulse(Transform other, float impulse)
        {
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                otherRigidbody.AddForce(transform.forward * impulse, ForceMode.Impulse);
            }
        }

        #region [Event Callback Functions]
        /// <summary>
        /// Called when shell become collide any of other collider.
        /// </summary>
        /// <param name="Transform">The Transform data associated with this collision.</param>
        public event Action<Transform> OnHitCallback;

        /// <summary>
        /// Called when shell has become collide any of component which implemented of IDamageable interface.
        /// </summary>
        /// <param name="Transform">The Transform data associated with health.</param>
        public event Action<Transform> OnDamageCallback;

        /// <summary>
        /// Called when shell has become kill any of component which implemented of IHealth interface.
        /// </summary>
        /// <param name="Transform">The Transform data associated with health.</param>
        public event Action<Transform> OnKillCallback;
        #endregion

        #region [Getter / Setter]
        public Transform GetOwner()
        {
            return owner;
        }

        public void SetOwner(Transform value)
        {
            owner = value;
        }

        public BulletItem GetBulletItem()
        {
            return bulletItem;
        }

        public void SetBulletItem(BulletItem value)
        {
            bulletItem = value;
        }

        public Rigidbody GetRigidbody()
        {
            return rigidbody;
        }

        public Collider GetCollider()
        {
            return collider;
        }
        #endregion
    }
}