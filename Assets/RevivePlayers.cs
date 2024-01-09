using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RevivePlayers : NetworkBehaviour
{

    [ServerRpc(RequireOwnership = false)]
    public void ReviveServerRpc()
    {
        PlayerHealthNetwork[] playerHealths = FindObjectsOfType<PlayerHealthNetwork>();
        foreach (var item in playerHealths)
        {
            item.enabled = true;
            item.ReviveServerRpc();
        }
    }
}
