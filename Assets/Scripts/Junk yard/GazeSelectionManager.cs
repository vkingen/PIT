using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GazeSelectionManager : MonoBehaviour
{
    XRSimpleInteractable interactable;



    public Text debugText;

    public void AssignInteractable(XRSimpleInteractable item)
    {
        interactable = item;

        // 

        debugText.text += "\n" + "Assigning: " + interactable.name.ToString();
    }
    public void UnassignInteractable()
    {
        debugText.text += "\n" + "Unassigning: " +  interactable.name.ToString();
        interactable = null;
    }
}
