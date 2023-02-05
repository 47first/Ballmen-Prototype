using Ballmen.InGame.Player;
using Ballmen.Session;
using System.Collections.Generic;
using UnityEngine;

namespace Ballmen.InGame.Server
{
    internal interface IPlayerSpawner 
    {
        internal void NetworkSpawnPlayer(PlayerInfo playerInfo, GameTeam team);
    }

    internal sealed class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerDecorator _playerDecoratorPrefab;
        private IPlayerDecoratorsPull _decoratorsPull;
        private IPlayerTeleporter _playerTeleporter;

        internal void Initialize(IPlayerDecoratorsPull decoratorsPull, IPlayerTeleporter playerTeleporter)
        {
            _decoratorsPull = decoratorsPull;
            _playerTeleporter = playerTeleporter;
        }

        internal void NetworkSpawnPlayer(PlayerInfo playerInfo, GameTeam team)
        {
            var playerDecorator = Instantiate(_playerDecoratorPrefab);

            playerDecorator.BindPlayerInfo(playerInfo);
            playerDecorator.SetTeam(team);

            _playerTeleporter.TeleportToAnySpawnPoint(playerDecorator);

            playerDecorator.NetworkObject.SpawnWithOwnership(playerInfo.Id, true);
            playerDecorator.NetworkObject.DontDestroyWithOwner = true;

            _decoratorsPull.AddDecorator(playerInfo.GUID.ToString(), playerDecorator);
        }
    }
}
