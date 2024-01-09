using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private float currentHealth;

    [Header("Player stuff")]
    [SerializeField] private bool isFlyingPlayer = false;
    [SerializeField] private bool isGroundedPlayer = false;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider healthSlider;


    [SerializeField] private TMP_Text[] textObjectsToChangeColorOnDamage;
    [SerializeField] private Image[] uiObjectsToChangeColorOnDamage;

    [SerializeField] private Color startingTextColor;
    [SerializeField] private Color damageColor;
    //[SerializeField] private Color healthColor;

    public UnityEvent onDeathEvent;



    private void Start()
    {
        currentHealth = maxHealth;
        if (isFlyingPlayer)
        {
            healthSlider.value = currentHealth / 1000f;
            healthText.text = (currentHealth / 10f).ToString("F0") + "%";
        }
    }


    public void GiveHealth(int healthToGive)
    {
        currentHealth += healthToGive;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (isFlyingPlayer)
        {
            healthSlider.value = currentHealth / 1000f;
            healthText.text = (currentHealth / 10f).ToString("F0") + "%";
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            onDeathEvent.Invoke();
            if(!isFlyingPlayer && !isGroundedPlayer)
                Destroy(this.gameObject);
        }
        if(isFlyingPlayer)
        {
            StartCoroutine(DamageColorDelay());
            


            healthSlider.value = currentHealth / 1000f;
            healthText.text = (currentHealth / 10f).ToString("F0") + "%";
        }
    }

 

    private IEnumerator DamageColorDelay()
    {
        foreach (TMP_Text item in textObjectsToChangeColorOnDamage)
        {
            item.color = damageColor;
        }

        foreach (Image item in uiObjectsToChangeColorOnDamage)
        {
            item.color = damageColor;
        }
        
        yield return new WaitForSeconds(0.2f);


        foreach (TMP_Text item in textObjectsToChangeColorOnDamage)
        {
            item.color = startingTextColor;
        }

        foreach (Image item in uiObjectsToChangeColorOnDamage)
        {
            item.color = Color.white;
        }
    }
}
