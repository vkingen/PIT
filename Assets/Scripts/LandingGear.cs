using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingGear : MonoBehaviour
{
    public GameObject OnButton, OffButton;

    private void Start()
    {
        SwitchButtons(false);
    }

    public void SwitchButtons(bool state)
    {
        if (state)
        {
            OnButton.SetActive(true);
            OffButton.SetActive(false);
        }
        else
        {
            OnButton.SetActive(false);
            OffButton.SetActive(true);
        }
    }
}
