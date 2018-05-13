using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    public delegate void BuildingDestroyedEventHandler(ShatterOnCollision building);

    /// <summary>
    /// This class handles spawning a new gameobject once this health reaches critial limit
    /// Spawns a new gameobject and destroys this one.
    /// </summary>
    public class ShatterOnCollision : MonoBehaviour
    {
        private Transform buildingTransform;
        public Transform BuildingTransform { get { return buildingTransform ?? (buildingTransform = transform); } }

        public ShatterableRuntimeSet shatterableRuntimeSet;

        public bool IsTargetBuilding { get; set; }
        public FloatVariable powerGain;
        public FloatVariable maxPowerGain;

        public GameObject shatterPrefab;
        public event BuildingDestroyedEventHandler OnBuildingDestroyed;

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

        /// <summary>
        /// When this is fired, it checks incoming collider type. If bullet, decrease health.
        /// If building health reaches critical limit, it will fire shattering callbacks.
        /// </summary>
        /// <param name="collision"> Computed collision. </param>
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
                        if (shipBullet.OnBuildingShatter != null)
                        {
                            // invoke bullet shatter callback based on power reward
                            shipBullet.OnBuildingShatter(IsTargetBuilding ? maxPowerGain.Value : powerGain.Value);
                        }
                    }

                    Shatter();
                }
            }
        }

        /// <summary>
        /// Method called when this builing health reaches critical health. Instatiates shattered gameobject.
        /// Invokes shattered event.
        /// </summary>
        private void Shatter()
        {
            GameObject.Instantiate(shatterPrefab, BuildingTransform.position, BuildingTransform.rotation);

            if (OnBuildingDestroyed != null)
            {
                OnBuildingDestroyed(this);
            }

            Destroy(gameObject);
        }
    }
}