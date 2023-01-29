using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Ballmen.Server
{
    internal sealed class ServerImpulseCreator : NetworkBehaviour
    {
        private static ServerImpulseCreator _instance;

        [SerializeField] private LayerMask _impulseLayerMask;
        private IPlayerDecoratorPull _playerDecoratorPull;
        private Impulse _impulse;

        internal static ServerImpulseCreator Singleton => _instance;

        public void Initialize(IPlayerDecoratorPull playerDecoratorPull) 
        {
            _impulse = new ServerImpulse(_impulseLayerMask);
            _playerDecoratorPull = playerDecoratorPull;
        }

        [ServerRpc(RequireOwnership = false)]
        internal void CreateImpulseInRadiusServerRpc(Vector3 pos, float radius, float force, ServerRpcParams serverParams = default) 
        {
            if (IsServer == false)
                return;

            var punchList = _impulse.GetPunchListByRadius(pos, radius, force);

            PunchPlayers(punchList, serverParams.Receive.SenderClientId);
        }

        [ServerRpc(RequireOwnership = false)]
        internal void CreateImpulseInBoxServerRpc(Vector3 pos, Vector3 size, float force, ServerRpcParams serverParams = default)
        {
            if (IsServer == false)
                return;

            var punchList = _impulse.GetPunchListByBox(pos, size, force);

            PunchPlayers(punchList, serverParams.Receive.SenderClientId);
        }

        private void PunchPlayers(Dictionary<IPunchable, Vector3> punchList, ulong excludeId) 
        {
            punchList.Remove(_playerDecoratorPull.GetDecorator(excludeId));

            foreach (var punchArgs in punchList)
                punchArgs.Key.GetPunched(punchArgs.Value);
        }

        private void Start()
        {
            _instance = this;
        }
    }
}
