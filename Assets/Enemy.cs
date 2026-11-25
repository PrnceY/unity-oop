using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Enemy : MonoBehaviour
{
    protected EnemyState state = EnemyState.Patrol;
    protected Transform player;
    protected Health health;

    public float sightRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        player = GameObject.FindWithTag("Player").transform;
    }

    protected virtual void OnEnable() { health.OnDeath += HandleDeath; }
    protected virtual void OnDisable() { health.OnDeath -= HandleDeath; }

    protected virtual void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackRange)    ChangeState(EnemyState.Attack);
        else                        ChangeState(EnemyState.Chase);

        HandleState();
    }

    protected void ChangeState(EnemyState next)
    {
        if (state == next) return;
        state = next;
        Debug.Log($"{name} → {state}");
    }

    protected abstract void HandleState();
    protected virtual void HandleDeath() { Destroy(gameObject); }
}