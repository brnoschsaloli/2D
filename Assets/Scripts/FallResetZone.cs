using UnityEngine;
using UnityEngine.SceneManagement;

public class FallResetZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.LoseCoinsOnDeath();
            }


            GameSession.previousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("preAD");

            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}
