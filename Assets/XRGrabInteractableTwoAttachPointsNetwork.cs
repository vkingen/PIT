using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttachPointsNetwork : XRGrabInteractable
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;
    private NetworkRevolver revolver;

    protected override void Awake()
    {
        revolver = GetComponent<NetworkRevolver>();
        base.Awake();
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            revolver.ChangeHandType(false);
            attachTransform = leftAttachTransform;
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            revolver.ChangeHandType(true);
            attachTransform = rightAttachTransform;
        }

        //revolver.IsGrabbingState(true);
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        //revolver.IsGrabbingState(false);
        base.OnSelectExited(args);
    }
}
