using System.Collections;
using UnityEngine;
using ScriptableObjects;
using Random = System.Random;

namespace TOJAM2018.Gameplay
{
    /// <summary>
    /// Class that holds a reference to runtime set of buildings currently in the scene
    /// and assigns a new target, once at the start and when the current target is destroyed.
    /// </summary>
    public class BuildingAI : MonoBehaviour
    {
        public PlayerRuntimeSet playerRuntimeSet;
        public ShatterableRuntimeSet shatterableRuntimeSet;

        [SerializeField]
        private ShatterOnCollision currentTarget;
        private int targetIndex = -1;

        public GameEvent gameEndEvent;

        private const float MAXIMUM_BUILDING_DISTANCE = 300f;
        private bool buildingCloseToPlayers = true;

        private Random rand;

        private void Awake()
        {
            rand = new Random((int)System.DateTime.Now.Ticks);
        }

        /// <summary>
        /// On Start of this gameobject, it adds listeners to the building destroyed event on each
        /// building. Waits 0.5s to then set the first building target. 
        /// </summary>
        private void Start()
        {
            if (shatterableRuntimeSet == null)
            {
                return;
            }
            
            for (int i = 0; i < shatterableRuntimeSet.Items.Count; i++)
            {
                shatterableRuntimeSet.Items[i].OnBuildingDestroyed += BuildingDestroyed;
            }

            StartCoroutine(WaitDurationThenAction(0.5f, () => SetBuildingTarget()));
        }

        /// <summary>
        /// IEnumerator that waits waitDuration and after completion fires endAction.
        /// </summary>
        /// <param name="waitDuration"> Wait time in seconds. </param>
        /// <param name="endAction"> Action to fire at end of wait duration. </param>
        /// <returns></returns>
        IEnumerator WaitDurationThenAction(float waitDuration, System.Action endAction)
        {
            yield return new WaitForSeconds(waitDuration);
            endAction();
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

            // reset target index
            targetIndex = -1;

            // iterate over all buildings and grab first building that is 
            // within MAXIMIUM_BUILDING_DISTANCE
            for (int i = 0; i < shatterableRuntimeSet.Items.Count; i++)
            {
                if (currentTarget == shatterableRuntimeSet.Items[i])
                {
                    continue;
                }

                buildingCloseToPlayers = true;
                for (int j = 0; j < playerRuntimeSet.Items.Count; j++)
                {
                    buildingCloseToPlayers &= Vector3.Distance(shatterableRuntimeSet.Items[i].BuildingTransform.position, 
                                                                playerRuntimeSet.Items[j].PlayerTransform.position) < 
                                                                MAXIMUM_BUILDING_DISTANCE;
                }

                if (buildingCloseToPlayers)
                {
                    // nearby building found, save its index and stop searching
                    targetIndex = i;
                    break;
                }
            }

            if ((targetIndex >= 0 || shatterableRuntimeSet.Items.Count > 0)) // other buildings exist but not close to players
            {
                currentTarget = targetIndex >= 0 ? shatterableRuntimeSet.Items[targetIndex] : 
                                                    shatterableRuntimeSet.Items[rand.Next(0, shatterableRuntimeSet.Items.Count)];

                currentTarget.IsTargetBuilding = true;

                // start building material flicker on this new target
                FlickerMaterial buildingFlickerMaterial = currentTarget.GetComponent<FlickerMaterial>();
                if (buildingFlickerMaterial)
                {
                    buildingFlickerMaterial.StartFlicker();
                }

                Debug.Log("Building set!");
            }
        }

        /// <summary>
        /// Method called when a building from the runtime set is destroyed.
        /// Removes building from runtime set, evaluates end condition, and resumes
        /// search for a new buildint target.
        /// </summary>
        /// <param name="buildingDestroyed"></param>
        private void BuildingDestroyed(ShatterOnCollision buildingDestroyed)
        {
            // remove destroyed building manually from runtime set
            shatterableRuntimeSet.Items.Remove(buildingDestroyed);
            if (currentTarget == buildingDestroyed)
            {
                currentTarget = null;
            }
            
            // evaluate game end condition
            if (shatterableRuntimeSet.Items.Count == 0)
            {
                if (gameEndEvent != null)
                {
                    gameEndEvent.Raise();
                    return;
                }
            }

            // if buildings remain, find new target
            if (currentTarget == null)
            {
                SetBuildingTarget();
            }            
        }
    }
}