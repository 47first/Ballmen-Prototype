using Unity.Netcode;
using UnityEngine;

namespace Ballmen.Player
{
    internal enum PlayerType 
    { 
        Client = 1,
        Owner = 2,
        Server = 4
    }

    internal sealed class PlayerDecorator : NetworkBehaviour
    {
        [SerializeField] private PlayerType _type;
        [SerializeField] private MovementSettings _movementSettings;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _obstaclesLayerMask;

        //Wrappers
        private PlayerMovementWrapper _movementWrapper;
        private PlayerAttackWrapper _attackWrapper;
        private KickHandlerWrapper _kickHandlerWrapper;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            Initialize();
        }

        //Testing only
        private void Start()
        {
            Initialize();
        }

        [ClientRpc]
        internal void GetKickedClientRpc(Vector3 direction, float force)
        {
            _kickHandlerWrapper.HandleKick(direction, force);
        }

        private void Initialize()
        {
            _movementWrapper = new(new LocalPlayerMovement(_rigidbody, _obstaclesLayerMask), _movementSettings);
            _attackWrapper = new(new LocalPlayerAttack());
            _kickHandlerWrapper = new(new RigidbodyKickHandler(_rigidbody));
        }

        private void Update()
        {
            _movementWrapper?.HandleMovementCommand();
            _attackWrapper?.HandleAttackCommand();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(_rigidbody.transform.position, 0.6f);
        }
    }
}
