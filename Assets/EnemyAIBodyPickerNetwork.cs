using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyAIBodyPickerNetwork : NetworkBehaviour
{
    [SerializeField] private GameObject[] bodies;


    private void Awake()
    {
        if(IsHost || IsServer)
        {
            SelectBodyServerRpc();
        }
    }

    [ServerRpc]
    public void SelectBodyServerRpc()
    {
        bodies[Random.Range(0, bodies.Length)].SetActive(true);
    }
}
