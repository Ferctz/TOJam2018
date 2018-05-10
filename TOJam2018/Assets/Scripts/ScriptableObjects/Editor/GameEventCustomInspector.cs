using UnityEngine;
using UnityEditor;

namespace ScriptableObjects
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventCustomInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GameEvent gameEvent = (GameEvent)target;
            if (GUILayout.Button("Raise Event"))
            {
                gameEvent.Raise();
            }
        }
    }
}