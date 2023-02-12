using Ballmen.WinnerAnnouncer;
using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace Ballmen.Scene
{
    internal interface ISceneChanger 
    {
        internal void ChangeToAnnounceWinnerScene(GameResult gameResult);
        internal void ChangeToMainMenu(string message = "");
    }

    internal sealed class SceneChanger : ISceneChanger
    {
        void ISceneChanger.ChangeToAnnounceWinnerScene(GameResult gameResult)
        {
            var winnerAnnounceSceneName = SceneNames.GetByEnum(SceneEnum.WinnerAnnounce);
            NetworkManager.Singleton.Shutdown();

            WinnerAnnounceSceneEntryInfo.SetData(gameResult);

            SceneManager.LoadScene(winnerAnnounceSceneName);
        }

        void ISceneChanger.ChangeToMainMenu(string message)
        {
            var mainMenuSceneName = SceneNames.GetByEnum(SceneEnum.MainMenu);
            NetworkManager.Singleton.Shutdown();

            //Bind data to singleton here...

            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
