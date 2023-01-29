using Unity.Netcode;
using UnityEngine;
using Ballmen.Client;

namespace Ballmen.Session
{
    public class SessionInfoSpawner : MonoBehaviour
    {
        [SerializeField] private SessionInfo _sessionPrefab;
        private NetworkManager _networkManager;

        public void Start()
        {
            _networkManager = NetworkManager.Singleton;

            _networkManager.NetworkConfig.ConnectionData = ClientInfo.GetLocal().GetBytes();
            Debug.Log($"Connection data size: {_networkManager.NetworkConfig.ConnectionData.Length}");

            _networkManager.OnServerStarted += SpawnSessionInfo;
        }

        private void SpawnSessionInfo()
        {
            var instance = Instantiate(_sessionPrefab);

            instance.NetworkObject.SpawnWithOwnership(_networkManager.LocalClientId);
        }
    }
}
