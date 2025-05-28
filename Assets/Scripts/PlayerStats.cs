using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public HUDManager hudManager;
    [SerializeField] private AudioClip coincollect;
    public AudioSource sfxAudioSource;
    [SerializeField] private AudioClip takedamage;

    void Start()
    {
        if (hudManager != null)
        {
            hudManager.UpdateHearts(GameSession.Instance.currentHearts);
            // hudManager.UpdateCoins(GameSession.Instance.coins);
        }
        else
        {
            Debug.LogWarning("HUDManager not assigned to PlayerStats! Please assign it in the Inspector.");
        }
    }

    public void TakeDamage()
    {
        sfxAudioSource.PlayOneShot(takedamage);
        GameSession.Instance.currentHearts -= 1;
        if (hudManager != null)
        {
            hudManager.UpdateHearts(GameSession.Instance.currentHearts);
        }

        if (GameSession.Instance.currentHearts <= 0)
        {
            Debug.Log("Player morreu!");
            LoseCoinsOnDeath();
            GameSession.previousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("preAD");


            // Voltar para o ponto de respawn
            PlayerRespawn respawn = GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }

    public void AddCoin()
    {
        sfxAudioSource.PlayOneShot(coincollect);
        GameSession.Instance.AddCoin();
        if (hudManager != null)
        {
            hudManager.UpdateCoins(GameSession.Instance.coins);
        }
        Debug.Log("MOEDA");
    }

    public void LoseCoinsOnDeath()
    {
        GameSession.Instance.coins = Mathf.Max(0, GameSession.Instance.coins - 2);
        if (hudManager != null)
        {
            hudManager.UpdateCoins(GameSession.Instance.coins);
        }
        Debug.Log("O jogador perdeu 2 moedas!");
    }

    public void ResetLife()
    {
        GameSession.Instance.currentHearts = GameSession.Instance.maxHearts;
        if (hudManager != null)
        {
            hudManager.UpdateHearts(GameSession.Instance.currentHearts);
        }
        Debug.Log("Vida restaurada!");
    }

    // Debug
}
