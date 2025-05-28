using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }
    public static string previousScene;
    public int coins = 0;
    public int maxHearts = 3;
    public int currentHearts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentHearts = maxHearts;
        }
        else
        {
            Destroy(gameObject); // evita duplicatas
        }
    }

    public void AddCoin()
    {
        coins++;
    }

    public void TakeDamage()
    {
        currentHearts = Mathf.Max(0, currentHearts - 1);
    }

    public void ResetSession()
    {
        coins = 0;
        currentHearts = maxHearts;
    }
}
