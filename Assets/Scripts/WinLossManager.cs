using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required if your win/loss UI uses the UI components.

public class WinLossManager : MonoBehaviour
{
    public GameObject winScreen; // Assign your win screen GameObject in the inspector
    public GameObject lossScreen; // Assign your loss screen GameObject in the inspector

    // Store the script for the player
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckLossCondition())
            ShowLossScreen();
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        lossScreen.SetActive(false);
        Time.timeScale = 0; // Pauses the game
        
        GlobalScript.Instance.PlayWinMusic();
    }

    /// <summary>
    /// Show the loss screen and pause the game
    /// </summary>
    public void ShowLossScreen()
    {
        lossScreen.SetActive(true);
        winScreen.SetActive(false);
        Time.timeScale = 0; // Pauses the game
        
        GlobalScript.Instance.PlayLoseMusic();
    }

    
    bool CheckLossCondition()
    {
        // Game is over when the player dies
        return !_playerController.IsAlive && !lossScreen.activeSelf; 
    }

    // Call this function to resume the game from either screen
    public void ResumeGame()
    {
        // Disable UI
        winScreen.SetActive(false);
        lossScreen.SetActive(false);
        
        // Resume the game
        Time.timeScale = 1; 
    }

    public void GoToMainMenu()
    {
        ResumeGame();

        // Load the main menu
        SceneManager.LoadScene("MAINMENU");
    }

    public void ExitApp()
    {
        ResumeGame();

        // Close game
        Application.Quit();
    }
}
