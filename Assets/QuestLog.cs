using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestLog : MonoBehaviour
{
    //public bool[] questItemsFound;
    public GameObject[] checkMarkers;
    public UnityEvent whenAllQuestItemsHasBeenCollected;

    public void RemoveQuestItemFromList(int itemIndexToRemove)
    {
        for (int i = 0; i < checkMarkers.Length; i++)
        {
            if(i == itemIndexToRemove)
            {
                checkMarkers[i].SetActive(true);
            }
        }
        CheckQuestState();
    }

    private void CheckQuestState()
    {
        int questItems = checkMarkers.Length;
        foreach (GameObject item in checkMarkers)
        {
            if(item.activeInHierarchy)
            {
                questItems--;
            }
        }
        if(questItems == 0)
        {
            whenAllQuestItemsHasBeenCollected.Invoke();
        }
    }
}
