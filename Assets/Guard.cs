using UnityEngine;

public class Guard : Enemy
{
    public Transform[] patrolPoints;
    private int patrolIndex;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    private float nextAttackTime;

    protected override void HandleState()
    {
        switch (state)
        {
            case EnemyState.Patrol: DoPatrol(); break;
            case EnemyState.Chase:  DoChase();  break;
            case EnemyState.Attack: DoAttack(); break;
        }
    }

    void DoPatrol()
    {
        if (patrolPoints.Length == 0) return;
        Transform target = patrolPoints[patrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }

    void DoChase()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        DetectionManager.Instance?.AddSuspicion(15f * Time.deltaTime);
    }

    void DoAttack()
    {
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + attackCooldown;
        player.GetComponent<Health>()?.TakeDamage(attackDamage);
        Debug.Log("Guard attacks player!");
    }
}