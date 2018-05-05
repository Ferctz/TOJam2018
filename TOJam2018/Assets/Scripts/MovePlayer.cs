using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovePlayer : MonoBehaviour
    {
        private Transform playerTransform;

        public FloatVariable xAxis;
        public FloatVariable yAxis;

        public FloatVariable xMouse;
        public FloatVariable yMouse;

        public FloatVariable forwardForce;
        public FloatVariable torqueForce;

        private Vector3 torque = Vector3.zero;

        public Rigidbody playerRigidbody;

        // Should the rig steer along the Y-axis, up and down. Should be false for characters that move along the
        // ground, but true for spaceships or airoplanes.
        public bool YAxis = true;

        public float rigidbodyVelocityMagnitude;

        private void Awake()
        {
            playerTransform = transform;
        }

        private void FixedUpdate()
        {
            //playerRigidbody.AddForce(xAxis.Value * SIDE_THRUST, yAxis.Value * SIDE_THRUST, FORWARD_THRUST, ForceMode.Force);
            playerRigidbody.AddForce(playerTransform.forward * forwardForce.Value);
            torque.x = 0f + (-yMouse.Value);
            torque.y = 0f + xMouse.Value;
            playerRigidbody.AddRelativeTorque(torque * torqueForce.Value);

            // Try to keep up vector facing sky
            if (torque.magnitude == 0f && YAxis)
            {
                /*var zRot = playerRigidbody.rotation.eulerAngles.z;
                if (zRot > 180f) zRot -= 360f;
                var zForce = Mathf.Clamp(zRot / 10f, -1f, 1f) * CORRECTIVE_FORCE;
                playerRigidbody.AddTorque(playerRigidbody.transform.forward * -zForce);*/

                /*Quaternion m_RotationDelta = Quaternion.Euler(0f, 0f, -playerTransform.eulerAngles.z);
                playerRigidbody.MoveRotation(playerTransform.rotation * m_RotationDelta);*/
            }

            /*playerRigidbody.AddTorque(0, xMouse.Value * TORQUE_FORCE, 0);
            playerRigidbody.AddRelativeTorque(-yMouse.Value * TORQUE_FORCE, 0, 0);
            Vector3 properRight = Quaternion.Euler(0, 0, -playerTransform.localEulerAngles.z) * playerTransform.right;
            Vector3 uprightCorrection = Vector3.Cross(playerTransform.right, properRight);
            playerRigidbody.AddRelativeTorque(uprightCorrection * CORRECTIVE_FORCE);*/

            rigidbodyVelocityMagnitude = playerRigidbody.velocity.magnitude;
        }
    }
}