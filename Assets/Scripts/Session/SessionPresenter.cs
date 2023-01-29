using System;
using Unity.Netcode;
using UnityEngine;
using Ballmen.Client;

namespace Ballmen.Session
{
    internal interface ISessionPresenter : IDisposable { }

    internal sealed class SessionPresenter : ISessionPresenter
    {
        private ISessionInfo _sessionInfo;
        private NetworkManager _networkManager;

        public SessionPresenter(ISessionInfo sessionInfo, PlayerInfo initialPlayer)
        {
            _networkManager = NetworkManager.Singleton;

            _sessionInfo = sessionInfo;

            _networkManager.OnClientDisconnectCallback += RemovePlayer;
            _networkManager.ConnectionApprovalCallback = ConnectionApproval;

            AddPlayer(initialPlayer);
        }

        public void Dispose()
        {
            _networkManager.OnClientDisconnectCallback -= RemovePlayer;
        }

        public void AddPlayer(PlayerInfo player) => _sessionInfo.Players.Add(player);

        private void RemovePlayer(ulong clientId)
        {
            for(int i = 0; i < _sessionInfo.Players.Count; i++)
            {
                if (_sessionInfo.Players[i].Id == clientId)
                {
                    _sessionInfo.Players.RemoveAt(i);
                    return;
                }
            }
            Debug.LogError("Player unfounded");
        }

        public void ConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse responce)
        {
            var clientInfo = ClientInfo.GetFromBytes(request.Payload);

            if (_sessionInfo.State == SessionState.GatheringPlayers &&
                _sessionInfo.Players.Count < _sessionInfo.GameSettings.PlayerLimit)
            {
                responce.Approved = true;
                AddPlayer(new(request.ClientNetworkId, clientInfo));

                Debug.Log($"{clientInfo.Nickname} approved!");
            }

            else
            {
                responce.Approved = false;
                responce.Reason = "Can't connect to the game";

                Debug.Log($"{clientInfo.Nickname} unapproved!");
            }
        }
    }
}
