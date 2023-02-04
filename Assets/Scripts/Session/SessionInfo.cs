using Unity.Netcode;
using UnityEngine.SceneManagement;
using Ballmen.Scene;
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
        public PlayerStateContainer PlayersStates { get; }
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
        private TeamDistributor _teamDistributor;
        private IGameSettings _gameSettings;
        private ISessionPresenter _presenter;
        private SessionState _state;
        //Events
        private UnityEvent<PlayerInfo> _onPlayerDisconnected = new();
        private UnityEvent<PlayerInfo> _onPlayerApproved = new();
        private UnityEvent<PlayerInfo> _onPlayerConnected = new();

        public static SessionInfo Singleton => _instance;

        NetworkList<PlayerInfo> ISessionInfo.ConnectedPlayers => _connectedPlayers;

        IGameSettings ISessionInfo.GameSettings => _gameSettings;

        SessionState ISessionInfo.State => _state;

        public UnityEvent<PlayerInfo> OnPlayerDisconnected => _onPlayerDisconnected;

        public UnityEvent<PlayerInfo> OnPlayerApproved => _onPlayerApproved;

        public UnityEvent<PlayerInfo> OnPlayerConnected => _onPlayerConnected;

        PlayerStateContainer ISessionInfo.PlayersStates => ((ISessionInfo)_instance).PlayersStates;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            _instance = this;

            if (NetworkManager.IsHost)
            {
                var initialPlayer = GetHostPlayerInfo();

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

            SceneManager.LoadScene(SceneNames.GetByEnum(SceneEnum.MainMenu), LoadSceneMode.Single);
        }

        void ISessionInfo.ChangeState(SessionState state)
        {
            _state = state;
        }

        private void Awake()
        {
            _connectedPlayers = new(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Server);
        }

        private void OnDestroy() 
        {
            _connectedPlayers.Dispose();
        }

        private PlayerInfo GetHostPlayerInfo() => new(NetworkManager.LocalClientId, LocalClientInfo.GetLocal());

        private void InitializeGameSettings() => _gameSettings = GameSettings.Default;
    }
}
