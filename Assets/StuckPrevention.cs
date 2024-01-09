using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckPrevention : MonoBehaviour
{
    [SerializeField] private PlaneController planeController;
    [SerializeField] private PlayerSwitchManager playerSwitchManager;
    //private bool isStuck = false;
    //private bool hasTriggeredWarning = false;
    //private bool hasTriggeredRemovedButton = false;
    //[SerializeField] private GameObject stuckPreventionButton;

    //private float timer = 5.0f; // Initial timer value

    //private void FixedUpdate()
    //{
    //    if(planeController.rigidbody.velocity.magnitude <= 0.5f && planeController.throttleValue >= 1 && planeController.isColliding)
    //    {
    //        //isStuck = true;

    //        //if(!hasTriggeredWarning)
    //        //{
    //        //    // Activate a debug UI button to get unstuck
    //        //    stuckPreventionButton.SetActive(true);
    //        //    hasTriggeredWarning = true;
    //        //}

    //        if (timer > 0)
    //        {
    //            timer -= Time.fixedDeltaTime; // Decrease the timer by the fixed time step
    //        }
    //        else
    //        {
    //            timer = 0; // Ensure the timer doesn't go below 0
    //                       // Timer has reached 0, you can add your actions here
    //            Debug.Log("Timer reached 0!");
    //            ResetPosition();
                
    //        }

    //    }
    //    else
    //    {
    //       // stuckPreventionButton.SetActive(false);
    //        //if (!hasTriggeredRemovedButton)
    //        //{
    //        //    stuckPreventionButton.SetActive(false);
    //        //    hasTriggeredRemovedButton = true;
    //        //}
            
    //        Debug.Log("Vel: "+planeController.rigidbody.velocity.magnitude + " + Gas: " + planeController.throttleValue);
    //    }
    //    //if(planeController.rigidbody.velocity.magnitude > 5 && isStuck)
    //    //{
    //    //    isStuck = false;
    //    //}
    //}

    public void ResetPosition()
    {
        planeController.gameObject.SetActive(false);
        // Set throttle value to 0
        planeController.throttleValue = 0;
        // set velocity to 0
        planeController.rigidbody.velocity = Vector3.zero;
        // Set position of planecontroller to last active landing pad
        planeController.gameObject.transform.position = playerSwitchManager.lastKnownLandingPadPosition.position;
        planeController.gameObject.transform.rotation = playerSwitchManager.lastKnownLandingPadPosition.rotation;
        planeController.gameObject.SetActive(true);
        //hasTriggeredWarning = false;
        //isStuck = false;
    }
}
