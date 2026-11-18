using UnityEngine;

public class ExtractionZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ObjectiveManager.Instance?.CompleteObjective("reach_extraction");
    }
}