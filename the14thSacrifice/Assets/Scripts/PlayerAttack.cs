using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int attackDamage = 10;
    public float attackRange = 5f;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0;

    [Header("References")]
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public PauseMenu pauseMenu;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(PlayerMovement.dialogue) return;
        if (pauseMenu.isPaused) return;

        if (Input.GetMouseButton(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            animator.SetBool("Attacking", true);
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        Debug.Log("Just attacked");
        Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in enemiesHit)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                Debug.Log(attackDamage + " damage");
            }
        }

        Invoke("ResetAttack", attackCooldown);
    }

    private void ResetAttack()
    {
        PlayerMovement.isAttacking = false;
        animator.SetBool("Attacking", false);
    }

    public void setDamage(int a) {
        attackDamage = a;
    }
}
