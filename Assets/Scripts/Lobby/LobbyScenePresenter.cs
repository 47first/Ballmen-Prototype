using Ballmen.Scene;
using Ballmen.Session;
using Unity.Netcode;

namespace Ballmen.Lobby
{
    public class LobbyScenePresenter : NetworkScenePresenter
    {
        protected override void OnEnteringScene()
        {
            var networkManager = NetworkManager.Singleton;

            if (networkManager.IsServer)
                ((ISessionInfo)SessionInfo.Singleton).ChangeState(SessionState.GatheringPlayers);
        }
    }
}
