using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TMP_Text fpsText;
    private float deltaTime = 0.0f;

    private void Update()
    {
        // Calculate FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // Update the text object
        if (fpsText != null)
        {
            fpsText.text = "FPS: " + Mathf.Round(fps);
        }
    }
}
