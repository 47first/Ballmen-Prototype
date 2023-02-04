using Ballmen.Scene;
using Ballmen.Session;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ballmen.Lobby
{
    internal interface ILobbyPresenter { }

    internal class LobbyPresenter : ILobbyPresenter
    {
        private ILobbyView _view;
        private ISessionInfo _session;
        private NetworkManager _networkManager;

        public LobbyPresenter(ILobbyView view) 
        {
            _view = view;
            _networkManager = NetworkManager.Singleton;

            if(_networkManager.IsClient)
                _networkManager.SceneManager.OnUnload += OnNextScene;

            if (_networkManager.IsHost == false)
                _networkManager.SceneManager.OnSynchronizeComplete += BindClientView;

            else
            {
                view.StartButton.onClick.AddListener(StartGame);
                BindClientView(_networkManager.LocalClientId);
            }
        }

        public void BindClientView(ulong clientId)
        {
            if (_networkManager.LocalClientId != clientId)
                return;

            _session = SessionInfo.Singleton;
            _session.ConnectedPlayers.OnListChanged += CallUpdateView;
            CallUpdateView(default);
        }

        public void OnNextScene(ulong clientId, string sceneName, AsyncOperation asyncOperation)
        {
            if (_networkManager.LocalClientId != clientId)
                return;

            if(_networkManager.IsHost == false)
                _networkManager.SceneManager.OnSynchronizeComplete -= BindClientView;

            _session.ConnectedPlayers.OnListChanged -= CallUpdateView;
            _networkManager.SceneManager.OnUnload -= OnNextScene;
        }

        private void CallUpdateView(NetworkListEvent<PlayerInfo> eventArgs)
        {
            var model = new LobbyModel(_session.ConnectedPlayers);

            _view.UpdateView(model);
        }

        private void StartGame() 
        {
            var inGameSceneName = SceneNames.GetByEnum(SceneEnum.InGame);

            _networkManager.SceneManager.LoadScene(inGameSceneName, LoadSceneMode.Single);
        }
    }
}
