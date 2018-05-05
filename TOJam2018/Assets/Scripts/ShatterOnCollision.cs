using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class ShatterOnCollision : MonoBehaviour
    {
        public GameObject shatterPrefab;

        private Transform Transform;

        private void Awake()
        {
            Transform = transform;
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject.Instantiate(shatterPrefab, Transform.position, Transform.rotation);

            Destroy(gameObject);
        }
    }
}