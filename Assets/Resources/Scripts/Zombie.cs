using System;
using UnityEngine;
using static AnimatorUtil;

namespace Resources.Scripts
{
    public class Zombie : Monster
    {
        private Transform _playerTransform;
        private const float Speed = 2F;
        private ZombieState _zombieState;
        
        public Animator animator;
        public LayerMask ignoredLayer;

        // Start is called before the first frame update
        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _zombieState = ZombieState.Walking;
        }
        
        // Update is called once per frame
        private void Update()
        {
            HandleState();
        }

        private void FixedUpdate()
        {
            if (_zombieState == ZombieState.Dying) return;
            
            var hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 2F, ~ignoredLayer);
            if (hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case "Player":
                        _zombieState = ZombieState.AttackingPlayer;
                        MoveToPosition(_playerTransform.position);
                        break;
                    case "Door":
                    case "Wall": 
                        _zombieState = ZombieState.DestroyingObstacle;
                        MoveToPosition(hit.collider.transform.position);
                        break;
                }
            }
            else
            {
                MoveToPosition(_playerTransform.position);
            }
        }

        private void MoveToPosition(Vector3 targetPosition)
        {
            var ownPosition = transform.position;

            // Get the vector difference between the two positions
            var offset = new Vector2(targetPosition.x - ownPosition.x, targetPosition.y - ownPosition.y);

            // Determine the angle
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            // Apply the rotation
            transform.rotation = Quaternion.Euler(0F, 0F, angle);

            transform.position =
                Vector2.MoveTowards(ownPosition, targetPosition, Speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag.Equals("Bullet"))
            {
                _zombieState = ZombieState.Dying;
            }
        }

        private void HandleState()
        {
            switch (_zombieState)
            {
                case ZombieState.Walking:
                    ChangeAnimationTo(animator, "isRunning");
                    break;
                case ZombieState.AttackingPlayer:
                case ZombieState.DestroyingObstacle:
                    ChangeAnimationTo(animator, "isAttacking");
                    break;
                case ZombieState.Dying:
                    ChangeAnimationTo(animator,"isDying");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum ZombieState
        {
            Walking,
            AttackingPlayer,
            DestroyingObstacle,
            Dying
        }
    }
}
