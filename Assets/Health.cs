using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    public float Current { get; private set; }
    public event Action<float> OnDamaged;
    public event Action OnDeath;

    void Awake() { Current = maxHealth; }

    public void TakeDamage(float amount)
    {
        if (Current <= 0) return;
        Current = Mathf.Max(0, Current - amount);
        OnDamaged?.Invoke(amount);
        if (Current <= 0) OnDeath?.Invoke();
    }
}