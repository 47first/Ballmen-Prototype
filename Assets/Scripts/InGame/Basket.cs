using Ballmen.InGame.Server;
using Ballmen.Session;
using UnityEngine;

namespace Ballmen.InGame
{
    public class Basket : MonoBehaviour
    {
        private bool _isInitializedByServer = false;
        [SerializeField] private GameTeam _team;
        private IPlayerDecoratorsPull _playerDecorators;
        private ServerGameFlow _serverGameFlow;

        internal void Initialize(IPlayerDecoratorsPull playerDecoratorsPull, ServerGameFlow serverGameFlow)
        {
            _isInitializedByServer = true;
            _playerDecorators = playerDecoratorsPull;
            _serverGameFlow = serverGameFlow;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isInitializedByServer == false)
                return;

            var inBasketPlayerDecorator = _playerDecorators.GetDecorator(other.gameObject);

            Debug.Log($"Team {inBasketPlayerDecorator.Team} in basket");

            if (inBasketPlayerDecorator.Team == _team)
            {
                GameTeam goaledTeam = inBasketPlayerDecorator.Team switch
                {
                    GameTeam.Red => GameTeam.Blue,
                    GameTeam.Blue => GameTeam.Red,
                    _ => throw new System.InvalidOperationException("Team is not registered")
                };

                Debug.Log("Goal!");

                _serverGameFlow.AddScore(goaledTeam, 1);
            }
        }
    }
}
