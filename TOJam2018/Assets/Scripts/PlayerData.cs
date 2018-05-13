using UnityEngine;

namespace TOJAM2018.Gameplay
{
    /// <summary>
    /// This class should be attached to player instances. Adds this instance to player runtime set.
    /// </summary>
    public class PlayerData : MonoBehaviour
    {
        public PlayerRuntimeSet playerRuntimeSet;

        private Transform playerTransform;
        public Transform PlayerTransform { get { return playerTransform ?? (playerTransform = transform); } }

        private void OnEnable()
        {
            if (playerRuntimeSet != null)
            {
                playerRuntimeSet.Items.Add(this);
            }
        }

        private void OnDisable()
        {
            if (playerRuntimeSet != null)
            {
                playerRuntimeSet.Items.Remove(this);
            }
        }
    }
}