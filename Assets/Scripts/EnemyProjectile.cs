using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public ParticleSystem explosionParticleSystem;
    [SerializeField] private int damageToGive = 100; 

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if(health != null)
        {
            health.TakeDamage(damageToGive);
        }

        Instantiate(explosionParticleSystem, collision.contacts[0].point, Quaternion.identity);
        Destroy(gameObject);
    }
}
