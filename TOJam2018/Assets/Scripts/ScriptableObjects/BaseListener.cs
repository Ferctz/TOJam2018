using UnityEngine;

namespace ScriptableObjects
{
    public abstract class BaseListener<T> : MonoBehaviour
    {
        protected abstract void OnEnable();

        protected abstract void OnDisable();

        public abstract void OnEventRaised(T typeA, T typeB);
    }
}