using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScalingSocketInteractor : XRSocketInteractor
{
    private Vector3 tempScale;
    protected override void OnSelectEntering(XRBaseInteractable interactable)
    {
        
        base.OnSelectEntering(interactable);
        interactable.GetComponent<Collider>().isTrigger = true;
        interactable.transform.GetChild(0).localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    protected override void OnSelectExiting(XRBaseInteractable interactable)
    {
        base.OnSelectExiting(interactable);
        interactable.GetComponent<Collider>().isTrigger = false;
        interactable.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        

        base.OnSelectEntered(interactable);
        interactable.transform.parent = this.transform;
        tempScale = interactable.GetComponent<BoxCollider>().size;
        interactable.GetComponent<BoxCollider>().size -= new Vector3(
            tempScale.x * 0.8f,
            tempScale.y * 0.8f,
            tempScale.z * 0.8f);
    }
    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        interactable.transform.parent = null;

        interactable.GetComponent<BoxCollider>().size = tempScale;



        base.OnSelectExited(interactable);
        
    }
}
