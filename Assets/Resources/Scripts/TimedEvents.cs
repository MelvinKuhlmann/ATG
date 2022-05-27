using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts
{
    public class TimedEvents : MonoBehaviour
    {
        private int _timer;

        // The first item in the list, on index 0, wont go off on purpose. Only events starting from the 1st 
        // second will go off. Else we will have timing issues.
        public List<UnityEvent> eventsPerSecond = new();

        private void Start()
        {
            InvokeRepeating("TriggerEvent", 1F, 1F);
        }

        private void TriggerEvent()
        {
            _timer = Mathf.RoundToInt(Time.fixedTime);

            if (_timer > eventsPerSecond.Count - 1)
            {
                return;
            }

            eventsPerSecond[_timer].Invoke();
        }
    }
}