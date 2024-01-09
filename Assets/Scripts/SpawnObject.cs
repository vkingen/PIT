using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;

    public void SpawnTheObject()
    {
        GameObject clone = Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
