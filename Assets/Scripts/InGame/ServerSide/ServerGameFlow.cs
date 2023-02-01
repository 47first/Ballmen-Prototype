using Unity.Netcode;
using UnityEngine;

namespace Ballmen.InGame
{
    public class ServerGameFlow : MonoBehaviour
    {
        [SerializeField] private GameFlowInfo _gameFlowInfoPrefab;

        private void Start()
        {
            var networkManager = NetworkManager.Singleton;
            var gameFlowInstance = Instantiate(_gameFlowInfoPrefab);

            gameFlowInstance.NetworkObject.SpawnWithOwnership(networkManager.LocalClientId);
        }
    }
}
