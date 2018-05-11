using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    [System.Serializable]
    public class BulletDestroyBuildingCallback : UnityEvent<float> { }

    public class FireBullet : MonoBehaviour
    {
        public BoolVariable isGameRunning;

        private Transform Transform;
        private Transform dynamicTransform;

        public ShipBullet bulletPrefab;

        public Queue<ShipBullet> bulletQueue;
        private const int INITIAL_BULLET_POOL = 10;

        public BulletDestroyBuildingCallback bulletDestroyBuildingEvent;

        private void Awake()
        {
            Transform = transform;
            dynamicTransform = GameObject.Find("Dynamic").transform;
            InitQueue();
        }

        private void InitQueue()
        {
            bulletQueue = new Queue<ShipBullet>();
            for (int i = 0; i < INITIAL_BULLET_POOL; i++)
            {
                ShipBullet pooledBullet = GameObject.Instantiate(bulletPrefab, dynamicTransform.position, Quaternion.identity, dynamicTransform);
                pooledBullet.Sleep();
                bulletQueue.Enqueue(pooledBullet);
            }
        }

        private ShipBullet GetShipBullet()
        {
            if (bulletQueue.Count > 0)
            {
                return bulletQueue.Dequeue();
            }
            else
            {
                ShipBullet pooledBullet = GameObject.Instantiate(bulletPrefab, dynamicTransform.position, Quaternion.identity);
                return pooledBullet;
            }
        }

        public void FireShipBullet()
        {
            if (!isGameRunning.Value)
            {
                return;
            }

            ShipBullet bullet = GetShipBullet();
            bullet.transform.position = Transform.position + (Transform.forward * 8f);
            bullet.transform.forward = Transform.forward;
            bullet.bulletCollisionEvent -= OnBulletCollision;
            bullet.bulletCollisionEvent += OnBulletCollision;
            bullet.shatterBuildingEvent -= OnBuildingShatter;
            bullet.shatterBuildingEvent += OnBuildingShatter; 
            bullet.Fire();
        }

        private void OnBulletCollision(ShipBullet shipBullet)
        {
            bulletQueue.Enqueue(shipBullet);
            shipBullet.bulletCollisionEvent -= OnBulletCollision;
        }

        /// <summary>
        /// Receive the message from the bullet that a building has been shattered, invoke this public event
        /// </summary>
        private void OnBuildingShatter(float powerGained)
        {
            bulletDestroyBuildingEvent.Invoke(powerGained);
        }
    }
}