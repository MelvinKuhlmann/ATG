using UnityEngine;

namespace Resources.Scripts
{
    public class AlarmController : MonoBehaviour
    {
        private AlarmState _alarmState;
        private float _timer;
    
        public Animator animator;
    
        // Start is called before the first frame update
        private void Start()
        {
            _alarmState = AlarmState.Off;
            _timer = 30F;
        }

        // Update is called once per frame
        private void Update()
        {
            if (AlarmState.On.Equals(_alarmState))
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _timer = 30F;
                    _alarmState = AlarmState.Off;
                };
            }
            
            animator.SetBool("isOn", AlarmState.On.Equals(_alarmState));

            // For debug purposes
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TriggerAlarm();
            }
        }

        public void TriggerAlarm()
        {
            _alarmState = AlarmState.On;
        }

        private enum AlarmState
        {
            On,
            Off
        }
    }
}
