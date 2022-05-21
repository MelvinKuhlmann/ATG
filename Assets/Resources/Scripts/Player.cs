using UnityEngine;

namespace Resources.Scripts
{
    public class Player : MonoBehaviour
    {
        private Vector2 _movement;
    
        public float movementSpeed = 5F;

        public Rigidbody2D rb;

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            HandleMovementInput();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            rb.MovePosition(rb.position + _movement * (movementSpeed * Time.fixedDeltaTime));
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
