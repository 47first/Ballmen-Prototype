using Unity.Netcode;
using UnityEngine;

namespace Ballmen
{
    internal class CustomNetworkManager : NetworkManager
    {
        private static CustomNetworkManager _instance;

        internal void Start()
        {
            if (_instance == null)
                _instance = new CustomNetworkManager();

            else
                Destroy(gameObject);
        }
    }
}
