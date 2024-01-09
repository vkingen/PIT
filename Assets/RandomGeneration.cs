using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] listOfGameObjectsToSpawn;

    private void Start()
    {
        GameObject gameObjectToSpawn = listOfGameObjectsToSpawn[Random.Range(0, listOfGameObjectsToSpawn.Length)];
        Instantiate(gameObjectToSpawn ,transform.position, transform.rotation, transform);
        //gameObjectToSpawn.gameObject.transform.parent = this.gameObject.transform;
        Destroy(this);
    }
}
