using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ballmen.Scene;

public class NetworkManagerConnector : MonoBehaviour
{
    private NetworkManager _networkManager;

    public void StartClient()
    {
        _networkManager.Shutdown();

        _networkManager.StartClient();
    }

    public void StartHost()
    {
        _networkManager.Shutdown();

        _networkManager.StartHost();

        var lobbySceneName = SceneNames.GetByEnum(SceneEnum.Lobby);
        _networkManager.SceneManager.LoadScene(lobbySceneName, LoadSceneMode.Single);
    }

    private void Start()
    {
        _networkManager = NetworkManager.Singleton;
    }
}
