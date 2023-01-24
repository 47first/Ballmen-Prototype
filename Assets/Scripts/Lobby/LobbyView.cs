using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Ballmen.Lobby 
{
    internal interface ILobbyView
    {
        internal void UpdateView(LobbyModel model);
        internal Button StartButton { get; }
    }

    internal class LobbyView : MonoBehaviour, ILobbyView
    {
        [SerializeField] private List<TextMeshProUGUI> _playerNameTmps;
        [SerializeField] private Button _startButton;
        private ILobbyPresenter _presenter;

        Button ILobbyView.StartButton => _startButton;

        private void Awake()
        {
            _presenter = new LobbyPresenter(this);
        }

        void ILobbyView.UpdateView(LobbyModel model)
        {
            var players = model.Players;

            Debug.Log("Current player amount " + players.Count);

            for (int i = 0; i < _playerNameTmps.Count; i++)
            {
                if (i < players.Count)
                    _playerNameTmps[i].text = players[i].Nickname.ToString();
                else
                    _playerNameTmps[i].text = "...";
            }

            if (NetworkManager.Singleton.IsHost && players.Count == 2)
                _startButton.interactable = true;
            else
                _startButton.interactable = false;
        }
    }
}
