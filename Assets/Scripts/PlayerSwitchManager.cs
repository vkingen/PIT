using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchManager : MonoBehaviour
{
    public bool isSpaceshipController = false;

    [Header("Objects")]
    public Transform emptySpaceShip;
    [SerializeField] private Transform spaceShipController;
    [SerializeField] private Transform playerController;
    [SerializeField] private GameObject isMovingText;

    private float IsMovingTimer = 2f;
    private bool IsMoving;

    //[SerializeField] private LandingGear landingGearUI;

    public LandingPad activeLandingPad;
    public Transform lastKnownLandingPadPosition;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            EnterTheSpaceship();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            LandTheSpaceship();
        }

        if (IsMoving)
        {
            HandleIsMovingTimer();
        }
    }



    [SerializeField] private bool hasLanded = false;



    public bool canLand = false;
    private float timerDuration;

    public void LandTheSpaceship()
    {
        if(spaceShipController.gameObject.GetComponent<PlaneController>().rigidbody.velocity.magnitude <= 0.1)
        {
            if (activeLandingPad != null && canLand)
            {
                canLand = false;

                StartCoroutine(ExitSpaceshipDelay());
            }
        }
        else
        {
            IsMoving = true;
            HandleIsMovingTimer();
        }
    }

    
    private void HandleIsMovingTimer()
    {
        isMovingText.SetActive(true);
        
        IsMovingTimer -= Time.deltaTime;
        if (IsMovingTimer < 0)
        {
            isMovingText.SetActive(false);
            float heartbeatTimerMax = 2f;
            IsMovingTimer = heartbeatTimerMax;
            IsMoving = false;
        }
    }

    

    public void EnterTheSpaceship()
    {
        canLand = true;

        StartCoroutine(EnterSpaceshipDelay());
    }

    IEnumerator ExitSpaceshipDelay()
    {
        spaceShipController.gameObject.GetComponent<PlayerFadeTransition>().FadeOut();

        float timer = 0;
        timerDuration = spaceShipController.gameObject.GetComponent<PlayerFadeTransition>().fadeDuration;
        while (timer < timerDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        spaceShipController.gameObject.SetActive(false);
        spaceShipController.position = activeLandingPad.correctLandingOrientation.position;
        spaceShipController.rotation = activeLandingPad.correctLandingOrientation.rotation;

        playerController.gameObject.SetActive(true);
        playerController.position = activeLandingPad.teleportPlayerToTransform.position;
        playerController.rotation = activeLandingPad.teleportPlayerToTransform.rotation;

        playerController.gameObject.GetComponent<PlayerFadeTransition>().FadeIn();

        emptySpaceShip = activeLandingPad.emptySpaceShip;
        emptySpaceShip.gameObject.SetActive(true);

        //activeLandingPad.emptySpaceShip.gameObject.SetActive(true);
    }

    IEnumerator EnterSpaceshipDelay()
    {
        
        playerController.gameObject.GetComponent<PlayerFadeTransition>().FadeOut();

        float timer = 0;
        timerDuration = playerController.gameObject.GetComponent<PlayerFadeTransition>().fadeDuration;
        while (timer < timerDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        emptySpaceShip.gameObject.SetActive(false);
        playerController.gameObject.SetActive(false); 

        spaceShipController.gameObject.SetActive(true);

       

        spaceShipController.gameObject.GetComponent<PlayerFadeTransition>().FadeIn();


    }

}
