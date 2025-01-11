using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar health;

    public bool dead = false;

    public AudioSource ScreamSF;

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
        ScreamSF.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        Debug.Log("Player died!");
        SceneManager.LoadScene(3);
        // Add player death logic (e.g., respawn or game over)
    }
}
