using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AnimatorUtil;

namespace Resources.Scripts
{
    public class Zombie : Monster
    {
        private GameObject _player;
        private Transform _target;
        private const float Speed = 2F;
        private ZombieState _zombieState;
        public List<Collider2D> _colliders = new();
        
        public Animator animator;

        // Start is called before the first frame update
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            
            ChasePlayer();
        }
        
        // Update is called once per frame
        private void Update()
        {
            HandleState();
        }

        private void MoveToTarget()
        {
            var ownPosition = transform.position;
            var targetPosition = _target.transform.position;

            // Get the vector difference between the two positions
            var offset = new Vector2(targetPosition.x - ownPosition.x, targetPosition.y - ownPosition.y);

            // Determine the angle
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            // Apply the rotation
            transform.rotation = Quaternion.Euler(0F, 0F, angle);

            transform.position =
                Vector2.MoveTowards(ownPosition, targetPosition, Speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            switch (col.tag)
            {
                case "Player":
                case "Door":
                case "Wall":
                    if (!_colliders.Contains(col))
                    {
                        _colliders.Add(col);
                    }
                    break;
            }
            _zombieState = ZombieState.Attacking;
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (_colliders.Contains(col))
            {
                _colliders.Remove(col);
            }
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
                case ZombieState.Running:
                    ChangeAnimationTo(animator, "isRunning");
                    MoveToTarget();
                    break;
                case ZombieState.Attacking:
                    var targetGameObject = _colliders.Count == 0
                        ? _player
                        : _colliders.Aggregate((i, j) =>
                            i.GetComponent<StrategicValue>().GetValue() >= j.GetComponent<StrategicValue>().GetValue()
                                ? i
                                : j).gameObject;
                    
                    Debug.Log($"{gameObject.name} is targeting highest value object: {targetGameObject}");
                    _target = targetGameObject.transform;
                    
                    ChangeAnimationTo(animator, "isAttacking");
                    MoveToTarget();   
                    break;
                case ZombieState.Dying:
                    ChangeAnimationTo(animator,"isDying");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChasePlayer()
        {
            Debug.Log($"{gameObject.name} is Chasing player");
            _target = _player.transform;
            _zombieState = ZombieState.Running;
        }

        private enum ZombieState
        {
            Running,
            Attacking,
            Dying
        }
    }
}
