using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSencitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!PlayerMovement.dialogue)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            float mouseX = Input.GetAxis("Mouse X") * mouseSencitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSencitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
}
