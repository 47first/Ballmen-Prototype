using Unity.Netcode;
using UnityEngine;

public class OnFailureConnectionTest : MonoBehaviour
{
    void Start()
    {
        var networkManager = NetworkManager.Singleton;
        networkManager.OnTransportFailure += TransportFailureTest;
    }

    private void TransportFailureTest()
    {
        Debug.Log("On Transport Failure");
    }
}
