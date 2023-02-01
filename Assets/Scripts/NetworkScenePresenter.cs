﻿using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ballmen.Scene
{
    [DisallowMultipleComponent] // Can be used if there's network manager already
    internal abstract class NetworkScenePresenter : MonoBehaviour
    {
        protected abstract void OnEnteringScene();
        protected virtual void OnLeavingScene() { }

        private void Awake()
        {
            var networkManager = NetworkManager.Singleton;

            if (networkManager == null)
                throw new InvalidOperationException("To use NetworkScenePresenter " +
                    "you must make sure that Network Manager already in scene");
            
            networkManager.SceneManager.OnLoadComplete += OnLoaded;

            networkManager.SceneManager.OnUnload += OnUnloadScene;
        }

        private void OnLoaded(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            if (NetworkManager.Singleton.LocalClientId == clientId)
                OnEnteringScene();
        }

        private void OnUnloadScene(ulong clientId, string sceneName, AsyncOperation asyncOperation)
        {
            Debug.Log($"On {clientId} unload");

            if (NetworkManager.Singleton.LocalClientId == clientId)
            {
                UnbindAllEvents();

                OnLeavingScene();
            }
        }

        private void UnbindAllEvents() 
        {
            var networkManager = NetworkManager.Singleton;

            networkManager.SceneManager.OnLoadComplete -= OnLoaded;
            networkManager.SceneManager.OnUnload -= OnUnloadScene;
        }
    }
}