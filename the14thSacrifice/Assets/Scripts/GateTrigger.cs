using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public Transform player;
    public Transform GateL;
    public Transform GateR;

    public Transform PivotL;
    public Transform PivotR;

    public float openSpeed = 30f;
    public float maxOpenAngle = 80f;

    private bool isPlayerInside = false;
    private float currentAngleL = 0f;
    private float currentAngleR = 0f;

    private Quaternion initialRotationL;
    private Quaternion initialRotationR;

    void Start()
    {
        initialRotationL = PivotL.localRotation;
        initialRotationR = PivotR.localRotation;
    }

    void Update()
    {
        if (isPlayerInside)
        {
            // increase the rotation up to max angle
            currentAngleL = Mathf.MoveTowards(currentAngleL, -maxOpenAngle, openSpeed * Time.deltaTime);
            currentAngleR = Mathf.MoveTowards(currentAngleR, maxOpenAngle, openSpeed * Time.deltaTime);
        }
        else
        {
            // decrease the rotation to 0
            currentAngleL = Mathf.MoveTowards(currentAngleL, 0f, openSpeed * Time.deltaTime);
            currentAngleR = Mathf.MoveTowards(currentAngleR, 0f, openSpeed * Time.deltaTime);
        }

        // Apply the rotations to the gates while preserving their initial rotation
        PivotL.localRotation = initialRotationL * Quaternion.Euler(0, currentAngleL, 0);
        PivotR.localRotation = initialRotationR * Quaternion.Euler(0, currentAngleR, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            //Debug.Log("Player entered trigger");
            isPlayerInside = true; // Start opening gates
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            //Debug.Log("Player exited trigger");
            isPlayerInside = false; // Start closing gates
        }
    }
}
