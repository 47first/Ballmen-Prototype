using Ballmen.Session;
using Unity.Netcode;

namespace Ballmen.Lobby
{
    internal class LobbyModel
    {
        private NetworkList<PlayerInfo> _players;
        public NetworkList<PlayerInfo> Players => _players;
        public LobbyModel(NetworkList<PlayerInfo> players) 
        {
            _players = players;
        }
    }
}
