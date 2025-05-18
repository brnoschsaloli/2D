using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
            animator.SetBool("isHurt", true);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void EndHurt()
    {
        animator.SetBool("isHurt", false);
    }

    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("isDead");
            // Collider2D col = GetComponent<Collider2D>();
            // if (col != null) col.enabled = false;

            // // Disable other scripts (replace BossAI with your actual script names)
            // // var ai = GetComponent<Crab Boss>();
            // // if (ai != null) ai.enabled = false;

            // animator.SetBool("isHurt", false);
            // animator.SetBool("IsWalking", false);

            StartCoroutine(DestroyAfterDeath());
            GameObject.FindWithTag("TenseMusic")?.GetComponent<AudioSource>()?.Stop();
            PlayerPrefs.SetString("GameResult", "Você conseguiu cruzar a ilha dos desesperados! Parabéns!");
            Time.timeScale = 0f;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSecondsRealtime(0.5f); // Replace 1.5f with your death animation length
        Destroy(gameObject);
    }
} 