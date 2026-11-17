using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2f;
    private Transform playerBody;
    private float xRotation;

    void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation = Mathf.Clamp(xRotation - mouseY, -85f, 85f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}