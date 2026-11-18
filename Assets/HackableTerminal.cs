using UnityEngine;

public class HackableTerminal : MonoBehaviour, IInteractable
{
    public string Prompt => "Press F to hack terminal";
    private bool hacked;

    public void Interact(GameObject interactor)
    {
        if (hacked) return;
        hacked = true;
        DetectionManager.Instance?.AddSuspicion(-30f);
        Debug.Log("Terminal hacked! Suspicion reduced.");
    }
}