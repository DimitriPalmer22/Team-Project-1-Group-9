using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required if your win/loss UI uses the UI components.

public class WinLossManager : MonoBehaviour
{
    public GameObject winScreen; // Assign your win screen GameObject in the inspector
    public GameObject lossScreen; // Assign your loss screen GameObject in the inspector

    // Update is called once per frame
    void Update()
    {
        // Assuming CheckWinCondition() and CheckLossCondition() are methods 
        // that return a bool indicating whether the win or loss conditions have been met.
        if (CheckWinCondition())
        {
            // Show the win screen and pause the game
            ShowWinScreen();
        }
        else if (CheckLossCondition())
        {
            // Show the loss screen and pause the game
            ShowLossScreen();
        }
    }

    void ShowWinScreen()
    {
        winScreen.SetActive(true);
        lossScreen.SetActive(false);
        Time.timeScale = 0; // Pauses the game
        // Optionally, disable player controls here.
    }

    void ShowLossScreen()
    {
        lossScreen.SetActive(true);
        winScreen.SetActive(false);
        Time.timeScale = 0; // Pauses the game
        // Optionally, disable player controls here.
    }

    // Implement these methods based on your game's win/loss conditions
    bool CheckWinCondition()
    {
        // Your win condition code here
        return false; // Placeholder return
    }

    bool CheckLossCondition()
    {
        // Your loss condition code here
        return false; // Placeholder return
    }

    // Call this function to resume the game from either screen
    public void ResumeGame()
    {
        winScreen.SetActive(false);
        lossScreen.SetActive(false);
        Time.timeScale = 1; // Resumes the game
        // Optionally, re-enable player controls here.
    }
}
