using Ballmen.Session;
using Unity.Netcode;
using UnityEngine;

namespace Ballmen.InGame.Server
{
    internal interface IPlayerConnectionController
    {
        internal void OnPlayerReconnected(PlayerInfo reconnectedPlayer);
        internal void OnPlayerDisconnected(PlayerInfo disconnectedPlayer);
    }

    internal sealed class PlayerConnectionController : IPlayerConnectionController
    {
        private IPlayerDecoratorsPull _playerDecoratorsPull;
        internal PlayerConnectionController(IPlayerDecoratorsPull playerDecoratorsPull) 
        {
            _playerDecoratorsPull = playerDecoratorsPull;
        }

        void IPlayerConnectionController.OnPlayerReconnected(PlayerInfo reconnectedPlayerInfo)
        {
            Debug.Log($"Player reconnected!");

            var connectedPlayerDecorator = _playerDecoratorsPull.GetDecorator(reconnectedPlayerInfo);

            connectedPlayerDecorator.gameObject.SetActive(true);
            connectedPlayerDecorator.BindPlayerInfo(reconnectedPlayerInfo);
            connectedPlayerDecorator.NetworkObject.ChangeOwnership(reconnectedPlayerInfo.Id);

            Debug.Log($"{connectedPlayerDecorator.name} were appear");
        }

        void IPlayerConnectionController.OnPlayerDisconnected(PlayerInfo disconnectedPlayer)
        {
            var disconnectedPlayerDecorator = _playerDecoratorsPull.GetDecorator(disconnectedPlayer);

            disconnectedPlayerDecorator.NetworkObject.RemoveOwnership();
            disconnectedPlayerDecorator.gameObject.SetActive(false);

            Debug.Log($"{disconnectedPlayerDecorator.name} were hiden");
        }
    }
}
