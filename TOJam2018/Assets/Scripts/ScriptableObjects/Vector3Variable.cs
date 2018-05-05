using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/Vector3Variable")]
    public class Vector3Variable : ScriptableObject
    {
        public Vector3 Value;
    }
}