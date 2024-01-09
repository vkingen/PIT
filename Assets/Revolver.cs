using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Revolver : MonoBehaviour
{
    private bool isGrabbed;
    private bool hasShotTriggerRight = false;
    private bool hasShotTriggerLeft = false;

    [SerializeField] private int damage;

    private AudioSource shootAudio;

    
    


    [Header("Inputs")]
    public InputActionProperty rightHandShootingInput;
    public InputActionProperty leftHandShootingInput;

    public enum HandType
    {
        Right, 
        Left
    }

    public HandType handType;
    [SerializeField] private LayerMask layers;
   

    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Transform gunTip;
    [SerializeField] private Vector2 audioPitchRange;
    [SerializeField] private GameObject impact;


    private void Awake()
    {
        shootAudio = GetComponent<AudioSource>();


       
    }

    public void IsGrabbingState(bool state)
    {
        isGrabbed = state;
    }

    public void ChangeHandType(bool isRightHand)
    {
        if(isRightHand)
        {
            handType = HandType.Right;
        }
        else
        {
            handType = HandType.Left;
        }
    }

    private void Update()
    {
        // VR controller input
        if (rightHandShootingInput.action.ReadValue<float>() > 0 && handType == HandType.Right && isGrabbed && !hasShotTriggerRight)
        {
            hasShotTriggerRight = true;
            Shoot();
        }
        else if (rightHandShootingInput.action.ReadValue<float>() < 0.5f)
        {
            hasShotTriggerRight = false;
        }

        // VR controller input
        if (leftHandShootingInput.action.ReadValue<float>() > 0 && handType == HandType.Left && isGrabbed && !hasShotTriggerLeft)
        {
            hasShotTriggerLeft = true;
            Shoot();
        }
        else if(leftHandShootingInput.action.ReadValue<float>() < 0.5f)
        {
            hasShotTriggerLeft = false;
        }
    }

   

    private void Shoot()
    {
        GameObject muzzleFlashClone = Instantiate(muzzleFlash, gunTip.position, gunTip.rotation);
        Destroy(muzzleFlashClone, 2f);

        shootAudio.pitch = Random.Range(audioPitchRange.x, audioPitchRange.y);
        shootAudio.Play();

        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, 200, layers))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<ZombieHealth>().DealDamage(damage);


            }

            GameObject impactClone = Instantiate(impact, hit.point, Quaternion.identity);
            Destroy(impactClone, 1.5f);
        }
    }
}
