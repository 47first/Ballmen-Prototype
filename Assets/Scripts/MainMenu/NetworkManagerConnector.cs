using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ballmen.Scene;

public class NetworkManagerConnector : MonoBehaviour
{
    public void StartClient()
    {
        var networkManager = NetworkManager.Singleton;

        networkManager.Shutdown();
        networkManager.StartClient();
    }

    public void StartHost()
    {
        var networkManager = NetworkManager.Singleton;

        networkManager.Shutdown();
        networkManager.StartHost();

        var lobbySceneName = SceneNames.GetByEnum(SceneEnum.Lobby);
        networkManager.SceneManager.LoadScene(lobbySceneName, LoadSceneMode.Single);
    }
}
