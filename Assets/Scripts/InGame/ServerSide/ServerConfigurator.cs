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

        private ISessionInfo _sessionInfo;
        private IPlayerDecoratorPull _playerDecoratorsPull;
        private IPlayerConnectionController _playerConnectionController;

        public void Dispose()
        {
            if (_sessionInfo != null)
                _sessionInfo.Players.OnListChanged -= _playerConnectionController.OnPlayerListChange;

            _playerConnectionController?.Dispose();
            _playerDecoratorsPull?.Dispose();
        }

        internal void Configure()
        {
            if (NetworkManager.Singleton.IsServer == false)
                return;

            _playerConnectionController = new PlayerConnectionController();
            _playerDecoratorsPull = new PlayerDeconratorsPull();
            _sessionInfo = SessionInfo.Singleton;

            SpawnServerFacadeObject(_playerDecoratorsPull);

            foreach (var playerInfo in _sessionInfo.Players)
                SpawnPlayer(playerInfo);

            _sessionInfo.Players.OnListChanged += _playerConnectionController.OnPlayerListChange;
        }

        private void SpawnPlayer(PlayerInfo playerInfo)
        {
            var playerDecorator = Instantiate(_playerDecoratorPrefab);

            playerDecorator.NetworkObject.SpawnWithOwnership(playerInfo.Id, true);
            playerDecorator.NetworkObject.DontDestroyWithOwner = true;

            _playerDecoratorsPull.AddDecorator(playerInfo.GUID.ToString(), playerDecorator);
        }

        private void SpawnServerFacadeObject(IPlayerDecoratorPull playerDecoratorPull)
        {
            var serverFacade = Instantiate(_impulseCreatorPrefab);

            serverFacade.NetworkObject.SpawnWithOwnership(NetworkManager.Singleton.LocalClientId, true);
            serverFacade.Initialize(playerDecoratorPull);
        }
    }
}