using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance { get; private set; }

    // Assign your pause menu UI canvas in the inspector.
    [SerializeField] private GameObject pauseMenuUI;

    // Assign your pause menu.
    [SerializeField] private GameObject pauseMenu;
    
    // Assign your options menu.
    [SerializeField] private GameObject optionsMenu;
    
    // Assign your about menu.
    [SerializeField] private GameObject aboutMenu;
    
    // bool to keep track of if the game is paused
    private bool _isPaused;

    private void Start()
    {
        // Initialize the instance
        if (Instance == null)
            Instance = this;

        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GlobalScript.Instance.winLossManager.GameOver)
        {
            if (_isPaused)
                ResumeGame();

            else
                PauseGame();
        }
    }

    private void ResumeGame()
    {
        // hide the pause menu UI
        pauseMenuUI.SetActive(false);
        
        // Resume the game time.
        Time.timeScale = 1f; 
        
        _isPaused = false;
        
        GlobalScript.Instance.PlayGameplayMusic();
    }

    private void PauseGame()
    {
        // Only show the pause menu when pausing
        pauseMenu.SetActive(true);
        aboutMenu.SetActive(false);
        optionsMenu.SetActive(false);
        
        // show the pause menu UI
        pauseMenuUI.SetActive(true);
        
        // Freeze the game time.
        Time.timeScale = 0f; 
        
        _isPaused = true;
        
        GlobalScript.Instance.PlayPauseMusic();
    }

    public void GoToMainMenuFromPause()
    {
        // Reset the instance
        if (Instance != null)
        {
            Instance.ResumeGame();
            Instance = null;
        }

        // Load the main menu
        SceneManager.LoadScene("MAINMENU");

        ResumeGame();
    }

    public static bool Paused => Instance != null && Instance._isPaused;
}