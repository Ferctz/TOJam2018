using Random = System.Random;
using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class AddForceToFractures : MonoBehaviour
    {
        public Rigidbody[] fractureRigidbodies;
        private Random rand;
        private const float EXPLOSIVE_FORCE = 100f;

        private void Awake()
        {
            rand = new Random((int)System.DateTime.Now.Ticks);
            for (int i = 0; i < fractureRigidbodies.Length; i++)
            {
                fractureRigidbodies[i].AddForce((fractureRigidbodies[i].position - transform.position) * (float)rand.NextDouble() * EXPLOSIVE_FORCE, ForceMode.Impulse);
            }
        }
    }
}