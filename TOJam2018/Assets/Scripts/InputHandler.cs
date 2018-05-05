using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.InputHandling
{
    public class InputHandler : MonoBehaviour
    {
        public FloatVariable xAxis;
        public FloatVariable yAxis;
        public FloatVariable xMouse;
        public FloatVariable yMouse;

        public GameEvent fire1Event;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            xAxis.Value = Input.GetAxis("Horizontal");
            yAxis.Value = Input.GetAxis("Vertical");
            xMouse.Value = Input.GetAxis("Mouse X");
            yMouse.Value = Input.GetAxis("Mouse Y");

            if (Input.GetButtonDown("Fire1"))
            {
                fire1Event.Raise();
            }
        }
    }
}