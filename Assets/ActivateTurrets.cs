using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTurrets : MonoBehaviour
{
    [SerializeField] private EnemyTurret[] enemyTurret;

    
    private void OnEnable()
    {
        foreach (var item in enemyTurret)
        {
            item.GetPlayerReference(true);
        }
    }
    private void OnDisable()
    {
        foreach (var item in enemyTurret)
        {
            item.GetPlayerReference(false);
        }
    }
}
