using System;
using TMPro;
using UnityEngine;

namespace Ballmen.InGame
{
    internal sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _redTeamScoreTMP;
        [SerializeField] private TextMeshProUGUI _blueTeamScoreTMP;
        private IGameFlowInfo _gameFlowInfo;

        private void OnDestroy()
        {
            if (_gameFlowInfo != null)
            {
                _gameFlowInfo.RedTeamScore.OnValueChanged -= ChangeRedTeamValue;
                _gameFlowInfo.BlueTeamScore.OnValueChanged -= ChangeBlueTeamValue;
            }
        }

        internal void Bind(IGameFlowInfo gameFlowInfo)
        {
            _gameFlowInfo = gameFlowInfo;

            ChangeRedTeamValue(0, _gameFlowInfo.RedTeamScore.Value);
            ChangeBlueTeamValue(0, _gameFlowInfo.BlueTeamScore.Value);

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
