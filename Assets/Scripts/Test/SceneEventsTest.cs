using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEventsTest : MonoBehaviour
{
    void Awake()
    {
        NetworkManager.Singleton.SceneManager.OnLoad += OnLoad;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoaded;
        NetworkManager.Singleton.SceneManager.OnUnload += OnUnload;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += OnUnloaded;
    }

    private void OnUnloaded(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        Debug.Log($"player {clientId} unloaded");
    }

    private void OnUnload(ulong clientId, string sceneName, AsyncOperation asyncOperation)
    {
        Debug.Log($"player {clientId} unload");
    }

    private void OnLoaded(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        Debug.Log($"player {clientId} loaded");
    }

    private void OnLoad(ulong clientId, string sceneName, LoadSceneMode loadSceneMode, AsyncOperation asyncOperation)
    {
        Debug.Log($"player {clientId} load");
    }
}
