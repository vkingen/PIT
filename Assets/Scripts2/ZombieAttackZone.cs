using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackZone : MonoBehaviour
{
    [SerializeField] private string attackTag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(attackTag))
        {
            Health health = other.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(10);
            }

            PlayerHealthNetwork networkHealth = other.GetComponent<PlayerHealthNetwork>();
            if (networkHealth != null)
            {
                networkHealth.TakeDamage(10);
            }

        }
    }
}
