using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HUDManager hudManager;

    void Start()
    {
        hudManager.UpdateHearts(GameSession.Instance.currentHearts);
        hudManager.UpdateCoins(GameSession.Instance.coins);
    }

    public void TakeDamage()
    {
        GameSession.Instance.TakeDamage();
        hudManager.UpdateHearts(GameSession.Instance.currentHearts);

        if (GameSession.Instance.currentHearts <= 0)
        {
            Debug.Log("Player morreu!");
            // lógica de morte
        }
    }

    public void AddCoin()
    {
        GameSession.Instance.AddCoin();
        hudManager.UpdateCoins(GameSession.Instance.coins);
    }

    // Debug
}
