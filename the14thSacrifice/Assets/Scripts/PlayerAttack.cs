using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform boringSword;
    public float attackDuration = 0.2f; // Duration for first attack
    public Vector3 attackRotation = new Vector3(-70f, 0f, 90f); // Rotation for first attack
    public Vector3 attackPositionOffset = new Vector3(0f, -0.2f, 0.2f); // Position offset for first attack

    public float attackDuration2 = 0.4f; // Duration for second attack
    public Vector3 attackInitialMovement = new Vector3(-0.2f, 0f, 0f); // Initial movement for backhand
    public Vector3 attackRotation2 = new Vector3(-70f, 0f, -90f); // Rotation for second attack
    public Vector3 attackPositionOffset2 = new Vector3(0.2f, -0.2f, 0.2f); // Position offset for second attack

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Save initial position and rotation
        initialPosition = boringSword.localPosition;
        initialRotation = boringSword.localRotation;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(SwordAttack());
        }
    }

    private System.Collections.IEnumerator SwordAttack()
    {
        // Decide randomly the type of attack
        bool attackRight = Random.value > 0.5f;

        if (attackRight)
        {
            // Perform the first type of attack (slash)
            yield return StartCoroutine(PerformAttack(attackDuration, attackRotation, attackPositionOffset));
        }
        else
        {
            // Perform the second type of attack (backhand)
            yield return StartCoroutine(PerformBackhandAttack());
        }
    }

    private System.Collections.IEnumerator PerformAttack(float duration, Vector3 rotation, Vector3 positionOffset)
    {
        // Animate to attack position and rotation
        float elapsedTime = 0f;
        Vector3 targetPosition = initialPosition + positionOffset;
        Quaternion targetRotation = Quaternion.Euler(rotation);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // Lerp position and rotation
            boringSword.localPosition = Vector3.Lerp(initialPosition, targetPosition, progress);
            boringSword.localRotation = Quaternion.Slerp(initialRotation, targetRotation, progress);

            yield return null;
        }

        // Return to initial position and rotation
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            boringSword.localPosition = Vector3.Lerp(targetPosition, initialPosition, progress);
            boringSword.localRotation = Quaternion.Slerp(targetRotation, initialRotation, progress);

            yield return null;
        }
    }

    private System.Collections.IEnumerator PerformBackhandAttack()
    {
        // Phase 1: Initial movement to the left
        float elapsedTime = 0f;
        Vector3 intermediatePosition = initialPosition + attackInitialMovement;
        Quaternion intermediateRotation = initialRotation; // No rotation during the initial movement

        while (elapsedTime < attackDuration2 / 2f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (attackDuration2 / 2f);

            // Lerp to the intermediate position
            boringSword.localPosition = Vector3.Lerp(initialPosition, intermediatePosition, progress);
            boringSword.localRotation = Quaternion.Slerp(initialRotation, intermediateRotation, progress);

            yield return null;
        }

        // Phase 2: Perform the backhand slash
        Vector3 targetPosition = intermediatePosition + attackPositionOffset2;
        Quaternion targetRotation = Quaternion.Euler(attackRotation2);

        elapsedTime = 0f;
        while (elapsedTime < attackDuration2 / 2f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (attackDuration2 / 2f);

            // Lerp to the final position and rotation
            boringSword.localPosition = Vector3.Lerp(intermediatePosition, targetPosition, progress);
            boringSword.localRotation = Quaternion.Slerp(intermediateRotation, targetRotation, progress);

            yield return null;
        }

        // Return to initial position and rotation
        elapsedTime = 0f;
        while (elapsedTime < attackDuration2 / 2f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (attackDuration2 / 2f);

            boringSword.localPosition = Vector3.Lerp(targetPosition, initialPosition, progress);
            boringSword.localRotation = Quaternion.Slerp(targetRotation, initialRotation, progress);

            yield return null;
        }
    }
}
