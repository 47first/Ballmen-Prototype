using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ballmen.Scene
{
    [DisallowMultipleComponent] // Can be used if there's network manager already
    public abstract class NetworkScenePresenter : MonoBehaviour
    {
        protected abstract void OnEnteringScene();
        protected virtual void OnNetworkLoadedScene() { }

        private void Awake()
        {
            var networkManager = NetworkManager.Singleton;

            if (networkManager == null)
                throw new InvalidOperationException("To use NetworkScenePresenter " +
                    "you must make sure that Network Manager already in scene");

            networkManager.SceneManager.OnLoadComplete += OnLoaded;
        }

        private void Start()
        {
            OnEnteringScene();
        }

        private void OnDestroy()
        {
            var networkManager = NetworkManager.Singleton;

            if(networkManager != null)
                networkManager.SceneManager.OnLoadComplete -= OnLoaded;
        }

        private void OnLoaded(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            if (NetworkManager.Singleton.LocalClientId == clientId)
                OnNetworkLoadedScene();
        }
    }
}
