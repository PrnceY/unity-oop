using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    public float Current { get; private set; }
    public event Action<float> OnDamaged;
    public event Action OnDeath;
    public AudioClip hurtSound;
    private AudioSource audioSource;

    void Awake()
    {
        Current = maxHealth;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void TakeDamage(float amount)
    {
        if (Current <= 0) return;
        Current = Mathf.Max(0, Current - amount);
        if (hurtSound) audioSource.PlayOneShot(hurtSound);
        OnDamaged?.Invoke(amount);
        if (Current <= 0) OnDeath?.Invoke();
    }
}