using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BackpackGrabInteractable : XRGrabInteractable
{
    //[SerializeField] private BackpackSocketInteractor backpackInventorySlot;
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnHoverExited(interactor);
    }
}
