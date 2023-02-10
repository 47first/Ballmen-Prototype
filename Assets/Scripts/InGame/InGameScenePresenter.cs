using Ballmen.Scene;
using Ballmen.InGame.Server;
using UnityEngine;

namespace Ballmen.InGame
{
    internal class InGameScenePresenter : NetworkScenePresenter
    {
        [SerializeField] private InGameLocalConfigurator _localClientConfigurator;
        [SerializeField] private InGameServerConfigurator _serverConfigurator;

        protected override void OnSynchronizeWithScene()
        {
            Debug.Log("On In Game Entering");

            _serverConfigurator.Configure();
            //_localClientConfigurator.Configure();
        }

        protected override void OnLeavingScene()
        {
            Debug.Log("On In Game Leaving");

            //_localClientConfigurator.Dispose();
            _serverConfigurator.Dispose();
        }
    }
}
