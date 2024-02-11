using UnityEngine;

public class Powerups : MonoBehaviour
{
    private PlayerController _controller;

    // Variables for Speed Power up
    public bool hasSpeedPowerUp { get; private set; }
    public const float SpeedMultiplier = 1.5f;
    public const float JumpMultiplier = 1.25f;
    private const float SpeedTime = 5.0f; // How long power up is active
    private float _speedTimer; // Timer variable

    // Variables for Rate Of Fire Power up
    public bool hasRateOfFirePowerUp { get; private set; }
    public const float RateOfFireMultiplier = 2f;
    private const float RateOfFireTime = 5.0f;
    private float _rateOfFireTimer;

    void Start()
    {
        // Accesses the player controller
        _controller = GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_controller == null)
            return;

        switch (other.gameObject.tag)
        {
            // If Player enters the Speed Powerup
            case "SpeedPowerup":
                hasSpeedPowerUp = true;
                _speedTimer = SpeedTime;

                Destroy(other.gameObject);
                break;

            // If Player enters the RateOfFire Powerup
            case "RateOfFirePowerup":
                hasRateOfFirePowerUp = true;
                _rateOfFireTimer = RateOfFireTime;

                Destroy(other.gameObject);
                break;
        }
    }

    void Update()
    {
        // If speed powerup, run timer and then revert movementSpeed to normal
        if (hasSpeedPowerUp)
        {
            _speedTimer -= Time.deltaTime;

            if (_speedTimer <= 0)
                hasSpeedPowerUp = false;
        }

        // If rateOfFire powerup, run timer and then revert rateOfFire to normal
        if (hasRateOfFirePowerUp)
        {
            _rateOfFireTimer -= Time.deltaTime;

            if (_rateOfFireTimer <= 0)
                hasRateOfFirePowerUp = false;
        }
    }
}