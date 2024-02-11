using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    PlayerController controller;

    // Variables for Speed Powerup
    float speedTime = 5.0f; // How long powerup is active
    float speedTimer; // Timer variable
    public bool hasSpeedPowerup = false; // Boolean for if the powerup is active
    float originalSpeedValue = 8.0f;
    float modifiedSpeedValue = 16.0f;
    float originalJumpValue = 4.0f;
    float modifiedJumpValue = 6.0f;

    // Variables for Rate Of Fire Powerup
    float rateOfFireTime = 5.0f;
    float rateOfFireTimer;
    public bool hasRateOfFirePowerup = false;
    float originalRateOfFireValue = 60.0f;
    float modifiedRateOfFireValue = 120.0f;

    void Start() {
        // Accesses the player controller
        controller = gameObject.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        // If Player enters the Speed Powerup
        if ((other.gameObject.tag == "SpeedPowerup") && (controller != null)) {
            controller.movementSpeed = modifiedSpeedValue;
            controller.jumpForce = modifiedJumpValue;
            hasSpeedPowerup = true;
            speedTimer = speedTime;
            Destroy(other.gameObject);
        }

        // If Player enters the RateOfFire Powerup
        if ((other.gameObject.tag == "RateOfFirePowerup") && (controller != null)) {
            controller._bulletsPerMinute = modifiedRateOfFireValue;
            hasRateOfFirePowerup = true;
            rateOfFireTimer = rateOfFireTime;
            Destroy(other.gameObject);
        }
    }

    void Update() {
        // If speed powerup, run timer and then revert movementSpeed to normal
        if (hasSpeedPowerup) {
            speedTimer -= Time.deltaTime;
            if (speedTimer < 0)
            {
                hasSpeedPowerup = false;
                controller.movementSpeed = originalSpeedValue;
                controller.jumpForce = originalJumpValue;
            }
        }

        // If rateOfFire powerup, run timer and then revert rateOfFire to normal
        if (hasRateOfFirePowerup) {
            rateOfFireTimer -= Time.deltaTime;
            if (rateOfFireTimer < 0)
            {
                hasRateOfFirePowerup = false;
                controller._bulletsPerMinute = originalRateOfFireValue;
            }
        }
    }
}
