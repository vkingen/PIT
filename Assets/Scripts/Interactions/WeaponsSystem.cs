using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponsSystem : MonoBehaviour
{
    public enum Weapons
    {
        Minigun,
        MissileLauncher,

    }
    public InputActionProperty shipShootingInput;
    public Transform gunTransform; // The transform of the gun/barrel
    public GameObject bulletPrefab; // The bullet prefab
    public float shootInterval = 1.0f; // Time between shots
    public int magazineSize = 10; // Maximum number of shots before reloading
    public float reloadTime = 2.0f; // Time it takes to reload

    private float lastShotTime;
    private int currentAmmo;

    public Weapons selectedWeapon = Weapons.Minigun; // The initially selected weapon


    void Start()
    {
        // Initialize with the selected weapon
        SelectWeapon(selectedWeapon);
        currentAmmo = magazineSize;
    }
    private void Update()
    {
        if (shipShootingInput.action.ReadValue<float>() > 1 && Time.time - lastShotTime >= shootInterval)
        {
            if (currentAmmo > 0)
            {
                HandleShooting();
            }
        }

        if (Input.GetButton("Fire1") && Time.time - lastShotTime >= shootInterval)
        {
            if (currentAmmo > 0)
            {
                HandleShooting();
            }
        }
    }
    private void ReloadWeapon()
    {
        StartCoroutine(Reload());
    }
    private void HandleShooting()
    {
        lastShotTime = Time.time;
        currentAmmo--;

        // Create a bullet at the gun's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 100f;
        // Add code here to set the bullet's velocity or behavior.

        // Destroy the bullet after a certain time (e.g., if it doesn't hit anything)
        Destroy(bullet, 5.0f);
        //debugText.text = shipShootingInput.action.ReadValue<float>().ToString();
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
    }
    //void Update()
    //{
    //    // Check for input to change the selected weapon
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        SelectWeapon(Weapons.Minigun);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        SelectWeapon(Weapons.MissileLauncher);
    //    }
    //    // Add more cases for other weapons as needed
    //}

    void SelectWeapon(Weapons newWeapon)
    {
        // Set the selected weapon to the new weapon
        selectedWeapon = newWeapon;
        Debug.Log(selectedWeapon);
    }
}
