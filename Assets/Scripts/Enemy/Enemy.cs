using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float moveSpeed = 3.5f;
    
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private float currentHealth;
    private float lastAttackTime;
    private bool isAlive = true;

    private void Start()
    {
        currentHealth = health;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        if (!isAlive) return;
        
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < detectionRange)
        {
            navMeshAgent.SetDestination(playerTransform.position);

            if (distanceToPlayer < attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            navMeshAgent.ResetPath();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void Die()
    {
        isAlive = false;
        navMeshAgent.enabled = false;
        Destroy(gameObject, 1f);
    }

    public bool IsAlive => isAlive;
    public float GetHealth => currentHealth;
}