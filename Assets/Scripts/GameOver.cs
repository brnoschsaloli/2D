using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    void Start()
    {
        string result = PlayerPrefs.GetString("GameResult", "Você não conseguiu cruzar a ilha dos desesperados. Tente novamente!"); // valor padrão
        titleText.text = result;

        if (result == "Você não conseguiu cruzar a ilha dos desesperados. Tente novamente!")
        {
            titleText.color = Color.red;
        }
        else
        {
            titleText.color = Color.green;
        }
    }
}
