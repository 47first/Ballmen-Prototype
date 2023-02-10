using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerApproveTest : MonoBehaviour
{
    void Start()
    {
        if (NetworkManager.Singleton.IsServer == false)
            NetworkManager.Singleton.ConnectionApprovalCallback = ApproveTest;
    }

    private void ApproveTest(NetworkManager.ConnectionApprovalRequest arg1, NetworkManager.ConnectionApprovalResponse arg2)
    {
        Debug.Log("Approve Test");
    }
}
