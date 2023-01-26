using UnityEngine;

namespace Ballmen.Player
{
    internal interface IPlayerMovement
    {
        internal void Move(Vector3 direction, float force);
        internal void Jump(float power);
    }

    internal sealed class PlayerMovementWrapper
    {
        private IPlayerMovement _playerMovement;
        private MovementSettings _settings;
        internal PlayerMovementWrapper(IPlayerMovement playerMovement, MovementSettings settings) 
        {
            _playerMovement = playerMovement;
            _settings = settings;
        }

        internal void HandleMovementCommand() 
        {
            var dir = GetMoveDirection();

            if (Input.GetKeyDown(KeyCode.Space))
                _playerMovement.Jump(_settings.jumpForce);

            _playerMovement.Move(dir, _settings.moveSpeed);
        }

        private Vector3 GetMoveDirection()
        {
            float xDir = 0;

            if (Input.GetKey(KeyCode.D))
                xDir += 1;

            if (Input.GetKey(KeyCode.A))
                xDir -= 1;

            return Vector3.right * xDir;
        }
    }
}
