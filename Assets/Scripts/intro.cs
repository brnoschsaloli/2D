using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public void LoadPhase()
    {
        SceneManager.LoadScene("PhaseOneScene");
    }
}
