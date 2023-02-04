using Unity.Netcode;
using UnityEngine;
using Ballmen.Session;

namespace Ballmen.InGame.Server
{
    public class ServerGameFlow : MonoBehaviour
    {
        [SerializeField] private GameFlowInfo _gameFlowInfoPrefab;
        private IGameFlowInfo _gameFlowInfo;

        internal void Initialize()
        {
            var networkManager = NetworkManager.Singleton;
            var gameFlowInstance = Instantiate(_gameFlowInfoPrefab);

            gameFlowInstance.NetworkObject.SpawnWithOwnership(networkManager.LocalClientId);
            GameFlowInfo.SetSingleton(gameFlowInstance);

            _gameFlowInfo = GameFlowInfo.Singleton;

            if(_gameFlowInfo == null)
                Debug.LogWarning("GameFlowInfo is null");
        }

        internal void AddScore(GameTeam team, int score) 
        {
            NetworkVariable<int> teamScoreRef = team switch
            {
                GameTeam.Red => _gameFlowInfo.RedTeamScore,
                GameTeam.Blue => _gameFlowInfo.BlueTeamScore,
                _ => throw new System.InvalidOperationException("Team is not registered")
            };

            teamScoreRef.Value += score;
        }
    }
}
