using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkRevolver : NetworkBehaviour
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
    [SerializeField] private GameObject localMuzzleFlash;
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
        if (isRightHand)
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
        //if(!IsOwner) { return; }
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
        else if (leftHandShootingInput.action.ReadValue<float>() < 0.5f)
        {
            hasShotTriggerLeft = false;
        }
    }

    private void Shoot()
    {
        shootAudio.pitch = Random.Range(audioPitchRange.x, audioPitchRange.y);
        shootAudio.Play();
        GameObject muzzleFlashClone = Instantiate(localMuzzleFlash, gunTip.position, gunTip.rotation);
        Destroy(muzzleFlashClone, 2f);
        //GameObject muzzleFlashClone = Instantiate(localMuzzleFlash, gunTip.position, gunTip.rotation);
        //Destroy(muzzleFlashClone, 3f);

        //if (!IsLocalPlayer)
        //{
        //    MuzzleFlashServerRpc();
        //}
        //MuzzleFlashServerRpc();
        ShootServerRpc();
        //Shoot2();
    }


    //[ClientRpc]
    //public void MuzzleFlashClientRpc()
    //{
    //    if(!IsOwner)
    //    {
    //        //GameObject muzzleFlashClone = Instantiate(muzzleFlash, gunTip.position, gunTip.rotation);
    //        //NetworkObject networkObject = muzzleFlashClone.GetComponent<NetworkObject>();
    //        //networkObject.Spawn();
    //    }
        
    //}

    //[ServerRpc/*(RequireOwnership = false)*/]
    //public void MuzzleFlashServerRpc()
    //{
    //    GameObject muzzleFlashClone = Instantiate(muzzleFlash, gunTip.position, gunTip.rotation);
    //    NetworkObject networkObject = muzzleFlashClone.GetComponent<NetworkObject>();
    //    networkObject.Spawn();
    //    //MuzzleFlashClientRpc();
    //}

    [ServerRpc(RequireOwnership = false)]
    private void ShootServerRpc()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, 200, layers))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<ZombieHealthNetwork>().DealDamage(damage);
            }

            GameObject impactClone = Instantiate(impact, hit.point, Quaternion.identity);
            NetworkObject networkObject2 = impactClone.GetComponent<NetworkObject>();
            networkObject2.Spawn();

        }
    }

    //private void Shoot2()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, 200, layers))
    //    {
    //        if (hit.transform.CompareTag("Enemy"))
    //        {
    //            hit.transform.GetComponent<ZombieHealthNetwork>().DealDamage(damage);
    //        }

    //        //if(IsOwner)
    //        //{
    //        //    GameObject impactClone = Instantiate(impact, hit.point, Quaternion.identity);
    //        //    NetworkObject networkObject2 = impactClone.GetComponent<NetworkObject>();
    //        //    networkObject2.Spawn();
    //        //}

    //        GameObject impactClone = Instantiate(impact, hit.point, Quaternion.identity);
    //        NetworkObject networkObject2 = impactClone.GetComponent<NetworkObject>();
    //        networkObject2.Spawn();
    //    }
    //}
}
