using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseEndTrigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "PhaseTwoScene";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Fim da fase! Carregando próxima...");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
