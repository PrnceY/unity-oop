using System;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }
    public event Action<string> OnObjectiveCompleted;
    public event Action OnAllCompleted;

    private string[] objectives = { "collect_intel", "reach_extraction" };
    private bool[] completed;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        completed = new bool[objectives.Length];
    }

    public void CompleteObjective(string id)
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i] == id && !completed[i])
            {
                completed[i] = true;
                Debug.Log($"Objective complete: {id}");
                OnObjectiveCompleted?.Invoke(id);
                CheckAllDone();
            }
        }
    }

    void CheckAllDone()
    {
        foreach (bool b in completed) if (!b) return;
        Debug.Log("All objectives complete! Reach extraction.");
        OnAllCompleted?.Invoke();
    }
}