using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPhase : MonoBehaviour
{
    public void LoadPhase1()
    {
        SceneManager.LoadScene("PhaseOneScene");
    }

    public void LoadPhase2()
    {
        SceneManager.LoadScene("PhaseTwoScene");
    }

    public void LoadPhase3()
    {
        SceneManager.LoadScene("PhaseThreeScene");
    }

    public void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu");
        SceneManager.LoadScene("Menu");
    }
}
