using System.Collections;
using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class BuildingAI : MonoBehaviour
    {
        public PlayerRuntimeSet playerRuntimeSet;
        public ShatterableRuntimeSet shatterableRuntimeSet;

        private ShatterOnCollision currentTarget;

        private void Start()
        {
            //StartCoroutine()
        }

        IEnumerator WaitToSetFirstTarget()
        {
            yield return new WaitForSeconds(1f);
            SetBuildingTarget();
        }

        private void SetBuildingTarget()
        {

        }
    }
}