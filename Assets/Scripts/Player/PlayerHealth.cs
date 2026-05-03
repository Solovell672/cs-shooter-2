using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float healthRegenRate = 5f;
    [SerializeField] private float regenDelay = 3f;
    
    private float currentHealth;
    private float lastDamageTime;
    private bool isAlive = true;

    private void Start()
    {
        currentHealth = maxHealth;
        gameObject.tag = "Player";
    }

    private void Update()
    {
        if (!isAlive) return;
        
        // Regenerate health after delay
        if (Time.time >= lastDamageTime + regenDelay)
        {
            currentHealth = Mathf.Min(currentHealth + healthRegenRate * Time.deltaTime, maxHealth);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        lastDamageTime = Time.time;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        Debug.Log("Player died!");
        Time.timeScale = 0; // Pause game
    }

    public float GetHealth => currentHealth;
    public float GetMaxHealth => maxHealth;
    public bool IsAlive => isAlive;
}