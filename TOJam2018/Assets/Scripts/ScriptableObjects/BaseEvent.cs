using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class BaseEvent<T> : ScriptableObject
    {
        private List<BaseListener<T>> listeners = new List<BaseListener<T>>();

        public void Raise(T a, T b)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(a, b);
            }
        }

        public void RegisterListener(BaseListener<T> listener)
        {
            listeners.Add(listener);
        }

        public void UnRegisterListener(BaseListener<T> listener)
        {
            listeners.Remove(listener);
        }
    }
}