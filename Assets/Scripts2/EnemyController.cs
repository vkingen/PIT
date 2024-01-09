using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    // Hidden variables
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public FieldOfView fov;
    [HideInInspector] public Transform target;
    public bool isNetworkZombie;

    AudioSource enemyAudio;

    [HideInInspector] public bool isDead = false; 


    private void Update()
    {
        if(agent.speed > 0)
        {
            if(!enemyAudio.isPlaying)
            {
                enemyAudio.Play();
            }
        }
        else
        {
            enemyAudio.Stop();
        }
    }

    [Header("Movement Setup")]
    public float chasePlayerSpeed;

    [HideInInspector] public Vector3 startingPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        enemyAudio = GetComponent<AudioSource>();

        startingPosition = transform.position;
    }

    public void DeadState()
    {
        isDead = true;
        chasePlayerSpeed = 0;
        agent.speed = 0;

    }

    //public void EnableZombieAttack()
    //{
    //    attackZone.gameObject.SetActive(true);
    //}

    //public void DisableZombieAttack()
    //{
    //    attackZone.gameObject.SetActive(false);
    //}
}
