using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovePlayer : MonoBehaviour
    {
        public BoolVariable isGameRunning;

        private Transform playerTransform;

        public FloatVariable horizontal;
        public FloatVariable vertical;

        public BoolVariable invertY;

        public BoolVariable boost;

        public FloatVariable forwardForce;
        public FloatVariable torqueForce;

        public FloatVariable correctiveTorque;

        private Vector3 torque = Vector3.zero;

        public Rigidbody playerRigidbody;

        private float thrustMultiplier = 1f;
        private const float MAX_THRUST_MULTIPLIER = 40f;

        private void Awake()
        {
            playerTransform = transform;
        }

        private void Update()
        {
            if (!isGameRunning.Value)
            {
                return;
            }

            if (boost.Value)
            {
                thrustMultiplier += Time.deltaTime * 25f;
            }
            else
            {
                thrustMultiplier -= Time.deltaTime * 40f;
            }

            thrustMultiplier = Mathf.Clamp(thrustMultiplier, 1f, MAX_THRUST_MULTIPLIER);
        }

        private void FixedUpdate()
        {
            if (!isGameRunning.Value)
            {
                return;
            }

            playerRigidbody.AddForce(playerTransform.forward * (forwardForce.Value * thrustMultiplier));
            torque.x = 0f + (invertY.Value ? vertical.Value : -vertical.Value);
            torque.y = 0f + horizontal.Value;
            playerRigidbody.AddRelativeTorque(torque * torqueForce.Value);

            Quaternion softRot = Quaternion.FromToRotation(playerTransform.up, Vector3.up);
            playerRigidbody.AddTorque(new Vector3(softRot.x, softRot.y, softRot.z) * correctiveTorque.Value);
        }

        public void ToggleInvertY()
        {
            invertY.Value = !invertY.Value;
        }

        public void FreezePlayer()
        {
            playerRigidbody.isKinematic = true;
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }
    }
}