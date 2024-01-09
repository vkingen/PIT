using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] private bool teleportationIsOn = true;
    private bool showingTeleportRay = false;

    private ActionBasedContinuousMoveProvider actionBasedContinuousMoveProvider;
    private float moveSpeed;

    public GameObject leftTeleportation;
    //public GameObject rightTeleportation;

    public InputActionProperty leftActivate;
    //public InputActionProperty rightActivate;

    //public TMP_Text debugText;
    [SerializeField] private TMP_Text locomotionTypeText;

    private float teleportRayRemoveCounterMax = 0.05f;
    private float teleportRayRemoveCounter;

    private void Awake()
    {
        actionBasedContinuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        if (PlayerPrefs.GetInt("MovementType") == 0)
        {
            teleportationIsOn = true;
        }
        else if (PlayerPrefs.GetInt("MovementType") == 1)
        {
            teleportationIsOn = false;
        }
        //Debug.Log(PlayerPrefs.GetInt("MovementType"));
        moveSpeed = actionBasedContinuousMoveProvider.moveSpeed;
        ShowLocomotionTypeText();

    }

    private void Start()
    {
        //if(PlayerPrefs.GetInt("MovementType") != null)
        //{
            
        //}

        
    }

    public void ToggleLocomotionType()
    {
        teleportationIsOn = !teleportationIsOn;

        if (teleportationIsOn)
        {
            PlayerPrefs.SetInt("MovementType", 0);
        }
        else
        {
            PlayerPrefs.SetInt("MovementType", 1);
        }

        

        ShowLocomotionTypeText();
    }

    private void ShowLocomotionTypeText()
    {
        if(teleportationIsOn)
        {
            locomotionTypeText.text = "TELEPORT";
            actionBasedContinuousMoveProvider.moveSpeed = 0f;
        }
        else
        {
            locomotionTypeText.text = "WALK";
            actionBasedContinuousMoveProvider.moveSpeed = moveSpeed;
        }
    }

    private void Update()
    {
        //debugText.text = leftActivate.action.ReadValue<Vector2>().ToString();

        //Debug.Log(leftActivate.action.ReadValue<Vector2>().ToString());

        if (teleportationIsOn)
        {
            showingTeleportRay = leftActivate.action.ReadValue<Vector2>().x != 0f || leftActivate.action.ReadValue<Vector2>().y != 0f;
            if (showingTeleportRay)
            {
                leftTeleportation.SetActive(true);
                teleportRayRemoveCounter = teleportRayRemoveCounterMax;
            }
            else
            {
                teleportRayRemoveCounter -= Time.deltaTime;
                if(teleportRayRemoveCounter <= 0)
                {
                    leftTeleportation.SetActive(false);
                }
            }
            
            //rightTeleportation.SetActive(rightActivate.action.ReadValue<float>() > 0.1f);
        }
        else
        {
            leftTeleportation.SetActive(false);
            //rightTeleportation.SetActive(false);
        }
    }
}
