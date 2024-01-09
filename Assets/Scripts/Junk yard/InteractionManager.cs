using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Transform objectSpawnPoint;

    [SerializeField] private GameObject[] objectsToSpawn;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SpawnObject("Box");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnObject("Ball");
        }
    }
   
    public void SpawnObject(string objectName)
    {
        foreach (var item in objectsToSpawn)
        {
            if(item.name == objectName)
            {
                GameObject objectClone = Instantiate(item, objectSpawnPoint.position, objectSpawnPoint.rotation);
            }
        }
    }


    //public void DestroyObject()
    //{

    //}

    public void GrabObject()
    {

    }

    public void DropObject()
    {

    }

    public void ThrowObject()
    {

    }

    public void RotateObject()
    {

    }
}
