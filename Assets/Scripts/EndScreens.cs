using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreens : MonoBehaviour
{
    public void ReturnToMenu ()
    {
        // Load the gameplay scene
        SceneManager.LoadScene(0);
    }
    public void ExitGame ()
    {
        Application.Quit();
    }
}