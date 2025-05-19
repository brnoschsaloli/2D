using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeStats : MonoBehaviour
{
    public LifeHUDManager lifeHudManager;
    public int maxHearts = 3;
    public int currentHearts;
    public Animator animator;

    void Start()
    {
        currentHearts = maxHearts;
        if (lifeHudManager != null)
        {
            lifeHudManager.UpdateHearts(currentHearts);
        }
        else
        {
            Debug.LogWarning("LifeHUDManager not assigned to PlayerLifeStats! Please assign it in the Inspector.");
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void TakeDamage()
    {
        currentHearts = Mathf.Max(0, currentHearts - 1);
        if (lifeHudManager != null)
        {
            lifeHudManager.UpdateHearts(currentHearts);
        }

        if (animator != null)
        {
            if (currentHearts > 0)
            {
                animator.SetBool("isHurt", true);
            }
            else
            {
                animator.SetTrigger("isDead");
                Debug.Log("Player isDead!");
                GameObject.FindWithTag("TenseMusic")?.GetComponent<AudioSource>()?.Stop();
                PlayerPrefs.SetString("GameResult", "Você não conseguiu cruzar a ilha dos desesperados. Tente novamente!");
                SceneManager.LoadScene("GameOverLose", LoadSceneMode.Additive);
                
                
            }
            
        }

        // if (currentHearts <= 0)
        // {
        //     Debug.Log("Player morreu!");
        //     // Add respawn or endgame logic here if needed
        // }
    }

    // public void ResetLife()
    // {
    //     currentHearts = maxHearts;
    //     if (lifeHudManager != null)
    //     {
    //         lifeHudManager.UpdateHearts(currentHearts);
    //     }
    //     if (animator != null)
    //     {
    //         animator.SetBool("isDead", false);
    //         animator.SetBool("isHurt", false);
    //     }
    //     Debug.Log("Vida restaurada!");
    // }

    public void EndHurt()
    {
        if (animator != null)
            animator.SetBool("isHurt", false);
    }
} 