using UnityEngine;
using static UnityEditor.PlayerSettings;

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

        private bool IsOnGround() 
        {
            var startPoint = _rigidbody.transform.position;
            var endPoint = _rigidbody.transform.position - Vector3.up * 1.1f;

            Debug.DrawLine(startPoint, endPoint, Color.red);

            return Physics.Raycast(_rigidbody.transform.position,
                Vector3.down, 1.1f, _layerMask);
        }

        void IPlayerMovement.Move(Vector3 direction, float force)
        {
            var pos = _rigidbody.transform.position;

            if (Physics.Raycast(pos, direction, 0.6f, _layerMask) == false)
                _rigidbody.AddForce(force * Time.deltaTime * direction, ForceMode.Impulse);
        }

        void IPlayerMovement.Jump(float power)
        {
            if(IsOnGround())
                _rigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);
        }
    }
}
