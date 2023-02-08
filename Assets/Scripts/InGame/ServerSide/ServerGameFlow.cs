using Unity.Netcode;
using UnityEngine;
using Ballmen.Session;

namespace Ballmen.InGame.Server
{
    public class ServerGameFlow : MonoBehaviour
    {
        [SerializeField] private GameFlowInfo _gameFlowInfoPrefab;
        private IPlayerDecoratorsPull _playerDecoratorsPull;
        private IPlayerTeleporter _playerTeleporter;
        private IGameFlowInfo _gameFlowInfo;
        private ISessionInfo _sessionInfo;

        internal void Initialize(IPlayerDecoratorsPull decoratorsPull, IPlayerTeleporter playerTeleporter)
        {
            var networkManager = NetworkManager.Singleton;
            var gameFlowInstance = Instantiate(_gameFlowInfoPrefab);

            gameFlowInstance.NetworkObject.SpawnWithOwnership(networkManager.LocalClientId);
            GameFlowInfo.SetSingleton(gameFlowInstance);

            _gameFlowInfo = GameFlowInfo.Singleton;
            _sessionInfo = SessionInfo.Singleton;
            _playerDecoratorsPull = decoratorsPull;
            _playerTeleporter = playerTeleporter;

            Debug.Assert(_gameFlowInfo != null);
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

            StartNewRound();
        }

        private void StartNewRound() 
        {

            if (IsThereAnyWinners(out GameTeam winnerTeam))
                AnnounceWinners(winnerTeam);

            else
            {
                foreach (var decorator in _playerDecoratorsPull.GetDecoratorsEnumerable())
                {
                    Debug.Log($"{decorator.name} is{(decorator.IsSpawned ? "" : "n\'t")} spawned");

                    if (decorator.IsSpawned)
                        _playerTeleporter.TeleportToAnySpawnPoint(decorator);
                }
            }
        }

        private void AnnounceWinners(GameTeam winnerTeam) 
        {
            
        }

        private bool IsThereAnyWinners(out GameTeam winnerTeam)
        {
            var gameSettings = _sessionInfo.GameSettings;

            winnerTeam = GameTeam.None;

            if (_gameFlowInfo.RedTeamScore.Value >= gameSettings.ScoreLimit)
                winnerTeam = GameTeam.Red;

            else if (_gameFlowInfo.BlueTeamScore.Value >= gameSettings.ScoreLimit)
                winnerTeam = GameTeam.Blue;

            return winnerTeam != GameTeam.None;
        }
    }
}
