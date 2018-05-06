using System.Collections;
using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class BuildingAI : MonoBehaviour
    {
        public PlayerRuntimeSet playerRuntimeSet;
        public ShatterableRuntimeSet shatterableRuntimeSet;

        [SerializeField]
        private ShatterOnCollision currentTarget;

        private const float MAXIMUM_BUILDING_DISTANCE = 300f;
        private bool buildingCloseToPlayers = true;

        private void Start()
        {
            StartCoroutine(WaitToSetFirstTarget());
        }

        IEnumerator WaitToSetFirstTarget()
        {
            yield return new WaitForSeconds(1f);
            SetBuildingTarget();
        }

        /// <summary>
        /// Sets a new target for players to destroy, making sure it is at least MAXIMUM_BUILDING_DISTANCE away from both players
        /// </summary>
        private void SetBuildingTarget()
        {
            if (shatterableRuntimeSet == null ||
                shatterableRuntimeSet.Items.Count == 0)
            {
                return;
            }

            buildingCloseToPlayers = true;
            for (int i = 0; i < shatterableRuntimeSet.Items.Count; i++)
            {
                for (int j = 0; j < playerRuntimeSet.Items.Count; j++)
                {
                    buildingCloseToPlayers &= Vector3.Distance(shatterableRuntimeSet.Items[i].transform.position, playerRuntimeSet.Items[j].PlayerTransform.position) < MAXIMUM_BUILDING_DISTANCE;
                }

                if (buildingCloseToPlayers)
                {
                    currentTarget = shatterableRuntimeSet.Items[i];

                    currentTarget.buildingDestroyedEvent += SetBuildingTarget;
                }
            }
        }
    }
}