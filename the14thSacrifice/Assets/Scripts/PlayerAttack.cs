using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform boringSword;
    public float attackDuration = 0.2f; // Duration of the attack animation
    public Vector3 attackRotation = new Vector3(-70f, 0f, 90f); // Sword attack rotation
    public Vector3 attackPositionOffset = new Vector3(0f, -0.2f, 0.2f); // Position offset during attack

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
        // Animate to attack position and rotation
        float elapsedTime = 0f;
        Vector3 targetPosition = initialPosition + attackPositionOffset;
        Quaternion targetRotation = Quaternion.Euler(attackRotation);

        while (elapsedTime < attackDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / attackDuration;

            // Lerp position and rotation
            boringSword.localPosition = Vector3.Lerp(initialPosition, targetPosition, progress);
            boringSword.localRotation = Quaternion.Slerp(initialRotation, targetRotation, progress);

            yield return null;
        }

        // Return to initial position and rotation
        elapsedTime = 0f;
        while (elapsedTime < attackDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / attackDuration;

            boringSword.localPosition = Vector3.Lerp(targetPosition, initialPosition, progress);
            boringSword.localRotation = Quaternion.Slerp(targetRotation, initialRotation, progress);

            yield return null;
        }
    }
}
