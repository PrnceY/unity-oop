using UnityEngine;

[RequireComponent(typeof(Health))]
public class TargetDummy : MonoBehaviour
{
    private Health health;

    void Awake() { health = GetComponent<Health>(); }

    void OnEnable()
    {
        health.OnDamaged += HandleDamaged;
        health.OnDeath += HandleDeath;
    }

    void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;
        health.OnDeath -= HandleDeath;
    }

    void HandleDamaged(float amount) { Debug.Log($"{name} took {amount} | HP: {health.Current}"); }
    void HandleDeath() { Destroy(gameObject); }
}