using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public int numberOfObjectsToSpawn;
    public Vector2 asteroidSizeRange;
    public GameObject[] asteroidPrefabs; // The asteroid prefab you want to spawn
    public float spawnRadius;
    public float minDistanceFromCenter;

    void Start()
    {
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            float randomSize = Random.Range(asteroidSizeRange.x, asteroidSizeRange.y);

            // Get random rotation values
            float randomRotationX = Random.Range(0f, 360f);
            float randomRotationY = Random.Range(0f, 360f);
            float randomRotationZ = Random.Range(0f, 360f);
            Quaternion randomRotation = Quaternion.Euler(randomRotationX, randomRotationY, randomRotationZ);

            int rand = Random.Range(0, asteroidPrefabs.Length);
            GameObject asteroid = Instantiate(asteroidPrefabs[rand], transform.position + spawnPosition, randomRotation);
            asteroid.transform.parent = transform;
            if(rand != 0 || rand != 1)
                asteroid.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

            if (CheckCollision(asteroid))
            {
                Destroy(asteroid);
                i--;
            }
        }
    }

    bool CheckCollision(GameObject newAsteroid)
    {
        Collider[] colliders = Physics.OverlapSphere(newAsteroid.transform.position, newAsteroid.transform.localScale.x / 2);

        // Check if the new asteroid overlaps with any other colliders
        foreach (Collider collider in colliders)
        {
            if (collider != newAsteroid.GetComponent<Collider>())
            {
                return true;
            }
        }

        return false;
    }

    Vector3 GetRandomPosition()
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = Random.insideUnitSphere * spawnRadius;
        } while (spawnPosition.magnitude < minDistanceFromCenter);

        return transform.position + spawnPosition;
    }
}
