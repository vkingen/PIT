using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemConsumeSocket : XRSocketInteractor
{
    public QuestLog questLog;
    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        if (interactable.name == "Robotic_Part_head")
        {
            questLog.RemoveQuestItemFromList(0);
        }
        if (interactable.name == "Robotic_Part_Torso")
        {
            questLog.RemoveQuestItemFromList(1);
        }
        if (interactable.name == "Robotic_Part_LeftLeg")
        {
            questLog.RemoveQuestItemFromList(2);
        }
        if (interactable.name == "Robotic_Part_RightLeg")
        {
            questLog.RemoveQuestItemFromList(3);
        }
        if (interactable.name == "Robotic_Part_LeftArm")
        {
            questLog.RemoveQuestItemFromList(4);
        }
        if (interactable.name == "Robotic_Part_RightArm")
        {
            questLog.RemoveQuestItemFromList(5);
        }

        Destroy(interactable.gameObject);
        return;
    }
}
