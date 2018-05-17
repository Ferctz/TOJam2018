using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    public delegate void BulletCollisionEventHandler(ShipBullet shipBullet);
    public delegate void ShatterBuildingCallback(float powerGained);

    /// <summary>
    /// Class that handles firing and movement of a bullet.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ShipBullet : MonoBehaviour
    {
        private Transform bulletTransform;
        public Transform BulletTransform { get { return bulletTransform ?? (bulletTransform = transform); } }

        public Rigidbody bulletRigidbody;
        public MeshRenderer bulletMesh;
        public FloatVariable bulletForce;
        public FloatVariable bulletImpulseForce;

        private Transform Transform;

        // event fired internally when this bullet collides with another collider
        public event BulletCollisionEventHandler OnBulletCollision;

        // callback for when this bullet destroys a building
        public ShatterBuildingCallback OnBuildingShatter;

        private void Awake()
        {
            Transform = transform;
        }

        private void FixedUpdate()
        {
            if (!bulletRigidbody || !bulletForce)
            {
                return;
            }
            bulletRigidbody.AddForce(Transform.forward * bulletForce.Value, ForceMode.Force);
        }

        /// <summary>
        /// Method resets rigidbody values, and applies impulse force to rigidbody.
        /// </summary>
        public void Fire()
        {
            if (!bulletRigidbody || !bulletMesh || !bulletImpulseForce)
            {
                return;
            }

            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;

            bulletRigidbody.isKinematic = false;
            bulletMesh.enabled = true;

            bulletRigidbody.AddForce(Transform.forward * bulletImpulseForce.Value, ForceMode.Impulse);
        }

        /// <summary>
        /// Method resets rigidbody values, forces a kinematic gameobject, and disables mesh.
        /// </summary>
        public void Sleep()
        {
            if (!bulletRigidbody || !bulletMesh)
            {
                return;
            }

            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;

            bulletRigidbody.isKinematic = true;
            bulletMesh.enabled = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Sleep();
            if (OnBulletCollision != null)
            {
                OnBulletCollision(this);
            }
        }
    }
}