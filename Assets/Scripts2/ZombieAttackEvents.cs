using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackEvents : MonoBehaviour
{
    [SerializeField] private Transform attackZone;

    public void EnableZombieAttack()
    {
        attackZone.gameObject.SetActive(true);
    }

    public void DisableZombieAttack()
    {
        attackZone.gameObject.SetActive(false);
    }
}
