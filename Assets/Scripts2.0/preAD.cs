using UnityEngine;
using UnityEngine.SceneManagement;

public class preAD : MonoBehaviour
{
    private RewardedAdsButton rewardedAdsButton;

    private void Awake()
    {
        rewardedAdsButton = FindAnyObjectByType<RewardedAdsButton>();
        if (rewardedAdsButton != null)
        {
            rewardedAdsButton.LoadAd();
        }
        else
        {
            Debug.LogError("RewardedAdsButton instance not found in the scene.");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
