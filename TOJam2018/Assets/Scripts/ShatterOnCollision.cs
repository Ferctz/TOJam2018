using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    public delegate void BuildingDestroyedCallback(ShatterOnCollision building);

    public class ShatterOnCollision : MonoBehaviour
    {
        private Transform buildingTransform;
        public Transform BuildingTransform { get { return buildingTransform ?? (buildingTransform = transform); } }

        public ShatterableRuntimeSet shatterableRuntimeSet;

        public bool IsTargetBuilding { get; set; }
        public FloatVariable powerGain;
        public FloatVariable maxPowerGain;

        public GameObject shatterPrefab;
        public BuildingDestroyedCallback buildingDestroyedEvent;

        public IntVariable hitsUntilShatter;
        private int hits = -1;

        private int bulletLayer = 0;

        private void Awake()
        {
            hits = 0;

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
                hits++;
                if (hits >= hitsUntilShatter.Value)
                {
                    ShipBullet shipBullet = collision.collider.GetComponentInParent<ShipBullet>();
                    if (shipBullet)
                    {
                        if (shipBullet.shatterBuildingEvent != null)
                        {
                            shipBullet.shatterBuildingEvent(IsTargetBuilding ? maxPowerGain.Value : powerGain.Value);
                        }
                    }

                    Shatter();
                }
            }
        }

        private void Shatter()
        {
            GameObject.Instantiate(shatterPrefab, BuildingTransform.position, BuildingTransform.rotation);

            if (buildingDestroyedEvent != null)
            {
                buildingDestroyedEvent(this);
            }

            Destroy(gameObject);
        }
    }
}