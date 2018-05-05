using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/FloatVariable")]
    public class FloatVariable : ScriptableObject
    {
        public float Value;
    }
}