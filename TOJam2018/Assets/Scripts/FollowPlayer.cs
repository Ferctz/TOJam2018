using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class FollowPlayer : MonoBehaviour
    {
        public Transform followTransform;
        public Vector3 offset;

        private Transform Transform;

        private Vector3 calculatedOffset;

        private void Awake()
        {
            Transform = transform;
        }

        private void LateUpdate()
        {
            /*calculatedOffset.x = offset.x * followTransform.forward.x;
            calculatedOffset.y = offset.y * followTransform.forward.y;
            calculatedOffset.z = offset.z * followTransform.forward.z;*/

            Transform.position = followTransform.position + (offset.x * followTransform.right) +
                                                            (offset.y * followTransform.up) +
                                                            (offset.z * followTransform.forward);
            Transform.rotation = followTransform.rotation;
        }
    }
}