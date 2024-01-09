using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpTypes
    {
        Health,
        Ammo
    }
    [SerializeField] private int amountToGive;
    [SerializeField] private WeaponTypes[] weaponTypes;
    [SerializeField] private GameObject audioFX;


    public PowerUpTypes powerUpType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch (powerUpType)
            {
                case PowerUpTypes.Health:
                    other.GetComponent<Health>().GiveHealth(amountToGive);
                    break;
                case PowerUpTypes.Ammo:
                    foreach (var item in weaponTypes)
                    {
                        item.totalAmountOfAmmoRemaining += item.magazineSize * 2;
                    }
                    Gun gun = FindObjectOfType<Gun>();
                    if(gun != null)
                    {
                        gun.UpdateUI();
                    }
                    break;
               
            }
            GameObject fx = Instantiate(audioFX, transform.position, transform.rotation);
            Destroy(fx, 3f);
            Destroy(this.gameObject);
        }
    }
}
