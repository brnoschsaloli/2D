using UnityEngine;

public class BgMusic : MonoBehaviour
{
    private static BgMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate music GameObjects
        }
    }
}
