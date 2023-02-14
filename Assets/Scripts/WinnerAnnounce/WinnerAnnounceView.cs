using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ballmen.WinnerAnnouncer
{
    internal sealed class WinnerAnnounceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _winnerTmp;
        [SerializeField] private Button _tryReconnectButton;
        [SerializeField] private Button _backToMenuButton;
        private IWinnerAnnouncePresetner _presenter;

        internal void Initialize(GameResult gameResult) 
        {
            _presenter = new WinnerAnnouncePresenter();

            UpdateView(gameResult);
            BindButtonsEvents(_presenter);
        }

        private void UpdateView(GameResult gameResult) 
        {
            _winnerTmp.text = $"{gameResult._winnerTeam} Wins!";
            _winnerTmp.color = gameResult._winnerTeam == Session.GameTeam.Red ?
                Color.red : Color.blue;
        }

        private void BindButtonsEvents(IWinnerAnnouncePresetner presenter) 
        {
            _tryReconnectButton.onClick.AddListener(presenter.OnTryReconnectButtonClick);
            _backToMenuButton.onClick.AddListener(presenter.OnToMenuButtonClick);
        }
    }
}
