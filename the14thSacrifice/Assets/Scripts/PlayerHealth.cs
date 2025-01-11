using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar health;

    private void Start()
    {
        currentHealth = maxHealth;
        health.SetHP(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        health.SetHP(currentHealth);
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Add player death logic (e.g., respawn or game over)
    }
}
