using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] private Transform bodyPartToRotate, barrelPartToRotate, gunTip;
    [SerializeField] private Transform player;
    [SerializeField] private float targetingRadius;
    [SerializeField] private float shootingRadius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rotationSpeed = 5.0f;

    [SerializeField] private float shootDelayTime = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private float projectileSpeed;

    private bool canShoot = true;

    private bool spaceshipPlayerIsActive = false;

   

    public void GetPlayerReference(bool setSpaceshipPlayerStatus)
    {
        spaceshipPlayerIsActive = setSpaceshipPlayerStatus;
        if(setSpaceshipPlayerStatus)
            player = FindObjectOfType<PlaneController>().transform;
    }

    private void Update()
    {
        if (!spaceshipPlayerIsActive) return;

        if (Vector3.Distance(transform.position, player.position) <= targetingRadius)
        {
            RaycastHit hit;

            if (Physics.Raycast(gunTip.position, player.position - gunTip.position, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform == player)
                {
                    // Calculate rotation angles
                    Vector3 targetDirection = player.position - bodyPartToRotate.position;
                    Vector3 horizontalDirection = new Vector3(targetDirection.x, 0, targetDirection.z);

                    // Calculate rotation for bodyPartToRotate (y-axis)
                    Quaternion bodyRotation = Quaternion.LookRotation(horizontalDirection);
                    bodyPartToRotate.rotation = Quaternion.Slerp(bodyPartToRotate.rotation, bodyRotation, Time.deltaTime * rotationSpeed);

                    // Calculate rotation for barrelPartToRotate (x-axis)
                    float angleX = Vector3.Angle(Vector3.up, targetDirection);
                    Quaternion barrelRotation = Quaternion.Euler(new Vector3(angleX, 0, 0));
                    barrelPartToRotate.localRotation = Quaternion.Slerp(barrelPartToRotate.localRotation, barrelRotation, Time.deltaTime * rotationSpeed);
                    
                    if (Vector3.Distance(transform.position, player.position) <= shootingRadius && canShoot)
                    {
                        //Debug.Log(canShoot);
                        StartCoroutine(Shoot());
                    }
                }
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        // Create and shoot a projectile
        GameObject newProjectile = Instantiate(projectilePrefab, gunTip.position, gunTip.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, gunTip.position, Quaternion.identity);
        Destroy(muzzleFlash, 3f);
        rb.velocity = gunTip.forward * projectileSpeed;

        //Debug.Log(newProjectile.name);
        yield return new WaitForSeconds(shootDelayTime);

        canShoot = true;
    }
}
