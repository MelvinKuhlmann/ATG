using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts
{
    public class Player : MonoBehaviour
    {
        private Camera _camera;
        private Vector2 _movement;

        public float movementSpeed = 5F;
        public Rigidbody2D rb;

        // Start is called before the first frame update
        private void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            HandleMovementInput();
            HandleAim();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            rb.MovePosition(rb.position + _movement * (movementSpeed * Time.fixedDeltaTime));
        }
        
        private void HandleAim()
        {
            // Get the position of mouse and the player on the screen
            var mouse = Input.mousePosition;
            var playerOnScreenPoint = _camera.WorldToScreenPoint(transform.localPosition);

            // Get the vector difference between the two positions
            var offset = new Vector2(mouse.x - playerOnScreenPoint.x, mouse.y - playerOnScreenPoint.y);

            // Determine the angle
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            
            // Apply the rotation
            transform.rotation = Quaternion.Euler(0F, 0F, angle);
        }

        private void HandleMovementInput()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
        }

        public Vector2 GetMovement()
        {
            return _movement;
        }
    }
}
