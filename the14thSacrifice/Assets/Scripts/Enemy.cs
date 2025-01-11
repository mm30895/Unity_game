using UnityEngine;
using UnityEngine.AI;

public class Minotaur : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public NavMeshAgent navAgent;

    [Header("Settings")]
    public float attackCooldown = 2f;
    public int attackDamage = 20;

    private bool isPlayerInVisionRange = false;
    private bool isPlayerInAttackRange = false;
    private float lastAttackTime = 0;

    private void Update()
    {
        if (isPlayerInVisionRange && !isPlayerInAttackRange)
        {
            MoveToPlayer();
        }
        else if (isPlayerInAttackRange)
        {
            StopMoving();
            AttackPlayer();
        }
    }

    private void MoveToPlayer()
    {
        if (player != null)
        {
            navAgent.SetDestination(player.position);
        }
    }

    private void StopMoving()
    {
        navAgent.ResetPath();
    }

    private void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Minotaur attacks the player!");

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            lastAttackTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name == "VisionRange")
            {
                isPlayerInVisionRange = true;
                Debug.Log("Player entered vision range.");
            }
            else if (other.gameObject.name == "AttackRange")
            {
                isPlayerInAttackRange = true;
                Debug.Log("Player entered attack range.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name == "VisionRange")
            {
                isPlayerInVisionRange = false;
                Debug.Log("Player exited vision range.");
            }
            else if (other.gameObject.name == "AttackRange")
            {
                isPlayerInAttackRange = false;
                Debug.Log("Player exited attack range.");
            }
        }
    }
}
