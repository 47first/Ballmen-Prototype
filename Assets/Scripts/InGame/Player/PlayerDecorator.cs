using Ballmen.Session;
using Unity.Netcode;
using UnityEngine;

namespace Ballmen.InGame.Player
{
    internal interface IPlayerWrapper { }

    internal sealed class PlayerDecorator : NetworkBehaviour, IPunchable
    {
        private static PlayerDecorator _local;
        [SerializeField] private MovementSettings _movementSettings;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _obstaclesLayerMask;

        //Wrappers
        private PlayerMovementWrapper _movementWrapper;
        private PlayerAttackWrapper _attackWrapper;
        private KickHandlerWrapper _kickHandlerWrapper;

        //Info
        private PlayerInfo _playerInfo;
        private GameTeam _team;

        internal static PlayerDecorator Local => _local;

        internal PlayerInfo PlayerInfo => _playerInfo;

        internal GameTeam Team => _team;

        void IPunchable.GetPunched(Vector3 direction) => ReceivePunchClientRpc(direction);

        public override void OnGainedOwnership()
        {
            base.OnGainedOwnership();

            Debug.Log("On Gained Ownership");

            if (IsOwner)
                Initialize();

            Debug.Log($"_local == null = {_local == null}");
        }

        public override void OnLostOwnership()
        {
            Debug.Log("On Lost Ownership");

            if (IsOwner)
                _local = null;

            Debug.Log($"_local == null = {_local == null}");

            base.OnLostOwnership();
        }

        internal void Initialize()
        {
            if (IsOwner)
            {
                _movementWrapper = new(new LocalPlayerMovement(_rigidbody, _obstaclesLayerMask), _movementSettings);
                _attackWrapper = new(new LocalPlayerAttack(this));
                _kickHandlerWrapper = new(new RigidbodyKickHandler(_rigidbody));

                _local = this;
            }
        }

        [ClientRpc]
        internal void ReceivePunchClientRpc(Vector3 direction)
        {
            if (IsOwner == false)
                return;

            _kickHandlerWrapper.HandleKick(direction);
        }

        internal void BindPlayerInfo(PlayerInfo playerInfo)
        {
            _playerInfo = playerInfo;
        }

        internal void SetTeam(GameTeam team) 
        {
            _team = team;
        }

        private void Update()
        {
            _movementWrapper?.HandleMovementCommand();
            _attackWrapper?.HandleAttackCommand();
        }
    }
}
