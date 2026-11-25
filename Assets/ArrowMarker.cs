using UnityEngine;

public class ArrowMarker : MonoBehaviour
{
    public Transform target;
    public float bobSpeed = 2f;
    public float bobHeight = 0.3f;
    private Vector3 startPos;

    void Start() { startPos = transform.position; }

    void Update()
    {
        // Point toward target
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            if (dir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(dir);
        }

        // Bob up and down
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }
}