using Ballmen.InGame;
using Ballmen.Server;
using Ballmen.Session;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private GameTeam _team;
    private bool _isInitializedByServer = false;
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

        if (inBasketPlayerDecorator.PlayerInfo.Team == _team)
        {
            GameTeam goaledTeam = inBasketPlayerDecorator.PlayerInfo.Team switch
            {
                GameTeam.Red => GameTeam.Blue,
                GameTeam.Blue => GameTeam.Red
            };

            _serverGameFlow.AddScore(goaledTeam, 1);
        }
    }
}
