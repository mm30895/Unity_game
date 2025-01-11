using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    public bool isDead = false;
    public HealthBar healthBar;
    public GameObject Bar;
    public WinScreenManager winScreenManager;


    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBar.SetHP(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        healthBar.SetHP(currentHealth);
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true); 
        Debug.Log("Enemy died!");
        Bar.SetActive(false);
        healthBar.SetHP(maxHealth);
        
        Destroy(gameObject.GetComponent<Collider>()); 
    winScreenManager.show();
    }
}
