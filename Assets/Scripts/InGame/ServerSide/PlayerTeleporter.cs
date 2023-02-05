using Ballmen.InGame.Player;
using Ballmen.Session;
using System.Collections.Generic;
using UnityEngine;

namespace Ballmen.InGame.Server
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
            Vector3 position = Vector3.zero;

            foreach (var spawnPoint in _teamSpawnPoints[playerDecorator.Team])
            {
                if (spawnPoint.CanSpawn())
                {
                    position = spawnPoint.Position;
                    break;
                }
            }

            if (position == Vector3.zero)
            {
                Debug.LogWarning("There are no free spawn points!");
                position = _teamSpawnPoints[playerDecorator.Team][0].Position;
            }

            playerDecorator.SetPosition(position);
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
