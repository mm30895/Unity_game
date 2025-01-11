using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    public bool isDead = false;
    public HealthBar healthBar;
    public GameObject Bar;

    public AudioSource BackgroundMusic;
    public AudioSource CombatMusic;
    public AudioSource screamSF;
    public AudioSource deathSF;

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
        screamSF.Play();

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
        StartCoroutine(PlayDeathAudioSequence());
    }

    private IEnumerator PlayDeathAudioSequence()
    {
        // plays one audio after the other
        deathSF.Play();
        yield return new WaitForSeconds(deathSF.clip.length);

        yield return StartCoroutine(FadeOut(CombatMusic, 0.5f));
        yield return StartCoroutine(FadeIn(BackgroundMusic, 0.5f));
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        float targetVolume = 1f;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}
