using Ballmen.InGame.Player;
using System;
using UnityEngine;

namespace Ballmen.InGame
{
    public class InGameLocalConfigurator : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private ScoreView _scoreView;

        internal void Configure() 
        {
            var localPlayerDecorator = PlayerDecorator.Local;
            var gameFlowInfo = GameFlowInfo.Singleton;

            Debug.Assert(localPlayerDecorator != null);
            Debug.Assert(gameFlowInfo != null);

            _cameraController.SetTarget(localPlayerDecorator.transform);
            _scoreView.Bind(gameFlowInfo);
        }

    }
}
