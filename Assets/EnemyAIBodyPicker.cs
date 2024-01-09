using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBodyPicker : MonoBehaviour
{

    [SerializeField] private GameObject[] bodies;


    private void Awake()
    {
        //foreach (GameObject item in bodies)
        //{
        //    item.SetActive(false);
        //}
        bodies[Random.Range(0, bodies.Length)].SetActive(true);
    }
}
