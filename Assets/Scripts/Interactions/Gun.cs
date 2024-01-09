using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using static WeaponsSystem;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [SerializeField] private ParticleSystem MuzzleFlash;
    

    [Header("Gun tip transforms")]
    [SerializeField] private Transform[] rocketSpawnPoints;
    [SerializeField] private Transform minigunSpawnPoint;
    [SerializeField] private Transform plasmaSpawnPoint;


    [Header("Gun Scriptable Objects")]
    public WeaponTypes weaponTypes;
    public WeaponTypes[] weaponsToChooseFrom;


    private float LastShootTime;
    private bool rocketToggle;
    private Transform bulletSpawnPointPosition;
    private Collider targetCollider;

    [Header("Audio")]
    [SerializeField] private AudioSource reloadAudio;
    private AudioSource shootingAudioSource;
    private AudioSource minigunAudioSource;
    private AudioSource plasmaAudioSource;
    private AudioSource[] rocketLauncherAudioSource = new AudioSource[2];
    

    [Header("Inputs")]
    public InputActionProperty shipShootingInput;

    [Header("UI")]
    public Slider reloadingSlider;
    public TMP_Text ammoText;
    public GameObject headTrackingUICrosshair;


    // For reloading
    [SerializeField] private float timeToWait = 2.0f; // Adjust this as needed
    private float currentReloadTime = 0.0f;
    private bool isReloading = false;


    [Header("Turret Control")]
    public Transform headTransform;     // Assign the "Head" object in the Inspector
    public Transform turretHeadTransform; // Assign the "Turret head" object in the Inspector
    //public float maxRotationX = 45f;     // Maximum X rotation for the "Turret head"

    private void Start()
    {
        headTrackingUICrosshair.SetActive(false);
        plasmaAudioSource = plasmaSpawnPoint.GetComponent<AudioSource>();
        minigunAudioSource = minigunSpawnPoint.GetComponent<AudioSource>();
        rocketLauncherAudioSource[0] = rocketSpawnPoints[0].GetComponent<AudioSource>();
        rocketLauncherAudioSource[1] = rocketSpawnPoints[1].GetComponent<AudioSource>();

        reloadingSlider.value = 0;
        reloadingSlider.gameObject.SetActive(false);


        foreach (var item in weaponsToChooseFrom)
        {
            item.totalAmountOfAmmoRemaining = item.maxAmmo;
            item.ammo = item.magazineSize;
        }
        
        UpdateUI();
    }

    private void Update()
    {
        HandleInput();

        if(weaponTypes.name == "plasma")
        {
            if(!headTrackingUICrosshair.activeSelf)
            {
                headTrackingUICrosshair.SetActive(true);
            }
            TurretControl();
        }
        else
        {
            if (headTrackingUICrosshair.activeSelf)
            {
                headTrackingUICrosshair.SetActive(false);
            }
        }
        

    }

    private void PickCorrectMuzzleFlash()
    {
        MuzzleFlash = bulletSpawnPointPosition.GetComponentInChildren<ParticleSystem>();
    }

    private void TurretControl()
    {
        Vector3 currentRotation = turretHeadTransform.localRotation.eulerAngles;

        float headRotatioY = headTransform.localRotation.eulerAngles.y;
        float headRotationX;

        if (headTransform.localRotation.x <= 0.25f && headTransform.localRotation.x >= -0.60f)
            headRotationX = headTransform.localRotation.eulerAngles.x;
        else
            headRotationX = currentRotation.x;
        
        
        Vector3 newRotation = new Vector3(headRotationX, headRotatioY, currentRotation.z);
        turretHeadTransform.localRotation = Quaternion.Euler(newRotation);
    }

    public void UpdateUI()
    {
        ammoText.text = "AMMO: " + weaponTypes.ammo  + " / " + weaponTypes.totalAmountOfAmmoRemaining;
    }

    public void HandleInput()
    {
        // VR controller input
        if (shipShootingInput.action.ReadValue<float>() > 0)
        {
            Shoot();
        }

        // For debugging
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        // For debugging
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadWeapon();
        }
    }
    public void ReloadWeapon()
    {
        StartCoroutine(ReloadDelay());
    }

    private IEnumerator ReloadDelay()
    {
        reloadingSlider.gameObject.SetActive(true);
        isReloading = true;
        currentReloadTime = 0;
        reloadAudio.clip = weaponTypes.reloadAudio;
       
        timeToWait = weaponTypes.reloadAudio.length;
        reloadAudio.Play();


        while (currentReloadTime < timeToWait)
        {
            currentReloadTime += Time.deltaTime;
            reloadingSlider.value = currentReloadTime / timeToWait;
            yield return null;
        }

        
        reloadingSlider.value = 1;
        isReloading = false;


        int emptySpaceInMag = weaponTypes.magazineSize - weaponTypes.ammo;
        Debug.Log(emptySpaceInMag);
        if (emptySpaceInMag >= weaponTypes.totalAmountOfAmmoRemaining)
        {
            weaponTypes.ammo = weaponTypes.totalAmountOfAmmoRemaining;
            weaponTypes.totalAmountOfAmmoRemaining = 0;
        }
        else
        {
            weaponTypes.totalAmountOfAmmoRemaining -= emptySpaceInMag;
            weaponTypes.ammo += emptySpaceInMag;
        }




        //if (weaponTypes.maxAmmo >= weaponTypes.magazineSize)
        //{

        //    weaponTypes.maxAmmo -= restAmmoInMag;
        //    weaponTypes.ammo = weaponTypes.magazineSize;
        //}



        UpdateUI();
        reloadingSlider.gameObject.SetActive(false);
    }


    public void Shoot()
    {
        if (weaponTypes.ammo <= 0 || isReloading) return;

        if (LastShootTime + weaponTypes.ShootDelay < Time.time)
        {
            // Change amount of ammo
            weaponTypes.ammo--;
            UpdateUI();

            if (weaponTypes.name == "minigun")
            {
                bulletSpawnPointPosition = minigunSpawnPoint;
                shootingAudioSource = minigunAudioSource;
            }
            else if(weaponTypes.name == "plasma")
            {
                bulletSpawnPointPosition = plasmaSpawnPoint;
                shootingAudioSource = plasmaAudioSource;
            }
            else if (weaponTypes.name == "missiles")
            {
                rocketToggle = !rocketToggle;
                if (rocketToggle)
                {
                    bulletSpawnPointPosition = rocketSpawnPoints[0];
                    shootingAudioSource = rocketLauncherAudioSource[0];
                }
                else
                {
                    bulletSpawnPointPosition = rocketSpawnPoints[1];
                    shootingAudioSource = rocketLauncherAudioSource[1];
                }
            }

            PickCorrectMuzzleFlash();
            MuzzleFlash.Play();
            Vector3 direction = GetDirection();

            // Play shooting audio
            shootingAudioSource.clip = weaponTypes.shootingAudio;
            shootingAudioSource.Play();

            if (Physics.Raycast(bulletSpawnPointPosition.position, direction, out RaycastHit hit, float.MaxValue, weaponTypes.Mask))
            {
                if(hit.transform.GetComponent<Collider>())
                {
                    targetCollider = hit.transform.GetComponent<Collider>();
                }
                TrailRenderer trail = Instantiate(weaponTypes.BulletTrail, bulletSpawnPointPosition.position, bulletSpawnPointPosition.rotation);

                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                LastShootTime = Time.time;
            }
            else
            {
                TrailRenderer trail = Instantiate(weaponTypes.BulletTrail, bulletSpawnPointPosition.position, bulletSpawnPointPosition.rotation);

                StartCoroutine(SpawnTrail(trail, bulletSpawnPointPosition.position + GetDirection() * 500, Vector3.zero, false));

                LastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = bulletSpawnPointPosition.forward;

        if (weaponTypes.AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-weaponTypes.BulletSpreadVariance.x, weaponTypes.BulletSpreadVariance.x),
                Random.Range(-weaponTypes.BulletSpreadVariance.y, weaponTypes.BulletSpreadVariance.y),
                Random.Range(-weaponTypes.BulletSpreadVariance.z, weaponTypes.BulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= weaponTypes.BulletSpeed * Time.deltaTime;

            yield return null;
        }
        
        Trail.transform.position = HitPoint;
        
        if (MadeImpact)
        {
            Instantiate(weaponTypes.ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));
            
            // Destroy Trail
            Destroy(Trail.gameObject);

            if(targetCollider != null)
            {
                if (targetCollider.bounds.Contains(HitPoint))
                {
                    if (targetCollider.GetComponent<Health>())
                        targetCollider.GetComponent<Health>().TakeDamage(weaponTypes.damage);

                }
            }
        }

        if(Trail != null)
        {
            Destroy(Trail.gameObject, Trail.time);
        }
    }
    public void SelectWeapon(WeaponTypes newWeapon)
    {
        weaponTypes = newWeapon;
        PickCorrectMuzzleFlash();
        UpdateUI();
    }

    public void SelectWeapon(string newWeapon)
    {
        foreach (var item in weaponsToChooseFrom)
        {
            if(item.name == newWeapon)
            {
                SelectWeapon(item);
            }
        }
    }

}
