using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public float damage = 10f;
    public float lifetime = 2f;

    void Start() { Destroy(gameObject, lifetime); }

    void Update() { transform.Translate(transform.forward * speed * Time.deltaTime, Space.World); }

    void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponentInParent<IDamageable>();
        target?.TakeDamage(damage);
        Destroy(gameObject);
    }
}