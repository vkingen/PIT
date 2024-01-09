using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class BackpackSocketInteractor : XRSocketInteractor
{
    //protected override void OnSelectEntering(XRBaseInteractable interactable)
    //{
    //    base.OnSelectEntering(interactable);
    //}

    //protected override void OnSelectExiting(XRBaseInteractable interactable)
    //{
    //    base.OnSelectExiting(interactable);
    //}

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        interactable.transform.parent = this.transform;
        //interactable.transform.GetChild(0).gameObject.SetActive(false);

        base.OnSelectEntered(interactable);
    }
    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        interactable.transform.parent = null;
        //interactable.transform.GetChild(0).gameObject.SetActive(true);

        base.OnSelectExited(interactable);
    }
}
