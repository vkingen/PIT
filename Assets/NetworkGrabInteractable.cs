using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkGrabInteractable : NetworkBehaviour
{
    private NetworkObject networkObject;  

    //private void Awake()
    //{
    //    networkObject = GetComponent<NetworkObject>();
    //}

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        networkObject = GetComponent<NetworkObject>();
    }
    public void RequestOwnership()
    {
        RequestOwnershipServerRpc(NetworkManager.Singleton.LocalClient.ClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestOwnershipServerRpc(ulong clientID)
    {
        networkObject.ChangeOwnership(clientID);
    }
}
