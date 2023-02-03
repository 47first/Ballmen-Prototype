using Ballmen.InGame;
using Ballmen.Player;
using Ballmen.Session;
using System;
using Unity.Netcode;
using UnityEngine;

namespace Ballmen.Server
{
    public class ServerConfigurator : MonoBehaviour, IDisposable
    {
        [SerializeField] private PlayerDecorator _playerDecoratorPrefab;
        [SerializeField] private ServerImpulseCreator _impulseCreatorPrefab;
        [SerializeField] private ServerGameFlow _serverGameFlowPrefab;

        private ISessionInfo _sessionInfo;
        private IPlayerDecoratorsPull _playerDecoratorsPull;
        private IPlayerConnectionController _playerConnectionController;

        public void Dispose()
        {
            if (_sessionInfo != null)
            {
                _sessionInfo.OnPlayerConnected.RemoveListener(_playerConnectionController.OnPlayerReconnected);
                _sessionInfo.OnPlayerDisconnected.RemoveListener(_playerConnectionController.OnPlayerDisconnected);
            }

            _playerDecoratorsPull?.Dispose();
        }

        internal void Configure()
        {
            if (NetworkManager.Singleton.IsServer == false)
                return;

            _sessionInfo = SessionInfo.Singleton;
            _playerDecoratorsPull = new PlayerDecoratorsPull();
            _playerConnectionController = new PlayerConnectionController(_playerDecoratorsPull);

            SpawnServerGameFlow();
            SpawnImpulseCreator(_playerDecoratorsPull);

            TeamDistributor.DistributePlayersTeams(_sessionInfo.Players);

            foreach (var playerInfo in _sessionInfo.Players)
                SpawnPlayer(playerInfo);

            _sessionInfo.OnPlayerConnected.AddListener(_playerConnectionController.OnPlayerReconnected);
            _sessionInfo.OnPlayerDisconnected.AddListener(_playerConnectionController.OnPlayerDisconnected);
        }

        private void SpawnServerGameFlow()
        {
            Instantiate(_serverGameFlowPrefab);
        }

        private void SpawnPlayer(PlayerInfo playerInfo)
        {
            var playerDecorator = Instantiate(_playerDecoratorPrefab);

            playerDecorator.BindPlayerInfo(playerInfo);
            playerDecorator.NetworkObject.SpawnWithOwnership(playerInfo.Id, true);
            playerDecorator.NetworkObject.DontDestroyWithOwner = true;

            _playerDecoratorsPull.AddDecorator(playerInfo.GUID.ToString(), playerDecorator);
        }

        private void SpawnImpulseCreator(IPlayerDecoratorsPull playerDecoratorPull)
        {
            var impulseCreator = Instantiate(_impulseCreatorPrefab);

            impulseCreator.NetworkObject.SpawnWithOwnership(NetworkManager.Singleton.LocalClientId, true);
            impulseCreator.Initialize(playerDecoratorPull);
        }
    }
}
