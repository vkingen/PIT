using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabAndFollowHead : MonoBehaviour
{
    private XRGrabInteractable interactable;
    private bool isGazeSelected = false;
    XRBaseInteractor interactorReference;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.onSelectEntered.AddListener(OnSelectEnter);
        interactable.onSelectExited.AddListener(OnSelectExit);
    }

    private void OnSelectEnter(XRBaseInteractor interactor)
    {
        interactorReference = interactor;
        isGazeSelected = true;
        interactable.transform.SetParent(interactor.transform, false);
    }

    private void OnSelectExit(XRBaseInteractor interactor)
    {
        interactorReference = interactor;
        isGazeSelected = false;
        interactable.transform.SetParent(null);
    }

    private void Update()
    {
        if (isGazeSelected)
        {
            // Update the position and rotation of the object as needed to follow the gaze.
            // You can use the interactor's position and rotation, and optionally apply an offset.
            // For example:
            interactable.transform.position = interactorReference.transform.position;
            interactable.transform.rotation = interactorReference.transform.rotation;
        }
    }
}
