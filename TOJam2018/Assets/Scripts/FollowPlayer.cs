using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField]
        private Transform followTransform;
        public Transform FollowTransform
        {
            get { return followTransform; }
            set { followTransform = value; }
        }

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

            if (followTransform == null)
            {
                return;
            }

            Transform.position = followTransform.position + (offset.x * followTransform.right) +
                                                            (offset.y * followTransform.up) +
                                                            (offset.z * followTransform.forward);
            Transform.rotation = followTransform.rotation;
        }
    }
}