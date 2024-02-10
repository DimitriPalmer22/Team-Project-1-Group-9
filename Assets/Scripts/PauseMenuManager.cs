using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance { get; private set; }

    // Assign your pause menu UI canvas in the inspector.
    [SerializeField] private GameObject pauseMenuUI; 

    // bool to keep track of if the game is paused
    private bool _isPaused;

    private void Start()
    {
        // Initialize the instance
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    private void PauseGame()
    {
        // show the pause menu UI
        pauseMenuUI.SetActive(true);
        
        // Freeze the game time.
        Time.timeScale = 0f; 
        
        _isPaused = true;
    }

    public static bool Paused => Instance != null && Instance._isPaused;
}