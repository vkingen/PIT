using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using Meta.WitAi.Requests;
using UnityEngine.InputSystem;
using TMPro;

public class ActivateVoice : MonoBehaviour
{
    [SerializeField] private Wit wit;

    [Header("Inputs")]
    public InputActionProperty voiceActivationButton;

    [Header("Loading text")]
    [SerializeField] private GameObject listeningObject;


    private void Start()
    {
        listeningObject.SetActive(false);
    }

    private void Update()
    {
        if(wit == null) wit = GetComponent<Wit>();


        if(Input.GetKey(KeyCode.K) || voiceActivationButton.action.ReadValue<float>() > 0)
        {
            wit.Activate();
            listeningObject.SetActive(true);

        }
        else
        {
            listeningObject.SetActive(false);
        }
        
    }
}
