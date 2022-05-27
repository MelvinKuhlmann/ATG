using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Resources.Scripts
{
    public class DoorController : Obstacle
    {
        private DoorState _doorState;
        private DoorPowerLevel _doorPowerLevel;
        private float _power;

        public GameObject interactKey;
        public Animator animator;
        public Light2D indicatorLight;
        public bool permanentPower;
        public Transform healthbar;


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

        protected override void ChildStart()
        {
            _doorState = DoorState.Closed;
            _doorPowerLevel = DoorPowerLevel.Full;
            _power = 100F;
        }

        protected override void ChildUpdate()
        {
            if (!permanentPower)
            {
                UpdatePower();
                CheckPowerLevel();
            }

            if (Input.GetKeyDown(KeyCode.E) && interactKey.activeInHierarchy &&
                !DoorPowerLevel.Depleted.Equals(_doorPowerLevel))
            {
                ToggleDoor();
            }
        }

        protected override int InitializeDurability()
        {
            return 100;
        }

        protected override Transform GetHealthBar()
        {
            return healthbar;
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