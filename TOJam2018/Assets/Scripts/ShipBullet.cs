using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    public delegate void BulletCollisionCallback(ShipBullet shipBullet);
    public delegate void ShatterBuildingCallback(float powerGained);

    [RequireComponent(typeof(Rigidbody))]
    public class ShipBullet : MonoBehaviour
    {
        public Rigidbody bulletRigidbody;
        public MeshRenderer bulletMesh;
        public FloatVariable bulletForce;
        public FloatVariable bulletImpulseForce;

        private Transform Transform;

        public BulletCollisionCallback bulletCollisionEvent;
        public ShatterBuildingCallback shatterBuildingEvent;

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
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;

            bulletRigidbody.isKinematic = false;
            bulletMesh.enabled = true;

            bulletRigidbody.AddForce(Transform.forward * bulletImpulseForce.Value, ForceMode.Impulse);
        }

        public void Sleep()
        {
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;

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