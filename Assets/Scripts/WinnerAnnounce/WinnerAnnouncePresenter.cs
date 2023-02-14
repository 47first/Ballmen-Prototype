using Ballmen.Scene;
using Ballmen.Services;
using Unity.Netcode;

namespace Ballmen.WinnerAnnouncer
{
    internal interface IWinnerAnnouncePresetner
    {
        internal void OnToMenuButtonClick();
        internal void OnTryReconnectButtonClick();
    }

    internal sealed class WinnerAnnouncePresenter : IWinnerAnnouncePresetner
    {
        void IWinnerAnnouncePresetner.OnToMenuButtonClick()
        {
            var sceneChanger = DependencyInjectionService.Singleton.GetDependency<ISceneChanger>();

            sceneChanger.ChangeToMainMenu();
        }

        void IWinnerAnnouncePresetner.OnTryReconnectButtonClick()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
