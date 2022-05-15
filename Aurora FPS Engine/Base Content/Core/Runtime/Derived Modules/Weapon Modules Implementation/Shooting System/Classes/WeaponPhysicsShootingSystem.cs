/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Infinite Dawn
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright © 2017 Tamerlan Shakirov All rights reserved.
   ================================================================ */

using AuroraFPSRuntime.Attributes;
using AuroraFPSRuntime.CoreModules.PhysicsEngine;
using AuroraFPSRuntime.SystemModules;
using System;
using UnityEngine;

namespace AuroraFPSRuntime.WeaponModules
{
    [HideScriptField]
    [AddComponentMenu("Aurora FPS Engine/Weapon Modules/Shooting/Physics Shooting System")]
    [DisallowMultipleComponent]
    public class WeaponPhysicsShootingSystem : WeaponShootingSystem
    {
        [SerializeField]
        [NotNull]
        private PhysicsBullet bullet;

        [SerializeField]
        [MinValue(1.0f)]
        private float bulletSpeedMultiplier = 1.15f;

        // Stored required components.
        private PoolManager poolManager;
        private PhysicsBullet _bullet;

        // Stored required properties.
        private LayerMask cullingLayer;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Debug.Assert(bullet != null, $"<b><color=#FF0000>.Physics bullet prefab is required!\nAttach reference of physics bullet prefab to {gameObject.name}<i>(gameobject)</i> -> Physics Shotgun Shooting System <i>(component)</i> -> Bullet<i>(field)</i>.</color></b>");

            poolManager = PoolManager.GetRuntimeInstance();

            LayerMask physicsShellLayer = LayerMask.NameToLayer("Physics Shell");
            cullingLayer = PhysicsCollisionMatrix.LoadMaskForLayer(physicsShellLayer.value);
        }

        /// <summary>
        /// Implement this method to make logic of shooting. 
        /// </summary>
        /// <param name="origin">Origin vection of shoot.</param>
        /// <param name="direction">Direction vector of shoot.</param>
        protected override void MakeShoot(Vector3 origin, Vector3 direction)
        {
            PhysicsBullet physicsBullet = poolManager.CreateOrPop<PhysicsBullet>(bullet, origin, Quaternion.LookRotation(direction));
            physicsBullet.SetOwner(transform.root);
            physicsBullet.ApplySpeed(direction, bulletSpeedMultiplier);
            OnFireBullet(physicsBullet);
        }

        /// <summary>
        /// Culling layer of calculation shoot direction.
        /// </summary>
        protected override LayerMask GetDirectionLayer()
        {
            return cullingLayer;
        }

        #region [Bullet Action Wrappers]
        private void OnFireBullet(PhysicsBullet bullet)
        {
            OnFireBulletCallback?.Invoke(bullet);

            _bullet = bullet;
            bullet.OnHitCallback += OnHitCallback;
            bullet.OnDamageCallback += OnDamageCallback;
            bullet.OnKillCallback += OnKillCallback;
            bullet.OnBeforePushCallback += BulletPooled;
        }

        private void BulletPooled()
        {
            _bullet.OnHitCallback -= OnHitCallback;
            _bullet.OnDamageCallback -= OnDamageCallback;
            _bullet.OnKillCallback -= OnKillCallback;
            _bullet.OnBeforePushCallback -= BulletPooled;
        }
        #endregion

        #region [Event Callback Functions]
        /// <summary>
        /// Called when weapon fired and instantiating new physics bullet.
        /// </summary>
        /// <param name="PhysicsBullet">Fired bullet instance.</param>
        public event Action<PhysicsBullet> OnFireBulletCallback;

        /// <summary>
        /// Called when bullet become collide any of other collider.
        /// </summary>
        /// <param name="Transform">The Transform data associated with this collision.</param>
        public event Action<Transform> OnHitCallback;

        /// <summary>
        /// Called when bullet has become collide any of component which implemented of IDamageable interface.
        /// </summary>
        /// <param name="Transform">The Transform data associated with health.</param>
        public event Action<Transform> OnDamageCallback;

        /// <summary>
        /// Called when bull has become kill any of component which implemented of IHealth interface.
        /// </summary>
        /// <param name="Transform">The Transform data associated with health.</param>
        public event Action<Transform> OnKillCallback;
        #endregion

        #region [Getter / Setter]
        public PhysicsBullet GetBullet()
        {
            return bullet;
        }

        public void SetBullet(PhysicsBullet value)
        {
            bullet = value;
        }

        public float GetImpulseAmplifier()
        {
            return bulletSpeedMultiplier;
        }

        public void SetImpulseAmplifier(float value)
        {
            bulletSpeedMultiplier = value;
        }

        public LayerMask GetCullingLayer()
        {
            return cullingLayer;
        }
        #endregion
    }
}