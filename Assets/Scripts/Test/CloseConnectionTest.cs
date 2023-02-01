using Unity.Netcode;
using UnityEngine;

public class CloseConnectionTest : MonoBehaviour
{
    public void CloseConnection()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
