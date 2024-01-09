using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceToSpaceship : MonoBehaviour
{
    [SerializeField] private Transform spaceship;
    [SerializeField] private float distance;
    [SerializeField] private float distanceToTrigger;

    [SerializeField] private LandingGear landingGear;

    [SerializeField] private TMP_Text UIText;

    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, spaceship.position);
        if(distance <= distanceToTrigger)
        {
            landingGear.SwitchButtons(true);
        }
        else
        {
            landingGear.SwitchButtons(false);
            UIText.text = "DISTANCE TO SPACESHIP: " + distance.ToString("F0") + "M";
        }
    }
}
