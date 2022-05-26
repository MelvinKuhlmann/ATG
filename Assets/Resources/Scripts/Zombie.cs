using System.Collections;
using UnityEngine;

namespace Resources.Scripts
{
    public class Zombie : MonoBehaviour
    {
        private Transform _playerTransform;
        private const float Speed = 2F;

        // Start is called before the first frame update
        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            var ownPosition = transform.position;
            var playerPosition = _playerTransform.position;

            // Get the vector difference between the two positions
            var offset = new Vector2(playerPosition.x - ownPosition.x, playerPosition.y - ownPosition.y);
        
            // Determine the angle
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        
            // Apply the rotation -90F because the sprite is not facing right by default
            transform.rotation = Quaternion.Euler(0F, 0F, angle - 90F);

            // This causes lag/crashes of the game, not sure why yet.
            transform.position =
                Vector2.MoveTowards(ownPosition, playerPosition, Speed * Time.deltaTime);
        }
    }
}
