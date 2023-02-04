using Ballmen.InGame.Player;
using Ballmen.Session;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Ballmen.InGame.Server
{
    public class InGameServerConfigurator : MonoBehaviour, IDisposable
    {
        [SerializeField] private PlayerDecorator _playerDecoratorPrefab;
        [SerializeField] private ServerImpulseCreator _impulseCreatorPrefab;
        [SerializeField] private ServerGameFlow _serverGameFlowPrefab;
        //In scene
        [SerializeField] private List<Basket> _sceneBaskets;

        private ISessionInfo _sessionInfo;
        private IPlayerDecoratorsPull _playerDecoratorsPull;
        private IPlayerConnectionController _playerConnectionController;

        public void Dispose()
        {
            if (_sessionInfo != null)
            {
                _sessionInfo.OnPlayerApproved.RemoveListener(_playerConnectionController.OnPlayerReconnected);
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
            var serverGameFlow = GetAndSpawnServerGameFlow();

            SpawnImpulseCreator(_playerDecoratorsPull);
            InitializeBaskets(_playerDecoratorsPull, serverGameFlow);
            TeamDistributor.DistributePlayersTeams(_sessionInfo.PlayersStates);

            foreach (var playerInfo in _sessionInfo.ConnectedPlayers)
                SpawnPlayerDecorator(playerInfo);

            _sessionInfo.OnPlayerApproved.AddListener(_playerConnectionController.OnPlayerReconnected);
            _sessionInfo.OnPlayerDisconnected.AddListener(_playerConnectionController.OnPlayerDisconnected);
        }

        private void InitializeBaskets(IPlayerDecoratorsPull decoratorsPull, ServerGameFlow serverGameFlow)
        {
            foreach (var basket in _sceneBaskets)
                basket.Initialize(decoratorsPull, serverGameFlow);
        }

        private ServerGameFlow GetAndSpawnServerGameFlow()
        {
            var serverGameFlow = Instantiate(_serverGameFlowPrefab);

            serverGameFlow.Initialize();

            return serverGameFlow;
        }

        private void SpawnPlayerDecorator(PlayerInfo playerInfo)
        {
            var playerDecorator = Instantiate(_playerDecoratorPrefab);
            var playerTeam = _sessionInfo.PlayersStates.GetStateByGuid(playerInfo.GUID.ToString()).Team;

            playerDecorator.BindPlayerInfo(playerInfo);
            playerDecorator.SetTeam(playerTeam);
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
