using System.Collections;
using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class BuildingSpawner : MonoBehaviour
    {
        public GameObject buildingPrefab;

        public TerrainData gameTerrainData;

        private void Start()
        {
            StartCoroutine(WaitForInitialSpawn());
        }

        IEnumerator WaitForInitialSpawn()
        {
            yield return new WaitForSeconds(3f);
            SpawnBuilding();
        }

        private void SpawnBuilding()
        {
            GameObject buildingClone = GameObject.Instantiate(buildingPrefab);
        }
    }
}