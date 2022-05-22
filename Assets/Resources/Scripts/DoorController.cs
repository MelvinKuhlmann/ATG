using System;
using UnityEngine;

namespace Resources.Scripts
{
    public class DoorController : MonoBehaviour
    {
        public GameObject interactKey;

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
                Debug.Log("Interacted with door!");
            }
        }
    }
}
