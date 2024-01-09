using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealthNetwork : NetworkBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private TMP_Text healthText;
    MultiplayerEndGame endGame;

    [SerializeField] private GameObject projectileTarget;

    public UnityEvent onDeathEvent;
    public UnityEvent onReviveEvent;

    public bool isDead = false;

    [SerializeField] private Material deadMaterial;
    [SerializeField] private GameObject head, leftHand, rightHand;
    private Material startingMaterialHead, startingMaterialHands;

 

    [ServerRpc]
    public void AddPlayerToEndGameServerRpc()
    {
        endGame = FindObjectOfType<MultiplayerEndGame>();
        endGame.playerHealthNetworks.Add(this);
        //AddPlayerToEndGameClientRpc();
    }

    [ClientRpc]
    public void AddPlayerToEndGameClientRpc()
    {
        
    }

    IEnumerator Del()
    {
        yield return new WaitForSeconds(4f);
        AddPlayerToEndGameServerRpc();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateUIServerRpc();
        startingMaterialHead = head.GetComponent<MeshRenderer>().material;
        startingMaterialHands = leftHand.GetComponent<SkinnedMeshRenderer>().material;

        StartCoroutine(Del());
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateUIServerRpc()
    {
        UpdateUIClientRpc();
    }


    [ClientRpc]
    public void UpdateUIClientRpc()
    {
        healthText.text = $"Health: {currentHealth}hp";
    }

    public void TakeDamage(int damage)
    {
        TakeDamageServerRpc(damage);
    }

    [ServerRpc(RequireOwnership = false)]
    public void TakeDamageServerRpc(int damage)
    {
        TakeDamageClientRpc(damage);
    }

    [ClientRpc]
    public void TakeDamageClientRpc(int damage)
    {
        currentHealth -= damage;
        UpdateUIServerRpc();
        if (currentHealth <= 0)
        {
            isDead = true;

            onDeathEvent.Invoke();


            //SceneManager.LoadScene("MainMenu");
            ShowDeadStateServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ShowDeadStateServerRpc()
    {
        ShowDeadStateClientRpc();
    }

    [ClientRpc]
    public void ShowDeadStateClientRpc()
    {
        //MultiplayerEndGame endGame = FindObjectOfType<MultiplayerEndGame>();
        //endGame.EndTheGame();
        healthText.text = "Health: 0hp";
        projectileTarget.SetActive(false);
        head.GetComponent<MeshRenderer>().material = deadMaterial;
        leftHand.GetComponent<SkinnedMeshRenderer>().material = deadMaterial;
        rightHand.GetComponent<SkinnedMeshRenderer>().material = deadMaterial;
        this.enabled = false;

        if(IsServer || IsHost)
        {
            endGame.CheckPlayerDeathStatus();
        }
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void ReviveServerRpc()
    {
        if(currentHealth <= 0)
        {
            ReviveClientRpc();
        }
    }

    [ClientRpc]
    public void ReviveClientRpc()
    {
        currentHealth = maxHealth;
        projectileTarget.SetActive(true);
        UpdateUIServerRpc();
        head.GetComponent<MeshRenderer>().material = startingMaterialHead;
        leftHand.GetComponent<SkinnedMeshRenderer>().material = startingMaterialHands;
        rightHand.GetComponent<SkinnedMeshRenderer>().material = startingMaterialHands;
        onReviveEvent.Invoke();
    }
}
