using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationClearedStatus : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemies;
    [SerializeField] private GameObject beaconToEnable;
    [SerializeField] private GameObject beaconToDisable;
    private bool isCleared = false;

    public void CheckEnemiesStatus()
    {
        if (isCleared) return;

        int totalAmountOfEnemies = enemies.Length;
        foreach (EnemyController item in enemies)
        {
            if (item.isDead)
            {
                totalAmountOfEnemies--;
            }
        }
        if(totalAmountOfEnemies <= 0)
        {
            beaconToEnable.SetActive(true);
            beaconToDisable.SetActive(false);
            isCleared = true;
        }
    }
}
