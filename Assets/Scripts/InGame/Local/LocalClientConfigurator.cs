using Ballmen.Player;
using UnityEngine;

namespace Ballmen.InGame
{
    public class LocalClientConfigurator : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private ScoreView _scoreView;

        internal void Configure() 
        {
            var localPlayerDecorator = PlayerDecorator.Local;
            var gameFlowInfo = GameFlowInfo.Singleton;

            Debug.Log($"localPlayerDecorator == null = {localPlayerDecorator == null}");

            _cameraController.SetTarget(localPlayerDecorator.transform);
            _scoreView.Bind(gameFlowInfo);
        }
    }
}
