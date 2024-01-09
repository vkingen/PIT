using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class ZombieWaveSystem : NetworkBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] zombiePrefab;

    private int zombieWaveIndex = 0;
    [SerializeField] private int zombiesPerWave = 4;
    private int amountOfZombiesToSpawn;
    private bool hasStartedWave = false;

    private int zombiesLeft;
    [SerializeField] private TMP_Text amountOfZombiesLeft;
    [SerializeField] private TMP_Text stateStatus;

    AudioSource audioSource;

    private float spawnDelay = 1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    [ServerRpc(RequireOwnership = false)]
    public void RemoveZombieServerRpc()
    {
        RemoveZombieClientRpc();
    }

    [ClientRpc]
    public void RemoveZombieClientRpc()
    {
        zombiesLeft--;
        UpdateZombiesLeftTextClientRpc();
    }

    [ClientRpc]
    private void UpdateZombiesLeftTextClientRpc()
    {
        
        amountOfZombiesLeft.text = zombiesLeft.ToString();
    }

    [ServerRpc(RequireOwnership = false)]
    public void StartWaveServerRpc()
    {
        StartWaveClientRpc();
    }

    [ClientRpc]
    public void StartWaveClientRpc()
    {
        
        if (!hasStartedWave)
        {
            zombieWaveIndex++;
            stateStatus.text = "WAVE " + zombieWaveIndex + " STARTED!";
            hasStartedWave = true;

            if(audioSource != null)
            {
                audioSource.Play();
            }
            
            //amountOfZombiesToSpawn = zombiesPerWave * zombieWaveIndex;
            amountOfZombiesToSpawn = Mathf.RoundToInt(zombiesPerWave * Mathf.Pow(1.5f, zombieWaveIndex));

            StartCoroutine(SpawnWithDelay());
        }
    }

    public void StartWave()
    {
        StartWaveServerRpc();
    }


    IEnumerator SpawnWithDelay()
    {
        if (IsHost || IsServer)
        {
            GameObject zombieClone = Instantiate(zombiePrefab[Random.Range(0, zombiePrefab.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            NetworkObject networkObject2 = zombieClone.GetComponent<NetworkObject>();
            networkObject2.Spawn();
        }
        
        amountOfZombiesToSpawn--;
        zombiesLeft++;
        UpdateZombiesLeftTextClientRpc();

        float spawnDelayMultiplier;

        if (zombieWaveIndex <= 15)
        {
            spawnDelayMultiplier = zombieWaveIndex * 0.1f;
        }
        else
        {
            spawnDelayMultiplier = 15 * 0.1f;
        }
        

        yield return new WaitForSeconds(spawnDelay - spawnDelayMultiplier);

        if (amountOfZombiesToSpawn > 0)
        {
            StartCoroutine(SpawnWithDelay());
        }
        else
        {
            stateStatus.text = "WAVE " + zombieWaveIndex + " COMPLETED!";
            hasStartedWave = false;
        }
    }
}
