using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    [System.Serializable]
    public class BulletDestroyBuildingEventHandler : UnityEvent<float> { }

    /// <summary>
    /// Class that handles pooling/instantiation/firing of ShipBullets. Also Fires an event 
    /// when a ShipBullet successfully destroys a building.
    /// </summary>
    public class FireBullet : MonoBehaviour
    {
        public BoolVariable isGameRunning;

        private Transform Transform;
        private Transform dynamicTransform;

        public ShipBullet bulletPrefab;

        public Queue<ShipBullet> bulletQueue;
        private const int INITIAL_BULLET_POOL = 10;

        public BulletDestroyBuildingEventHandler OnBulletDestroyBuilding;

        private void Awake()
        {
            Transform = transform;
            dynamicTransform = GameObject.Find("Dynamic").transform;
            InitQueue();
        }

        /// <summary>
        /// Populates a queue with INITIAL_BULLET_POOL amount of ShipBullets. 
        /// </summary>
        private void InitQueue()
        {
            bulletQueue = new Queue<ShipBullet>();
            for (int i = 0; i < INITIAL_BULLET_POOL; i++)
            {
                ShipBullet pooledBullet = GameObject.Instantiate(bulletPrefab, 
                                                                dynamicTransform.position, 
                                                                Quaternion.identity, 
                                                                dynamicTransform);
                pooledBullet.Sleep();
                bulletQueue.Enqueue(pooledBullet);
            }
        }

        /// <summary>
        /// Returns a bullet from bulletQueue if not empty, else instantiates a bullet.
        /// </summary>
        /// <returns> Bullet class instance from queue or instantiation. </returns>
        private ShipBullet GetShipBullet()
        {
            if (bulletQueue.Count > 0)
            {
                return bulletQueue.Dequeue();
            }
            else
            {
                ShipBullet pooledBullet = GameObject.Instantiate(bulletPrefab, 
                                                                dynamicTransform.position, 
                                                                Quaternion.identity);
                return pooledBullet;
            }
        }

        /// <summary>
        /// Public method to fire a bullet. Should be linked to a GameEventListener listening for an input event;
        /// </summary>
        public void FireShipBullet()
        {
            if (!isGameRunning.Value)
            {
                return;
            }

            ShipBullet bullet = GetShipBullet();
            bullet.transform.position = Transform.position + (Transform.forward * 8f);
            bullet.transform.forward = Transform.forward;

            bullet.OnBulletCollision -= RemoveBullet;
            bullet.OnBulletCollision += RemoveBullet;

            bullet.OnBuildingShatter -= BuildingShatter;
            bullet.OnBuildingShatter += BuildingShatter; 
            bullet.Fire();
        }

        /// <summary>
        /// Remove a bullet from bulletQueue.
        /// </summary>
        /// <param name="shipBullet"> Bullet to remove. </param>
        private void RemoveBullet(ShipBullet shipBullet)
        {
            bulletQueue.Enqueue(shipBullet);
            shipBullet.OnBulletCollision -= RemoveBullet;
        }

        /// <summary>
        /// Receive the message from the bullet that a building has been shattered, invoke this public event.
        /// </summary>
        private void BuildingShatter(float powerGained)
        {
            OnBulletDestroyBuilding.Invoke(powerGained);
        }
    }
}