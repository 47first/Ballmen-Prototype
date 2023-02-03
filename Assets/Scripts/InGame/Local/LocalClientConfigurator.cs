using Ballmen.Player;
using System;
using UnityEngine;

namespace Ballmen.InGame
{
    public class LocalClientConfigurator : MonoBehaviour, IDisposable
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private ScoreView _scoreView;

        public void Dispose()
        {
            _scoreView.Dispose();
        }

        internal void Configure() 
        {
            var localPlayerDecorator = PlayerDecorator.Local;
            var gameFlowInfo = GameFlowInfo.Singleton;

            Debug.Log($"localPlayerDecorator == null = {localPlayerDecorator == null}");
            Debug.Log($"gameFlowInfo == null = {gameFlowInfo == null}");

            _cameraController.SetTarget(localPlayerDecorator.transform);
            _scoreView.Bind(gameFlowInfo);
        }

    }
}
