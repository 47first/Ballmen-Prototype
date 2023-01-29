using Ballmen.Session;
using System;
using Unity.Netcode;

namespace Ballmen.Server
{
    internal interface IPlayerConnectionController : IDisposable
    {
        internal void OnPlayerListChange(NetworkListEvent<PlayerInfo> changeEvent);
    }

    public class PlayerConnectionController : IPlayerConnectionController
    {
        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        void IPlayerConnectionController.OnPlayerListChange(NetworkListEvent<PlayerInfo> changeEvent)
        {
            if (changeEvent.Type == NetworkListEvent<PlayerInfo>.EventType.Remove)
            {
                OnPlayerDisconnected();
            }

            else if (changeEvent.Type == NetworkListEvent<PlayerInfo>.EventType.Add)
            {
                OnPlayerReconnected();
            }
        }

        private void OnPlayerReconnected()
        {
            /*
            var connectedPlayer = _playersPull.GetPlayer(changeEvent.Value);

            connectedPlayer.gameObject.transform.position = Vector3.zero;
            connectedPlayer.NetworkObject.ChangeOwnership(changeEvent.Value.Id);
            connectedPlayer.gameObject.SetActive(true);

            Debug.Log($"{connectedPlayer.name} were appear");
            */
        }

        private void OnPlayerDisconnected()
        {
            /*
            var player = _playersPull.GetPlayer(changeEvent.PreviousValue);

            player.NetworkObject.RemoveOwnership();
            player.gameObject.SetActive(false);

            Debug.Log($"{player.name} were hiden");
            */
        }
    }
}
