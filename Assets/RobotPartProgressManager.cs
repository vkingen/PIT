using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotPartProgressManager : MonoBehaviour
{
    public static RobotPartProgressManager Instance;
    private int robotPartsCollected = 0;
    public UnityEvent whenAllPartsCollected;
    public List<GameObject> robotPartsToDestroy = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

    }

    public void UpdateParts()
    {
        robotPartsCollected++;
        if(robotPartsCollected == 6)
        {
            
            foreach(var part in robotPartsToDestroy)
            {
                Destroy(part);
            }
            whenAllPartsCollected.Invoke();
        }
    }
}
