using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/IntVariable")]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}