using UnityEngine;
using UnityEngine.SceneManagement;

public class preAD : MonoBehaviour
{
    // Arraste o seu Image de anúncio aqui no Inspector

    // Método ligado ao botão “Ad”
    public void Ad()
    {
        SceneManager.LoadScene("Ad");
    }


    // Método ligado ao botão “MainMenu”
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
