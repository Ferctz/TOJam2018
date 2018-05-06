using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    public delegate void BuildingDestroyedCallback();

    public class ShatterOnCollision : MonoBehaviour
    {
        private Transform Transform;

        public ShatterableRuntimeSet shatterableRuntimeSet;

        public GameObject shatterPrefab;
        public BuildingDestroyedCallback buildingDestroyedEvent;

        public IntVariable hitsUntilShatter;
        private int hits;

        private int bulletLayer = 0;

        private void Awake()
        {
            Transform = transform;

            hits = hitsUntilShatter.Value;

            bulletLayer = LayerMask.NameToLayer("Bullet");
        }

        private void OnEnable()
        {
            if (shatterableRuntimeSet != null)
            {
                shatterableRuntimeSet.Items.Add(this);
            }
        }

        private void OnDisable()
        {
            if (shatterableRuntimeSet != null)
            {
                shatterableRuntimeSet.Items.Remove(this);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.layer == bulletLayer)
            {
                hits--;
                if (hits <= 0)
                {
                    Shatter();
                }
            }
        }

        private void Shatter()
        {
            GameObject.Instantiate(shatterPrefab, Transform.position, Transform.rotation);

            if (buildingDestroyedEvent != null)
            {
                buildingDestroyedEvent();
            }

            Destroy(gameObject);
        }
    }
}