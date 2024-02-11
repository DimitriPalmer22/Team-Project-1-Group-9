using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLossZoneScript : MonoBehaviour
{
    [SerializeField] private WinLossZoneType zoneType;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (zoneType)
        {
            case WinLossZoneType.Win:
                WinZoneLogic(other);
                break;
            case WinLossZoneType.Loss:
                LossZoneLogic(other);
                break;
            default:
                return;
        }
        
    }

    private void WinZoneLogic(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                // Get the actor script of the player / enemy
                var actorScript = other.GetComponent<Actor>();
                
                // Win the game
                GlobalScript.Instance.winLossManager.ShowWinScreen();
                break;
            default:
                return;
        }
    }

    private void LossZoneLogic(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
            case "Player":
                // Get the actor script of the player / enemy
                var actorScript = other.GetComponent<Actor>();
                
                // take away health
                actorScript.LoseHealth(actorScript.Health);
                break;
            default:
                return;
        }
    }
    
}

public enum WinLossZoneType
{
    Win,
    Loss
}