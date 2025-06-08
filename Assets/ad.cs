using UnityEngine;
using UnityEngine.SceneManagement;

public class ad : MonoBehaviour
{
   

    // Update is called once per frame
     public void X()
    {
        SceneManager.LoadScene(GameSession.previousScene);
    }
}
