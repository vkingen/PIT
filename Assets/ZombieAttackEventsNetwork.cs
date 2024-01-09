using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombieAttackEventsNetwork : NetworkBehaviour
{
    [SerializeField] private Transform attackZone;

    public void EnableZombieAttack()
    {
        EnableZombieAttackServerRpc();
    }

    public void DisableZombieAttack()
    {
        DisableZombieAttackServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void EnableZombieAttackServerRpc()
    {
        attackZone.gameObject.SetActive(true);
    }

    [ServerRpc(RequireOwnership = false)]
    public void DisableZombieAttackServerRpc()
    {
        attackZone.gameObject.SetActive(false);
    }
}
