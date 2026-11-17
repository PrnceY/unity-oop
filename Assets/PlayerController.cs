using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
            moveInput = (transform.right * x + transform.forward * z).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void FixedUpdate()
    {
        Vector3 vel = moveInput * moveSpeed;
        vel.y = rb.linearVelocity.y;
        rb.linearVelocity = vel;
    }
}