using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCheatSheet : MonoBehaviour
{
    public GameObject CheatSheet;

    private void Start()
    {
        Hide();
    }
    public void Show()
    {
        CheatSheet.SetActive(true);
    }

    public void Hide()
    {
        CheatSheet.SetActive(false);
    }
}
