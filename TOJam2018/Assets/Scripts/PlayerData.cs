using System.Collections;
using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class PlayerData : MonoBehaviour
    {
        public int playerNumber;

        private Transform playerTransform;
        public Transform PlayerTransform { get { return playerTransform ?? (playerTransform = transform); } }
    }
}