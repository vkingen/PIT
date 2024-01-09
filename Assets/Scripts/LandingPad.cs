using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LandingPad : MonoBehaviour
{

    public Transform teleportPlayerToTransform;
    public Transform emptySpaceShip;
    public Transform correctLandingOrientation;
    [SerializeField] private LandingGear landingGearUI;
    [SerializeField] private bool isHangarLandingPad; 

    PlayerSwitchManager playerSwitchManager;


    private void Awake()
    {
        playerSwitchManager = FindObjectOfType<PlayerSwitchManager>();
        if (isHangarLandingPad)
        {
            playerSwitchManager.activeLandingPad = this;
            playerSwitchManager.emptySpaceShip = emptySpaceShip;
            playerSwitchManager.lastKnownLandingPadPosition = this.transform;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSwitchManager.activeLandingPad = this;
            playerSwitchManager.lastKnownLandingPadPosition = this.transform;
            playerSwitchManager.canLand = true;
            landingGearUI.SwitchButtons(true);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSwitchManager.activeLandingPad = null;
            playerSwitchManager.canLand = false;
            landingGearUI.SwitchButtons(false);
        }
    }




    //[SerializeField] private Transform teleportPlayerToTransform;
    //[SerializeField] private Transform emptySpaceShip;
    //[SerializeField] private Transform spaceShipController;
    //[SerializeField] private Transform playerController;
    //[SerializeField] private LandingGear landingGearUI;

    //[SerializeField] private Transform correctLandingOrientation;

    //[SerializeField] private bool hasLanded = false;
    //[SerializeField] private bool canLand = false;

    //PlayerSwitchManager playerSwitchManager;


    //private float timerDuration;

    //private void Start()
    //{
    //    emptySpaceShip.gameObject.SetActive(false);
    //}
    //IEnumerator ExitSpaceshipDelay()
    //{
    //    spaceShipController.gameObject.GetComponent<PlayerFadeTransition>().FadeOut();

    //    float timer = 0;
    //    timerDuration = spaceShipController.gameObject.GetComponent<PlayerFadeTransition>().fadeDuration;
    //    while (timer < timerDuration)
    //    {
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }

    //    spaceShipController.gameObject.SetActive(false);
    //    spaceShipController.position = correctLandingOrientation.position;
    //    spaceShipController.rotation = correctLandingOrientation.rotation;

    //    playerController.gameObject.SetActive(true);
    //    playerController.position = teleportPlayerToTransform.position;
    //    playerController.rotation = teleportPlayerToTransform.rotation;

    //    playerController.gameObject.GetComponent<PlayerFadeTransition>().FadeIn();

    //    emptySpaceShip.gameObject.SetActive(true);
    //}

    //IEnumerator EnterSpaceshipDelay()
    //{
    //    emptySpaceShip.gameObject.SetActive(false);
    //    playerController.gameObject.GetComponent<PlayerFadeTransition>().FadeOut();

    //    float timer = 0;
    //    timerDuration = playerController.gameObject.GetComponent<PlayerFadeTransition>().fadeDuration;
    //    while (timer < timerDuration)
    //    {
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }

    //    spaceShipController.gameObject.SetActive(true);

    //    playerController.gameObject.SetActive(false);

    //    spaceShipController.gameObject.GetComponent<PlayerFadeTransition>().FadeIn();


    //}


    //public void LandTheSpaceship()
    //{
    //    canLand = false;

    //    StartCoroutine(ExitSpaceshipDelay());
    //}

    //public void EnterTheSpaceship()
    //{
    //    canLand = true;

    //    StartCoroutine(EnterSpaceshipDelay());
    //}

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.H) && canLand)
    //    {
    //        LandTheSpaceship();
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && !hasLanded)
    //    {
    //        canLand = true;
    //        landingGearUI.SwitchButtons(true);

    //    }
    //    else
    //    {
    //        canLand = false;
    //        landingGearUI.SwitchButtons(false);
    //    }

    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        canLand = false;
    //        landingGearUI.SwitchButtons(false);

    //    }
    //}


    ////private void OnTriggerStay(Collider other)
    ////{
    ////    if (other.CompareTag("Player") && !hasLanded)
    ////    {
    ////        if(!canLand)
    ////            canLand = true;


    ////    }
    ////}
}
