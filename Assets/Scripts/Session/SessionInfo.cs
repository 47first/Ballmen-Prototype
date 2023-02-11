using Unity.Netcode;
using Ballmen.Client;
using UnityEngine.Events;

namespace Ballmen.Session
{
    internal enum SessionState 
    {
        GatheringPlayers,
        InGame,
        WaitForHost
    }

    internal interface ISessionInfo 
    {
        public NetworkList<PlayerInfo> ConnectedPlayers { get; }
        public IPlayerStateContainer PlayersStates { get; }
        public UnityEvent<PlayerInfo> OnPlayerDisconnected { get; }
        public UnityEvent<PlayerInfo> OnPlayerApproved { get; }
        public UnityEvent<PlayerInfo> OnPlayerConnected { get; }
        public IGameSettings GameSettings { get; }
        public SessionState State { get; }
        public void ChangeState(SessionState state);
    }

    public class SessionInfo : NetworkBehaviour, ISessionInfo
    {
        private static SessionInfo _instance;
        private NetworkList<PlayerInfo> _connectedPlayers;
        private IGameSettings _gameSettings;
        private ISessionPresenter _presenter;
        private IPlayerStateContainer _playerStateContainer;
        private SessionState _state;
        //Events
        private readonly UnityEvent<PlayerInfo> _onPlayerDisconnected = new();
        private readonly UnityEvent<PlayerInfo> _onPlayerApproved = new();
        private readonly UnityEvent<PlayerInfo> _onPlayerConnected = new();

        public static SessionInfo Singleton => _instance;

        NetworkList<PlayerInfo> ISessionInfo.ConnectedPlayers => _connectedPlayers;

        IPlayerStateContainer ISessionInfo.PlayersStates => _playerStateContainer;

        IGameSettings ISessionInfo.GameSettings => _gameSettings;

        SessionState ISessionInfo.State => _state;

        public UnityEvent<PlayerInfo> OnPlayerDisconnected => _onPlayerDisconnected;

        public UnityEvent<PlayerInfo> OnPlayerApproved => _onPlayerApproved;

        public UnityEvent<PlayerInfo> OnPlayerConnected => _onPlayerConnected;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            _instance = this;

            if (NetworkManager.IsHost)
            {
                var initialPlayer = GetHostPlayerInfo();

                _playerStateContainer = new PlayerStateContainer();
                InitializeGameSettings();
                _presenter = new SessionPresenter(this, initialPlayer);
            }
        }

        public override void OnNetworkDespawn()
        {
            _instance = null;

            if (NetworkManager.IsHost)
                _presenter.Dispose();

            base.OnNetworkDespawn();
        }

        void ISessionInfo.ChangeState(SessionState state)
        {
            _state = state;
        }

        private void Awake()
        {
            _connectedPlayers = new(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Server);
        }

        public override void OnDestroy() 
        {
            _connectedPlayers?.Dispose();

            base.OnDestroy();
        }

        private PlayerInfo GetHostPlayerInfo() => new(NetworkManager.LocalClientId, LocalClientInfo.GetLocal());

        private void InitializeGameSettings() => _gameSettings = GameSettings.Default;
    }
}
