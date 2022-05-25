using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Resources.Scripts
{
    public class DoorController : MonoBehaviour
    {
        private DoorState _doorState;
        private DoorPowerLevel _doorPowerLevel;
        private float _power;
        public GameObject interactKey;
        public Animator animator;
        public Light2D indicatorLight;

        private void Start()
        {
            _doorState = DoorState.Closed;
            _power = 100F;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && interactKey)
            {
                interactKey.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && interactKey)
            {
                interactKey.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && interactKey.activeInHierarchy && !DoorPowerLevel.Depleted.Equals(_doorPowerLevel))
            {
                ToggleDoor();
            }

            UpdatePower();
            CheckPowerLevel();
        }

        private void UpdatePower()
        {
            _power -= Time.deltaTime;
            if (_power < 0) _power = 0;
            
            _doorPowerLevel = _power switch
            {
                <= 60 and > 30 => DoorPowerLevel.Medium,
                <= 30 => DoorPowerLevel.Depleted,
                _ => DoorPowerLevel.Full
            };
        }
        
        private void CheckPowerLevel()
        {
            indicatorLight.color = _doorPowerLevel switch
            {
                DoorPowerLevel.Medium => Color.yellow,
                DoorPowerLevel.Depleted => Color.red,
                _ => Color.green
            };
        }

        private void ToggleDoor()
        {
            animator.speed = DoorPowerLevel.Medium.Equals(_doorPowerLevel) ? 0.06F : 1F;

            switch (_doorState)
            {
                case DoorState.Closed:
                    animator.SetBool("open", true);
                    animator.SetBool("close", false);
                    _doorState = DoorState.Open;
                    break;
                case DoorState.Open:
                    animator.SetBool("open", false);
                    animator.SetBool("close", true);
                    _doorState = DoorState.Closed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum DoorState
    {
        Open,
        Closed
    }
    
    public enum DoorPowerLevel
    {
        Depleted,
        Medium,
        Full
    }
}
