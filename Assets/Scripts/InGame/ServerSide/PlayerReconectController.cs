using Ballmen.Session;
using System;
using UnityEngine;

namespace Ballmen.InGame.Server
{
    internal sealed class PlayerConnectionController : IDisposable
    {
        private ISessionInfo _sessionInfo;
        private IPlayerDecoratorsPull _decoratorsPull;
        internal PlayerConnectionController(ISessionInfo sessionInfo, IPlayerDecoratorsPull decoratorsPull) 
        {
            _sessionInfo = sessionInfo;
            _decoratorsPull = decoratorsPull;

            _sessionInfo.OnPlayerApproved.AddListener(OnPlayerReconnected);
            _sessionInfo.OnPlayerDisconnected.AddListener(OnPlayerDisconnected);
        }

        public void Dispose()
        {
            _sessionInfo?.OnPlayerApproved.RemoveListener(OnPlayerReconnected);
            _sessionInfo?.OnPlayerDisconnected.RemoveListener(OnPlayerDisconnected);
        }

        private void OnPlayerReconnected(PlayerInfo reconnectedPlayerInfo)
        {
            Debug.Log($"Player approved!");

            var connectedPlayerDecorator = _decoratorsPull.GetDecorator(reconnectedPlayerInfo);

            connectedPlayerDecorator.gameObject.SetActive(true);
            connectedPlayerDecorator.BindPlayerInfo(reconnectedPlayerInfo);
            connectedPlayerDecorator.NetworkObject.SpawnWithOwnership(reconnectedPlayerInfo.Id);

            Debug.Log($"{connectedPlayerDecorator.name} were appear");
        }

        private void OnPlayerDisconnected(PlayerInfo disconnectedPlayer)
        {
            var disconnectedPlayerDecorator = _decoratorsPull.GetDecorator(disconnectedPlayer);

            disconnectedPlayerDecorator.NetworkObject.Despawn(false);
            disconnectedPlayerDecorator.gameObject.SetActive(false);

            Debug.Log($"{disconnectedPlayerDecorator.name} were hiden");
        }
    }
}
