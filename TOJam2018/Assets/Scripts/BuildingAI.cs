using System.Collections;
using UnityEngine;
using ScriptableObjects;
using Random = System.Random;

namespace TOJAM2018.Gameplay
{
    public class BuildingAI : MonoBehaviour
    {
        public PlayerRuntimeSet playerRuntimeSet;
        public ShatterableRuntimeSet shatterableRuntimeSet;

        [SerializeField]
        private ShatterOnCollision currentTarget;
        private int targetIndex = -1;

        public GameEvent gameEndEvent;

        private const float MAXIMUM_BUILDING_DISTANCE = 500f;
        private bool buildingCloseToPlayers = true;

        private Random rand;

        private void Awake()
        {
            rand = new Random((int)System.DateTime.Now.Ticks);
        }

        private void Start()
        {
            StartCoroutine(WaitToSetFirstTarget());
        }

        IEnumerator WaitToSetFirstTarget()
        {
            yield return new WaitForSeconds(0.5f);
            SetBuildingTarget();
        }

        /// <summary>
        /// Sets a new target for players to destroy, favouring those MAXIMUM_BUILDING_DISTANCE away from both players
        /// </summary>
        private void SetBuildingTarget()
        {
            if (shatterableRuntimeSet == null)
            {
                return;
            }

            targetIndex = -1;
            for (int i = 0; i < shatterableRuntimeSet.Items.Count; i++)
            {
                if (currentTarget == shatterableRuntimeSet.Items[i])
                {
                    continue;
                }

                buildingCloseToPlayers = true;
                for (int j = 0; j < playerRuntimeSet.Items.Count; j++)
                {
                    buildingCloseToPlayers &= Vector3.Distance(shatterableRuntimeSet.Items[i].BuildingTransform.position, playerRuntimeSet.Items[j].PlayerTransform.position) < MAXIMUM_BUILDING_DISTANCE;
                }

                if (buildingCloseToPlayers)
                {
                    // nearby building found, save its index and stop searching
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex >= 0 || shatterableRuntimeSet.Items.Count > 0) // buildings exist but not close to players
            {
                currentTarget = targetIndex >= 0 ? shatterableRuntimeSet.Items[targetIndex] : 
                                                    shatterableRuntimeSet.Items[rand.Next(0, shatterableRuntimeSet.Items.Count)];

                FlickerMaterial buildingFlickerMaterial = currentTarget.GetComponent<FlickerMaterial>();
                buildingFlickerMaterial.StartFlicker();

                currentTarget.buildingDestroyedEvent += SetBuildingTarget;

                Debug.Log("Building set!");
            }
            else // no more buildings left to destroy
            {
                gameEndEvent.Raise();
            }
        }
    }
}