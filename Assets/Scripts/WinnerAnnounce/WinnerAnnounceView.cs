using TMPro;
using UnityEngine;

namespace Ballmen.WinnerAnnouncer
{
    internal sealed class WinnerAnnounceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _winnerTmp;

        internal void UpdateView(GameResult gameResult) 
        {
            _winnerTmp.text = $"{gameResult._winnerTeam} Wins!";
            _winnerTmp.color = gameResult._winnerTeam == Session.GameTeam.Red ?
                Color.red : Color.blue;
        }
    }
}
