using Unity.Netcode;
using UnityEngine.SceneManagement;
using Ballmen.Scene;
using Ballmen.Client;

namespace Ballmen.Session
{
    internal interface ISessionInfo 
    {
        public NetworkList<PlayerInfo> Players { get; }
        public IGameSettings GameSettings { get; }
    }

    public class SessionInfo : NetworkBehaviour, ISessionInfo
    {
        private static SessionInfo _instance;

        private NetworkList<PlayerInfo> _players;
        private IGameSettings _gameSettings;
        private ISessionPresenter _presenter;

        public static SessionInfo Singleton => _instance;
        NetworkList<PlayerInfo> ISessionInfo.Players => _players;
        IGameSettings ISessionInfo.GameSettings => _gameSettings;

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

        private PlayerInfo GetHostPlayerInfo() => new(NetworkManager.LocalClientId, ClientInfo.GetLocal());

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
    }
}
