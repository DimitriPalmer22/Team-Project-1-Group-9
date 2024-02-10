using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        // Load the gameplay scene
        SceneManager.LoadScene(1);
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
}