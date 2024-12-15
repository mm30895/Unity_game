using UnityEngine;

public class CameraShakeOnMovement : MonoBehaviour
{
    public float shakeThreshold = 0.1f; // Minimum movement speed to trigger shake
    public float shakeDuration = 0.2f; // Duration of the shake effect
    public float shakeMagnitude = 0.05f; // Magnitude of the shake effect
    public float frequency = 20f; // Frequency of the sine wave oscillation

    private Vector3 initialPosition; // The original position of the camera
    private float currentShakeDuration; // Tracks the remaining duration of the shake
    private Vector3 lastPosition; // Tracks the object's last position
    private float time; // Time accumulator for smooth oscillation

    void Start()
    {
        // Save the initial position of the camera
        initialPosition = transform.localPosition;

        // Cache the Rigidbody, if attached

        // Initialize last position
        if (transform.parent != null)
        {
            lastPosition = transform.parent.position;
        }
        else
        {
            lastPosition = transform.position;
        }
    }

    void Update()
    {
        // Calculate the movement speed
        float speed = 0;

        // Otherwise, calculate speed manually
        Vector3 currentPosition = transform.parent != null ? transform.parent.position : transform.position;
        speed = Vector3.Distance(lastPosition, currentPosition) / Time.deltaTime;
        lastPosition = currentPosition;

        // Trigger shake if speed exceeds the threshold
        if (speed > shakeThreshold && currentShakeDuration <= 0)
        {
            currentShakeDuration = shakeDuration;
            time = 0f; // Reset time accumulator for smooth oscillation
        }

        // Apply smooth shake effect if duration is greater than zero
        if (currentShakeDuration > 0)
        {
            // Smooth oscillation using sine wave
            time += Time.deltaTime * frequency;
            float xShake = Mathf.Sin(time) * shakeMagnitude;
            float yShake = Mathf.Sin(time * 1.2f) * shakeMagnitude; // Slightly different frequency for Y

            // Apply the offset to the camera's position
            transform.localPosition = initialPosition + new Vector3(xShake, yShake, 0);

            // Reduce the shake duration over time
            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            // Reset camera position when shake ends
            transform.localPosition = initialPosition;
            currentShakeDuration = 0;
        }
    }
}
