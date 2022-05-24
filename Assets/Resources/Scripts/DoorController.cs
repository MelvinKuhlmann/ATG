using System;
using UnityEngine;

namespace Resources.Scripts
{
    public class DoorController : MonoBehaviour
    {
        private DoorState _doorState;
        public GameObject interactKey;
        public Animator animator;

        private void Start()
        {
            _doorState = DoorState.Closed;
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
            if (Input.GetKeyDown(KeyCode.E) && interactKey.activeInHierarchy)
            {
                ToggleDoor();
            }
        }

        private void ToggleDoor()
        {
            Debug.Log(_doorState);
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
}
