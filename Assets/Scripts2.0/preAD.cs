using UnityEngine;
using UnityEngine.SceneManagement;

public class preAD : MonoBehaviour
{
    // Arraste o seu Image de an�ncio aqui no Inspector

    // M�todo ligado ao bot�o �Ad�
    public void Ad()
    {
        SceneManager.LoadScene("Ad");
    }


    // M�todo ligado ao bot�o �MainMenu�
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
