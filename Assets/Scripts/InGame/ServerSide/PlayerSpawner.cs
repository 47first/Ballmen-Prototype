using Ballmen.InGame.Player;
using Ballmen.Session;
using System.Collections.Generic;
using UnityEngine;

namespace Ballmen.InGame.Server
{
    internal interface IPlayerSpawner 
    {
        internal void NetworkSpawnPlayer(PlayerInfo playerInfo, GameTeam team);
        internal void TeleportToAnySpawnPoint(PlayerDecorator playerDecorator);
    }

    internal sealed class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerDecorator _playerDecoratorPrefab;
        [SerializeField] private List<PlayerSpawnPoint> _spawnPoints;
        private Dictionary<GameTeam, List<PlayerSpawnPoint>> _teamSpawnPoints;
        private IPlayerDecoratorsPull _decoratorsPull;

        internal void Initialize(IPlayerDecoratorsPull decoratorsPull)
        {
            _decoratorsPull = decoratorsPull;
            InitializeDictionaryByTeams();
        }

        internal void SpawnOnAnyPoint(PlayerDecorator playerDecorator)
        {
            foreach (var spawnPoint in _teamSpawnPoints[playerDecorator.Team])
            {
                if (spawnPoint.CanSpawn())
                {
                    playerDecorator.transform.position = spawnPoint.Position;
                    return;
                }
            }
        }

        internal void NetworkSpawnPlayer(PlayerInfo playerInfo, GameTeam team)
        {
            var playerDecorator = Instantiate(_playerDecoratorPrefab);

            playerDecorator.BindPlayerInfo(playerInfo);
            playerDecorator.SetTeam(team);
            playerDecorator.NetworkObject.SpawnWithOwnership(playerInfo.Id, true);
            playerDecorator.NetworkObject.DontDestroyWithOwner = true;

            _decoratorsPull.AddDecorator(playerInfo.GUID.ToString(), playerDecorator);
        }

        private void InitializeDictionaryByTeams() 
        {
            _teamSpawnPoints = new();

            foreach (var spawnPoint in _spawnPoints)
            {
                if (_teamSpawnPoints.ContainsKey(spawnPoint.Team))
                    _teamSpawnPoints[spawnPoint.Team].Add(spawnPoint);

                else
                    _teamSpawnPoints.Add(spawnPoint.Team, new() { spawnPoint });
            }
        }
    }
}
