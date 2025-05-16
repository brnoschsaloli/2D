using UnityEngine;
using UnityEngine.UI;

public class LifeHUDManager : MonoBehaviour
{
    public Image[] hearts;

    public void UpdateHearts(int currentHearts)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHearts;
        }
    }
} 