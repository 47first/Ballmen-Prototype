using Ballmen.Scene;
using Ballmen.InGame.Server;
using UnityEngine;

namespace Ballmen.InGame
{
    internal class InGameScenePresenter : NetworkScenePresenter
    {
        [SerializeField] private InGameLocalConfigurator _localClientConfigurator;
        [SerializeField] private InGameServerConfigurator _serverConfigurator;

        protected override void OnEnteringScene()
        {
            Debug.Log("On In Game Entering");

            _serverConfigurator.Configure();
            _localClientConfigurator.Configure();
        }
    }
}
