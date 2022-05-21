using UnityEngine;

namespace Resources.Scripts
{
    public class PlayerStateHandler : MonoBehaviour
    {
        public PlayerState _playerState;

        public Player player;
        
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
        }

        public PlayerState GetPlayerState()
        {
            return _playerState;
        }
    }
}
