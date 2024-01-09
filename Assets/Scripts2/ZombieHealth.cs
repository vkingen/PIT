using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZombieHealth : MonoBehaviour
{
    public bool zombieKillDebugger;
    public int health = 100;
    //[SerializeField] private Image healthbar;
    [SerializeField] private bool isPlayer;

    

    //[SerializeField] private GameObject UICanvas;


    [Header("If the object is static object")]
    [SerializeField] private bool isStaticObject;
    public UnityEvent whenDestroyed;


    private Animator animator;
    private NavMeshAgent agent;
    private new Collider collider;


    private void Update()
    {
        if(zombieKillDebugger)
        {
            zombieKillDebugger = false;
            DealDamage(9999);
        }
    }



    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if (!isPlayer)
            animator.SetTrigger("GetHit");
        //if (healthbar != null)
        //{
        //    healthbar.fillAmount = health / 100f;
        //}
        if (health <= 0)
        {
            Die();
        }
    }

    public void AddForce()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * 1000);
    }

    public void Die()
    {
        
        whenDestroyed.Invoke();


        //if (isStaticObject)
        //{
        //    whenDestroyed.Invoke();
        //    return;
        //}
        //else if (!isPlayer)
        //{
        //    //Destroy(gameObject);
        //    Destroy(collider);
        //    //Destroy(UICanvas);
        //    if(agent != null)
        //    {
        //        agent.speed = 0;
        //        Destroy(agent);
        //    }
            
        //    animator.enabled = false;
        //    Destroy(this.gameObject, 10f); // Destroy this with some effect. Maybe dissolve the body. 
        //    Destroy(this);
        //}
        //else
        //{
        //    whenDestroyed.Invoke();
        //    // Player dead
        //    //SceneManager.LoadScene("AAA_MainMenu"); // ONLY TEMPORARY. MAYBE: -Fade to black before, - Go to seperate transition scene.. etc.
        //}
        
    }
}
