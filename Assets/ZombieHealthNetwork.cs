using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class ZombieHealthNetwork : NetworkBehaviour
{
    [SerializeField] private GameObject attackZone;
    public int health = 100;
    

    [SerializeField] private EnemyController enemyController;
    CapsuleCollider capsuleCollider;


    private void Start()
    {
        if(enemyController != null)
        {
            enemyController = GetComponent<EnemyController>();
        }
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void DealDamage(int damage)
    {
        DealDamageServerRpc(damage);
    }

    [ServerRpc(RequireOwnership = false)]
    public void DealDamageServerRpc(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();

            ZombieWaveSystem zombieWaveSystem = FindObjectOfType<ZombieWaveSystem>();
            if (zombieWaveSystem != null)
            {
                zombieWaveSystem.RemoveZombieServerRpc();
            }
        }
    }

    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(5f);
        if(IsServer || IsHost)
        {
            DestroyEnemyServerRpc();
        }
    }

    [ServerRpc]
    public void DestroyEnemyServerRpc()
    {
        NetworkObject netObject = GetComponent<NetworkObject>();
        netObject.Despawn();
    }

    public void Die()
    {
        attackZone.SetActive(false);
        StartCoroutine(DestroyWithDelay());
        enemyController.DeadState();
        capsuleCollider.enabled = false;
    }

    [ServerRpc(RequireOwnership = false)]
    public void DieServerRpc()
    {
        enemyController.DeadState();
        capsuleCollider.enabled = false;
    }
}
