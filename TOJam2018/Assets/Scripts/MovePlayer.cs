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

        public BoolVariable invertY;

        public BoolVariable boost;

        public FloatVariable forwardForce;
        public FloatVariable torqueForce;

        public FloatVariable correctiveTorque;

        private Vector3 torque = Vector3.zero;

        public Rigidbody playerRigidbody;

        private float thrustMultiplier = 1f;
        private bool cooldown = false;

        private void Awake()
        {
            playerTransform = transform;
        }

        private void Update()
        {
            if (!cooldown && boost.Value)
            {
                thrustMultiplier += Time.deltaTime * 10f;
                Mathf.Clamp(thrustMultiplier, 1f, 5f);

                if (thrustMultiplier > 25f)
                {
                    if (!cooldown)
                    {
                        cooldown = true;
                    }
                }
            }

            if (cooldown)
            {
                thrustMultiplier -= Time.deltaTime * 20f;
                if (thrustMultiplier <= 1f)
                {
                    thrustMultiplier = 1f;
                    cooldown = false;
                }
            }
        }

        private void FixedUpdate()
        {
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
    }
}