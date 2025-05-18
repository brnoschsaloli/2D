using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public GameObject playAgainButton;  

    private const string failureMessage = "Você não conseguiu cruzar a ilha dos desesperados. Tente novamente!";

    void Start()
    {
        string result = PlayerPrefs.GetString("GameResult", failureMessage);
        titleText.text = result;

        bool isFailure = result == failureMessage;
        titleText.color = isFailure ? Color.red : Color.green;

        playAgainButton.SetActive(isFailure);
    }
}
