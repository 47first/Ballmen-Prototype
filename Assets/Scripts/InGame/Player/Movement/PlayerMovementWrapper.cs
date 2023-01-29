using UnityEngine;

namespace Ballmen.Player
{
    internal interface IPlayerMovement
    {
        internal void Move(Vector3 direction);
        internal void AddVelocity(Vector3 direction);
    }

    internal sealed class PlayerMovementWrapper : IPlayerWrapper
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
                _playerMovement.AddVelocity(Vector3.up * _settings.jumpForce);

            _playerMovement.Move(dir * _settings.moveSpeed);
        }

        internal void AddVelocity(Vector3 direction) 
        {
            _playerMovement.AddVelocity(direction);
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

        /*
        private bool IsOnGround()
        {
            var startPoint = _rigidbody.transform.position;
            var endPoint = _rigidbody.transform.position - Vector3.up * 1.1f;

            Debug.DrawLine(startPoint, endPoint, Color.red);

            return Physics.Raycast(_rigidbody.transform.position,
                Vector3.down, 1.1f, _layerMask);
        }
        */
    }
}
