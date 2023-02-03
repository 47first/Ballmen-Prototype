using Ballmen.Scene;
using Ballmen.Server;
using UnityEngine;

namespace Ballmen.InGame
{
    internal class InGameScenePresenter : NetworkScenePresenter
    {
        [SerializeField] private LocalClientConfigurator _localClientConfigurator;
        [SerializeField] private ServerConfigurator _serverConfigurator;

        protected override void OnEnteringScene()
        {
            Debug.Log("On In Game Entering");

            _serverConfigurator.Configure();
            _localClientConfigurator.Configure();
        }

        protected override void OnLeavingScene()
        {
            _localClientConfigurator.Dispose();
            _serverConfigurator.Dispose();
        }
    }
}
