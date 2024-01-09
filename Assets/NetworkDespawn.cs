using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkDespawn : NetworkBehaviour
{
    NetworkObject networkObject;
    private void Start()
    {
        networkObject = GetComponent<NetworkObject>();
        StartCoroutine(DespawnWithDelay());
    }

    IEnumerator DespawnWithDelay()
    {
        yield return new WaitForSeconds(3f);
        if(IsServer || IsHost)
        {
            networkObject.Despawn();
        }
    }
}
