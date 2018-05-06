using System.Collections.Generic;
using UnityEngine;

namespace TOJAM2018.Gameplay
{    
    public class FireBullet : MonoBehaviour
    {
        private Transform Transform;
        private Transform dynamicTransform;

        public ShipBullet bulletPrefab;

        public Queue<ShipBullet> bulletQueue;
        private const int INITIAL_BULLET_POOL = 10;

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
            ShipBullet bullet = GetShipBullet();
            bullet.transform.position = Transform.position + (Transform.forward * 8f);
            bullet.transform.forward = Transform.forward;
            bullet.bulletCollisionEvent += OnBulletCollision;
            bullet.Fire();
        }

        private void OnBulletCollision(ShipBullet shipBullet)
        {
            bulletQueue.Enqueue(shipBullet);
        }
    }
}