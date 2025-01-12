using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 50f;  // The speed of rotation in degrees per second

    void Update()
    {
        // Rotate the object around its own Y-axis (local Y-axis)
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
