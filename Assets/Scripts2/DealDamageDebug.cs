using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageDebug : MonoBehaviour
{
    [SerializeField] private ZombieHealth[] healths;
    [SerializeField] private Rigidbody[] rb;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("DAMAGE");
            foreach(ZombieHealth health in healths)
            {
                health.DealDamage(10);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (Rigidbody r in rb)
            {
                r.AddForce(transform.up * 1000f);
            }
        }
    }
}
