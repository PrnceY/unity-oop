using System;
using UnityEngine;

public class IntelDocument : MonoBehaviour, IInteractable
{
    public string Prompt => "Press F to collect intel";
    public Transform extractionTarget;
    private bool collected;

    public void Interact(GameObject interactor)
    {
        if (collected) return;
        collected = true;
        ObjectiveManager.Instance?.CompleteObjective("collect_intel");
        Debug.Log("Intel collected!");
        gameObject.SetActive(false);

        FindAnyObjectByType<DirectionIndicator>()?.SetTarget(extractionTarget);
    }
}

