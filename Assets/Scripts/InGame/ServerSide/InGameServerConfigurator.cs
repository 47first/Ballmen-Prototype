using Ballmen.Session;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Ballmen.InGame.Server
{
    public class InGameServerConfigurator : MonoBehaviour, IDisposable
    {
        [SerializeField] private ServerImpulseCreator _impulseCreatorPrefab;
        [SerializeField] private ServerGameFlow _serverGameFlowPrefab;
        [SerializeField] private PlayerSpawner _playerSpawnerPrefab;
        //In scene
        [SerializeField] private List<Basket> _sceneBaskets;
        [SerializeField] private PlayerTeleporter _teleporter;

        private ISessionInfo _sessionInfo;
        private IPlayerDecoratorsPull _playerDecoratorsPull;
        private PlayerConnectionController _playerConnectionController;

        public void Dispose()
        {
            if (_sessionInfo != null)
                _playerConnectionController?.Dispose();

            _playerDecoratorsPull?.Dispose();
        }

        internal void Configure()
        {
            if (NetworkManager.Singleton.IsServer == false)
                return;

            _sessionInfo = SessionInfo.Singleton;
            _playerDecoratorsPull = new PlayerDecoratorsPull();
            _playerConnectionController = new PlayerConnectionController(_sessionInfo, _playerDecoratorsPull);

            _teleporter.Initialize();

            var serverGameFlow = SpawnServerGameFlow();
            var playerSpawner = InitializePlayerSpawner();

            SpawnImpulseCreator(_playerDecoratorsPull);
            InitializeBaskets(_playerDecoratorsPull, serverGameFlow);
            InitialPlayersSpawn(playerSpawner);
        }

        private void InitialPlayersSpawn(PlayerSpawner playerSpawner) 
        {
            TeamDistributor.DistributePlayersTeams(_sessionInfo.PlayersStates);

            foreach (var playerInfo in _sessionInfo.ConnectedPlayers)
            {
                var playerTeam = _sessionInfo.PlayersStates.GetStateByGuid(playerInfo.GUID.ToString()).Team;
                playerSpawner.NetworkSpawnPlayer(playerInfo, playerTeam);
            }
        }

        private void InitializeBaskets(IPlayerDecoratorsPull decoratorsPull, ServerGameFlow serverGameFlow)
        {
            foreach (var basket in _sceneBaskets)
                basket.Initialize(decoratorsPull, serverGameFlow);
        }

        private PlayerSpawner InitializePlayerSpawner() 
        {
            var playerSpawner = Instantiate(_playerSpawnerPrefab);

            playerSpawner.Initialize(_playerDecoratorsPull, _teleporter);

            return playerSpawner;
        }

        private ServerGameFlow SpawnServerGameFlow()
        {
            var serverGameFlow = Instantiate(_serverGameFlowPrefab);

            serverGameFlow.Initialize(_playerDecoratorsPull, _teleporter);

            return serverGameFlow;
        }

        private void SpawnImpulseCreator(IPlayerDecoratorsPull playerDecoratorPull)
        {
            var impulseCreator = Instantiate(_impulseCreatorPrefab);

            impulseCreator.NetworkObject.SpawnWithOwnership(NetworkManager.Singleton.LocalClientId, true);
            impulseCreator.Initialize(playerDecoratorPull);
        }
    }
}
