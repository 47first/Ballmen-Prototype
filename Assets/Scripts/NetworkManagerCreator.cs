using Unity.Netcode;
using UnityEngine;

namespace Ballmen
{
    // Works as singleton for NetworkManager's GameObject,
    // without necessity to create another scene's instance
    public class NetworkManagerCreator : MonoBehaviour
    {
        private static NetworkManager _networkManagerInstance;
        [SerializeField] private NetworkManager _networkManagerPrefab;

        private void Start()
        {
            if(_networkManagerInstance == null)
                _networkManagerInstance = Instantiate(_networkManagerPrefab);
        }
    }
}
