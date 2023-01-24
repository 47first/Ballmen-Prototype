using Unity.Netcode;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Ballmen.Player
{
    internal enum PlayerType 
    { 
        Client,
        Owner
    }

    internal sealed class PlayerDecorator : NetworkBehaviour
    {
        [SerializeField] private PlayerType _type;
        [SerializeField] private MovementSettings _movementSettings;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _obstaclesLayerMask;
        private PlayerMovementWrapper _movementWrapper;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        }

        private void Start()
        {
            Initialize(_type);
        }

        private void Initialize(PlayerType type) 
        {
            if (type == PlayerType.Owner)
            {
                _movementWrapper = new(new LocalPlayerMovement(_rigidbody, _obstaclesLayerMask), _movementSettings);
            }
        }

        private void Update()
        {
            _movementWrapper?.HandleMovement();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(_rigidbody.transform.position, 0.6f);
        }
    }
}
