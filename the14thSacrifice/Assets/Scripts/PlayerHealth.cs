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


        // Add player death logic (e.g., respawn or game over)
        Invoke("LostScene", 3);
    }
    private void LostScene() {
        SceneManager.LoadScene(4);
    }
    public void DieByMinion() {
        Invoke("LoadOtherScene", 3);
    }
    private void LoadOtherScene()
    {
        SceneManager.LoadScene(5);
    }
}
