using System;
using UnityEngine;

namespace Resources.Scripts
{
    public class PlayerStateHandler : MonoBehaviour
    {
        private PlayerState _playerState;
        public Animator animator;
        public Player player;
        
        
        // Start is called before the first frame update
        private void Start()
        {
            _playerState = PlayerState.Idle;
        }
        
        private void Update()
        {
            var playerMovement = player.GetMovement();
        
            if (playerMovement.x != 0 || playerMovement.y != 0)
            {
                _playerState = PlayerState.Moving;
            }
            else
            {
                _playerState = PlayerState.Idle;
            }

            HandleAnimation();
        }
        
        private enum PlayerState
        {
            Idle,
            Moving,
            Attacking,
            Dead
        }
        
        private void HandleAnimation()
        {
            switch (_playerState)
            {
                case PlayerState.Idle:
                    AnimatorUtil.ChangeAnimationTo(animator, "isIdle");
                    break;
                case PlayerState.Moving:
                    AnimatorUtil.ChangeAnimationTo(animator, "isRunning");
                    break;
                case PlayerState.Attacking:
                case PlayerState.Dead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
