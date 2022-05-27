using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

namespace Resources.Scripts
{
    public class Zombie : Monster
    {
        private Transform _playerTransform;
        private const float Speed = 2F;
        private ZombieState _zombieState;
        public Animator animator;

        // Start is called before the first frame update
        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _zombieState = ZombieState.Walking;
        }

        private void FixedUpdate()
        {
            var ownPosition = transform.position;
            var playerPosition = _playerTransform.position;

            // Get the vector difference between the two positions
            var offset = new Vector2(playerPosition.x - ownPosition.x, playerPosition.y - ownPosition.y);
        
            // Determine the angle
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        
            // Apply the rotation -90F because the sprite is not facing right by default
            transform.rotation = Quaternion.Euler(0F, 0F, angle - 90F);

            transform.position =
                Vector2.MoveTowards(ownPosition, playerPosition, Speed * Time.deltaTime);

            if (Vector2.Distance(ownPosition, playerPosition) <= 2F)
            {
                _zombieState = ZombieState.Attacking;
            }
            else
            {
                _zombieState = ZombieState.Walking;
            }
        }

        private void Update()
        {
            HandleState();
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
                    ChangeAnimationTo("isWalking");
                    break;
                case ZombieState.Attacking:
                    ChangeAnimationTo("isAttacking");
                    break;
                case ZombieState.Dying:
                    ChangeAnimationTo("isDying");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum ZombieState
        {
            Walking,
            Attacking,
            Dying
        }

        protected override Animator getAnimator()
        {
            return animator;
        }
    }
}
