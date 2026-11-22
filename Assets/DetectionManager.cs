using System;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    public static DetectionManager Instance { get; private set; }

    [Range(0, 100)] public float suspicion = 0f;
    public float drainRate = 2f;
    public event Action<float> OnSuspicionChanged;
    public event Action OnAlerted;
    public AudioClip alertSound;
    private AudioSource audioSource;

    private bool alerted;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        Application.targetFrameRate = 60;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (alerted) return;
        suspicion = Mathf.Max(0, suspicion - drainRate * Time.deltaTime);
        OnSuspicionChanged?.Invoke(suspicion);
    }

    public void AddSuspicion(float amount)
    {
        if (alerted) return;
        suspicion = Mathf.Min(100, suspicion + amount);
        OnSuspicionChanged?.Invoke(suspicion);
        if (suspicion >= 100) TriggerAlert();
    }

    void TriggerAlert()
    {
        alerted = true;
        Debug.Log("ALERT! You've been fully detected!");
        if (alertSound) audioSource.PlayOneShot(alertSound);
        OnAlerted?.Invoke();
    }
}