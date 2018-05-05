using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/StringVariable")]
    public class StringVariable : ScriptableObject
    {
        public string Value;
    }
}