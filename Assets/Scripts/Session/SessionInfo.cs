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
        public NetworkList<PlayerInfo> Players { get; }
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
        private NetworkList<PlayerInfo> _players;
        private IGameSettings _gameSettings;
        private ISessionPresenter _presenter;
        private SessionState _state;
        //Events
        private UnityEvent<PlayerInfo> _onPlayerDisconnected = new();
        private UnityEvent<PlayerInfo> _onPlayerApproved = new();
        private UnityEvent<PlayerInfo> _onPlayerConnected = new();

        public static SessionInfo Singleton => _instance;

        NetworkList<PlayerInfo> ISessionInfo.Players => _players;

        IGameSettings ISessionInfo.GameSettings => _gameSettings;

        SessionState ISessionInfo.State => _state;

        public UnityEvent<PlayerInfo> OnPlayerDisconnected => _onPlayerDisconnected;

        public UnityEvent<PlayerInfo> OnPlayerApproved => _onPlayerApproved;

        public UnityEvent<PlayerInfo> OnPlayerConnected => _onPlayerConnected;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            SetSingletonInstance(this);

            if (NetworkManager.IsHost)
            {
                var initialPlayer = GetHostPlayerInfo();
                SetPresetner(new SessionPresenter(this, initialPlayer));
                InitializeGameSettings();
            }
        }

        public override void OnNetworkDespawn()
        {
            RemoveSingletonInstance();

            if (NetworkManager.IsHost)
            {
                RemovePresetner();
                RemoveGameSettings();
            }

            base.OnNetworkDespawn();

            SceneManager.LoadScene(SceneNames.GetByEnum(SceneEnum.MainMenu), LoadSceneMode.Single);
        }

        private void Awake()
        {
            _players = new(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Server);
        }

        private void OnDestroy()
        {
            _players.Dispose();
        }

        private PlayerInfo GetHostPlayerInfo() => new(NetworkManager.LocalClientId, LocalClientInfo.GetLocal());

        private void SetSingletonInstance(SessionInfo instance) => _instance = instance;

        private void RemoveSingletonInstance() => _instance = null;

        private void SetPresetner(ISessionPresenter presenter) => _presenter = presenter;

        private void RemovePresetner() 
        { 
            _presenter?.Dispose();
            _presenter = null;
        }

        private void InitializeGameSettings() => _gameSettings = GameSettings.Default;

        private void RemoveGameSettings() => _gameSettings = null;

        void ISessionInfo.ChangeState(SessionState state)
        {
            _state = state;
        }
    }
}
