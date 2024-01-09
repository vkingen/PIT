using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private GameObject blackScreenObj;
    [SerializeField] private GameObject player;
    public void StartDelay()
    {
        StartCoroutine(DisableWithDelay());
    }


    IEnumerator DisableWithDelay()
    {
        blackScreenObj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        player.SetActive(false);
        yield return new WaitForSeconds(4f);
        player.SetActive(true);
        blackScreenObj.SetActive(false);
    }
}
