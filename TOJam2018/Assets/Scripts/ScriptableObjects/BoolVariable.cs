using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/BoolVariable")]
    public class BoolVariable : ScriptableObject
    {
        public bool Value;
    }
}