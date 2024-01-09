using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySlot : XRSocketInteractor
{
    [SerializeField] private bool isRobotPartInventory;
    RobotPartProgressManager robotPartProgressManager;

    protected override void Awake()
    {
        base.Awake();
        robotPartProgressManager = FindObjectOfType<RobotPartProgressManager>();
    }
    protected override void OnSelectEntering(XRBaseInteractable interactable)
    {
        base.OnSelectEntering(interactable);

        if(isRobotPartInventory)
        {
            interactableHoverMeshMaterial = null;
            if(interactable.transform.GetComponent<XRGrabInteractable>() != null)
            {
                interactable.transform.parent = this.transform;
                interactable.GetComponent<BoxCollider>().enabled = false;
                robotPartProgressManager.robotPartsToDestroy.Add(interactable.gameObject);
                //Destroy(interactable.transform.GetComponent<XRGrabInteractable>());
                //Destroy(interactable.transform.GetComponent<Rigidbody>());
            }
        }
    }
}
