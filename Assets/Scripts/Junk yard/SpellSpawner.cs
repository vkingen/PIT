using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    public GameObject[] spells;
    private GameObject spell;


    private bool hasSpawnedSpell = false;
    private void Start()
    {
        spell = spells[0];
        spell.SetActive(false);
    }

    public void SpawnSpell()
    {
        if(!hasSpawnedSpell)
        {
            // With enable
            //spell = spells[0];
            //spell.SetActive(true);

            // With instantiate
            GameObject clone = Instantiate(spells[0], transform.position, transform.rotation);
            clone.SetActive(true);
            clone.GetComponent<Rigidbody>().AddForce(Vector3.up * 500f);
            //clone.transform.SetParent(this.transform);
        }
    }
}
