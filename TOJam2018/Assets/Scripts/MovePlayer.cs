using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovePlayer : MonoBehaviour
    {
        private Transform playerTransform;

        public FloatVariable horizontal;
        public FloatVariable vertical;

        public FloatVariable forwardForce;
        public FloatVariable torqueForce;

        private Vector3 torque = Vector3.zero;

        public Rigidbody playerRigidbody;

        [SerializeField]
        private float rigidbodyVelocityMagnitude;

        private void Awake()
        {
            playerTransform = transform;
        }

        private void FixedUpdate()
        {
            playerRigidbody.AddForce(playerTransform.forward * forwardForce.Value);
            torque.x = 0f + (-vertical.Value);
            torque.y = 0f + horizontal.Value;
            playerRigidbody.AddRelativeTorque(torque * torqueForce.Value);

            rigidbodyVelocityMagnitude = playerRigidbody.velocity.magnitude;
        }
    }
}