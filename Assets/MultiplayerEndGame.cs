using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerEndGame : NetworkBehaviour
{
    public List<PlayerHealthNetwork> playerHealthNetworks = new List<PlayerHealthNetwork>();

    public void CheckPlayerDeathStatus()
    {
        if(IsServer || IsHost)
        {
            int amountOfPlayers = playerHealthNetworks.Count;
            //Debug.Log(amountOfPlayers);
            foreach (var item in playerHealthNetworks)
            {
                if (item.isDead)
                {
                    amountOfPlayers--;
                }
            }
            if(amountOfPlayers == 0)
            {
                EndTheGameServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void EndTheGameServerRpc()
    {
        EndTheGameClientRpc();
    }

    [ClientRpc]
    public void EndTheGameClientRpc()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("Multiplayer_SetupScene");
        if (IsServer || IsHost)
        {
            
        }
    }
}
