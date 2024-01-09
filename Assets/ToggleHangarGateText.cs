using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleHangarGateText : MonoBehaviour
{
    [SerializeField] private TMP_Text[] text;
    private bool toggle = false;

    public void UpdateText()
    {
        toggle = !toggle;
        foreach (TMP_Text item in text)
        {
            if(toggle)
            {
                item.text = "CLOSE GATE";
            }
            else
            {
                item.text = "OPEN GATE";
            }
        }
    }
}
