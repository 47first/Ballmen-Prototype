using UnityEngine;

namespace Ballmen.Player
{
    internal class RigidbodyKickHandler : IKickHandler
    {
        private Rigidbody _rigidbody;
        internal RigidbodyKickHandler(Rigidbody rigidbody) 
        {
            _rigidbody = rigidbody;
        }

        void IKickHandler.HandleKick(Vector3 direction)
        {
            _rigidbody.velocity = direction;
        }
    }
}
