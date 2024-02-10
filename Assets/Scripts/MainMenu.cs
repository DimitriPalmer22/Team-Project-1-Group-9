using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the gameplay scene
        SceneManager.LoadScene("Assets Made");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}