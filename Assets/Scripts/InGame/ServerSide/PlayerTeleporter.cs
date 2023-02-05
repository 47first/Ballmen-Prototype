using Ballmen.InGame.Player;
using Ballmen.Session;
using System.Collections.Generic;
using UnityEngine;

namespace Ballmen.InGame
{
    internal interface IPlayerTeleporter 
    {
        internal void TeleportToAnySpawnPoint(PlayerDecorator playerDecorator);
    }

    public class PlayerTeleporter : MonoBehaviour, IPlayerTeleporter
    {
        [SerializeField] private List<PlayerSpawnPoint> _spawnPoints;
        private Dictionary<GameTeam, List<PlayerSpawnPoint>> _teamSpawnPoints;

        void IPlayerTeleporter.TeleportToAnySpawnPoint(PlayerDecorator playerDecorator)
        {
            foreach (var spawnPoint in _teamSpawnPoints[playerDecorator.Team])
            {
                if (spawnPoint.CanSpawn())
                {
                    playerDecorator.transform.position = spawnPoint.Position;
                    return;
                }
            }

            Debug.LogWarning("There are no free spawn points!"); 
            playerDecorator.transform.position = _teamSpawnPoints[playerDecorator.Team][0].Position;
        }

        internal void Initialize()
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
