using UnityEngine;

namespace Ballmen.Player
{
    internal sealed class LocalPlayerMovement : IPlayerMovement
    {
        private Rigidbody _rigidbody;
        private LayerMask _layerMask;
        public LocalPlayerMovement(Rigidbody rigidbody, LayerMask layerMask) 
        {
            _rigidbody = rigidbody;
            _layerMask = layerMask;
        }

        void IPlayerMovement.Move(Vector3 direction)
        {
            var pos = _rigidbody.transform.position;

            if (Physics.Raycast(pos, direction, 0.6f, _layerMask) == false)
                _rigidbody.AddForce(Time.deltaTime * direction, ForceMode.Impulse);
        }

        void IPlayerMovement.AddVelocity(Vector3 direction)
        {
            _rigidbody.AddForce(direction, ForceMode.Impulse);
        }
    }
}
