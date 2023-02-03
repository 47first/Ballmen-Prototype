using System;
using TMPro;
using UnityEngine;

namespace Ballmen.InGame
{
    internal sealed class ScoreView : MonoBehaviour, IDisposable
    {
        [SerializeField] private TextMeshProUGUI _redTeamScoreTMP;
        [SerializeField] private TextMeshProUGUI _blueTeamScoreTMP;
        private IGameFlowInfo _gameFlowInfo;

        public void Dispose()
        {
            _gameFlowInfo.RedTeamScore.OnValueChanged -= ChangeRedTeamValue;
            _gameFlowInfo.BlueTeamScore.OnValueChanged -= ChangeBlueTeamValue;

            _gameFlowInfo = null;
        }

        internal void Bind(IGameFlowInfo gameFlowInfo)
        {
            _gameFlowInfo = gameFlowInfo;

            _gameFlowInfo.RedTeamScore.OnValueChanged += ChangeRedTeamValue;
            _gameFlowInfo.BlueTeamScore.OnValueChanged += ChangeBlueTeamValue;
        }

        private void ChangeRedTeamValue(int prev, int cur) 
        {
            _redTeamScoreTMP.text = cur.ToString();
        }

        private void ChangeBlueTeamValue(int prev, int cur)
        {
            _blueTeamScoreTMP.text = cur.ToString();
        }
    }
}
