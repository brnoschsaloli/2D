using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount = 1)
    {
        currentHealth -= amount;
        Debug.Log("Boss took damage! Current health: " + currentHealth);

        // Optionally play a hurt animation
        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss died!");
        // Optionally play death animation
        if (animator != null)
            animator.SetTrigger("Death");
        // Disable boss logic, collider, etc.
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        // Optionally destroy the boss after a delay
        Destroy(gameObject, 2f);
    }
} 