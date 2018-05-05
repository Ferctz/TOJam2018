using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    public delegate void BulletCollisionCallback(ShipBullet shipBullet);

    [RequireComponent(typeof(Rigidbody))]
    public class ShipBullet : MonoBehaviour
    {
        public Rigidbody bulletRigidbody;
        public MeshRenderer bulletMesh;
        public FloatVariable bulletForce;

        private Transform Transform;

        public BulletCollisionCallback bulletCollisionEvent;

        private void Awake()
        {
            Transform = transform;
        }

        private void FixedUpdate()
        {
            bulletRigidbody.AddForce(Transform.forward * bulletForce.Value, ForceMode.Force);
        }

        public void Fire()
        {
            bulletRigidbody.isKinematic = false;
            bulletMesh.enabled = true;
        }

        public void Sleep()
        {
            bulletRigidbody.isKinematic = true;
            bulletMesh.enabled = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Sleep();
            if (bulletCollisionEvent != null)
            {
                bulletCollisionEvent(this);
            }
        }
    }
}