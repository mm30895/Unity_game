using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MinotaurAI : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    [Header("References")]
    public Transform player;
    public EnemyHealth enemyHealth;
    public GameObject Bar;
    public CombatZoneTrigger combatZone;

    [Header("Settings")]
    public float visionSpeed = 3f;
    public float rotationSpeed = 3f;
    public float attackRange = 10f;
    public float visionRange = 20f;
    public float attackCooldown = 2f;
    public int attackDamage = 20;
    public int maxHealth = 100;

    private bool isPlayerInAttackRange = false;
    private bool hasPlayerBeenDetected = false;
    private float lastAttackTime = 0;

    public AudioSource BackgroundMusic;
    public AudioSource CombatMusic;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (enemyHealth.isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // logic for music switching
        if (!isPlayerInAttackRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(FadeOut(BackgroundMusic, 0.5f));
                StartCoroutine(FadeIn(CombatMusic, 0.5f));
            }
        }
        else
        {
            if (distanceToPlayer > attackRange)
            {
                StartCoroutine(FadeOut(CombatMusic, 0.5f));
                StartCoroutine(FadeIn(BackgroundMusic, 0.5f));
            }
        }

        isPlayerInAttackRange = distanceToPlayer <= attackRange;

        if (!hasPlayerBeenDetected && distanceToPlayer <= visionRange && combatZone.entered)
        {
            hasPlayerBeenDetected = true;
            Bar.SetActive(true);
        }

        if (hasPlayerBeenDetected && !isPlayerInAttackRange)
        {
            SetAnimationState(walking: true, attacking: false);
            MoveToPlayer();
        }
        else if (isPlayerInAttackRange)
        {
            SetAnimationState(walking: true, attacking: true);
            AttackPlayer();
        }
        else
        {
            SetAnimationState(walking: false, attacking: false);
        }
    }

    private void MoveToPlayer()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Minotaur attacks the player!");
            animator.SetBool("attacking", true);

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth.TakeDamage(damage);
    }

    private void Die()
    {
        SetAnimationState(walking: false, attacking: false); 
        animator.SetBool("isDead", true);
        Debug.Log("Minotaur has died.");

        this.enabled = false;
    }

    private void SetAnimationState(bool walking, bool attacking)
    {
        if (animator != null)
        {
            animator.SetBool("walking", walking);
            animator.SetBool("attacking", attacking);
        }
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
