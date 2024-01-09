using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomObjectSpawner : MonoBehaviour
{
    [Tooltip("0 = Dont destroy after spawned")]
    [SerializeField] private float destroySpawnedObjectAfterSeconds;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Vector3 addForceDirection;
    [SerializeField] private float addForceMultiplier;


    private void Awake()
    {
        if(spawnPoint == null)
        {
            spawnPoint = transform.GetChild(0).transform;
        }
    }

    public void SpawnObject()
    {
        GameObject clone = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        if(destroySpawnedObjectAfterSeconds > 0)
        {
            Destroy(clone, destroySpawnedObjectAfterSeconds);
        }

        Rigidbody rigidbody= clone.GetComponent<Rigidbody>();
        if(rigidbody != null )
        {
            rigidbody.AddForce(addForceDirection * addForceMultiplier);
        }
    }
}
