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
            _networkManager.OnClientConnectedCallback += ClientConnected;
            _networkManager.ConnectionApprovalCallback = ConnectionApproval;

            AddPlayer(initialPlayer);
        }

        private void ClientConnected(ulong clientId)
        {
            foreach (var playerInfo in _sessionInfo.ConnectedPlayers)
            {
                if (playerInfo.Id == clientId)
                {
                    //Require player founded
                    _sessionInfo.OnPlayerConnected.Invoke(playerInfo);
                    return;
                }
            }
        }

        public void Dispose()
        {
            _networkManager.OnClientDisconnectCallback -= RemovePlayer;
        }

        private void AddPlayer(PlayerInfo player)
        {
            _sessionInfo.ConnectedPlayers.Add(player);

            if (_sessionInfo.State == SessionState.GatheringPlayers)
                _sessionInfo.PlayersStates.AddState(player.GUID.ToString());
        }

        private void RemovePlayer(ulong clientId)
        {
            for(int i = 0; i < _sessionInfo.ConnectedPlayers.Count; i++)
            {
                if (_sessionInfo.ConnectedPlayers[i].Id == clientId)
                {
                    var removePlayerInfo = _sessionInfo.ConnectedPlayers[i];

                    if (_sessionInfo.State == SessionState.GatheringPlayers)
                        _sessionInfo.PlayersStates.RemoveState(removePlayerInfo.GUID.ToString());

                    _sessionInfo.OnPlayerDisconnected.Invoke(removePlayerInfo);
                    _sessionInfo.ConnectedPlayers.RemoveAt(i);

                    return;
                }
            }
            Debug.LogError("Player unfounded");
        }

        private void ConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse responce)
        {
            var clientInfo = ClientInfo.GetFromBytes(request.Payload);

            if (IsPlayerApproved(clientInfo.Guid))
            {
                var approvedPlayer = new PlayerInfo(request.ClientNetworkId, clientInfo);

                responce.Approved = true;
                AddPlayer(approvedPlayer);
                _sessionInfo.OnPlayerApproved.Invoke(approvedPlayer);

                Debug.Log($"{clientInfo.Nickname} approved!");
            }

            else
            {
                responce.Approved = false;
                responce.Reason = "Can't connect to the game";

                Debug.Log($"{clientInfo.Nickname} unapproved!");
            }
        }

        private bool IsPlayerApproved(string guid) 
        {
            return (_sessionInfo.State == SessionState.GatheringPlayers &&
                _sessionInfo.ConnectedPlayers.Count < _sessionInfo.GameSettings.PlayerLimit) || 
                (_sessionInfo.State == SessionState.InGame && 
                ((IPlayerStateContainer)_sessionInfo.PlayersStates).Contains(guid));
        }
    }
}
