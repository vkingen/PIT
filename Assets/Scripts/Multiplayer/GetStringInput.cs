using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetStringInput : MonoBehaviour
{
    public TMP_InputField inputField;
    //public TMP_Text debugText;
    public TestRelay relay;

    public void CheckInput()
    {
        if (inputField.text.Length == 6)
        {
            //Debug.Log(inputField.text.Length);
            //debugText.text = inputField.text;
            relay.JoinRelay(inputField.text);
        }
        else
        {
            //debugText.text = inputField.text;
            inputField.text = "";
        }
        
    }
}
