using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{
    [SerializeField] private string[] otherTags;
    [SerializeField] private bool destroyOtherObjectOnTrigger = false;
    public UnityEvent onTriggerEnteredEvent;
    public UnityEvent onTriggerExitedEvent;

    


    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in otherTags)
        {
            if (other.CompareTag(item))
            {
                onTriggerEnteredEvent.Invoke();
                if (destroyOtherObjectOnTrigger)
                    Destroy(other.gameObject);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        foreach (var item in otherTags)
        {
            if (other.CompareTag(item))
            {
                onTriggerExitedEvent.Invoke();
            }
        }
    }
}
