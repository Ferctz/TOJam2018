using UnityEngine;

namespace TOJAM2018.Gameplay
{
    /// <summary>
    /// Class that follows a transform based on an offset. Movement is done in LateUpdate.
    /// </summary>
    [RequireComponent(typeof(Camera))]
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

        private void Awake()
        {
            Transform = transform;
        }
        
        /// <summary>
        /// Movement and rotation translations done in LateUpdate to avoid rendering jitter.
        /// </summary>
        private void LateUpdate()
        {
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