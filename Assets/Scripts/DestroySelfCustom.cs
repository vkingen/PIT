using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfCustom : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
    public void DestroySelf()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
