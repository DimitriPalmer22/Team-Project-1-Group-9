using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : Actor
{
    // boolean used to determine if the player is on the ground
    private bool _onGround;

    // boolean used to keep track of if the jump buttons are pressed
    private bool _jumpThisFrame;

    /// <summary>
    /// List that keeps track of if the player is currently on a platform or not.
    /// Used strictly to set the value of the _onGround variable.
    /// </summary>
    private readonly List<GameObject> _collidingPlatforms = new();

    /// <summary>
    /// The animator used to control the player's firing animation
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// bool used to control the animator's current animation
    /// </summary>
    private bool _shootingThisFrame;

    [SerializeField] private TMP_Text healthText;

    private Powerups _powerUps;

    protected override float BulletsPerMinute
    {
        get
        {
            float powerUpMultiplier = 1;
            if (_powerUps != null && _powerUps.hasRateOfFirePowerUp)
                powerUpMultiplier = Powerups.RateOfFireMultiplier;
            
            return _bulletsPerMinute * powerUpMultiplier;
        }
    }

    protected override void Start()
    {
        base.Start();

        // Get the animator
        _animator = GetComponent<Animator>();

        // Let the actor script know that this is a player
        _isPlayer = true;
        
        // Get the powerups script
        _powerUps = GetComponent<Powerups>();
    }

    protected override void Update()
    {
        // Reset the test for shooting this frame
        _shootingThisFrame = false;
        
        base.Update();
        
        // Determine the current animation
        DetermineAnimation();
        
        // Update Health Text
        UpdateHealthText();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        // Jump if the variable to jump this frame is true
        // the input for the player to jump is inside update
        // the method of actually jumping is in the fixed update due to using rigid body
        if (_jumpThisFrame)
        {
            Jump();

            float powerUpMultiplier = 1;
            if (_powerUps.hasSpeedPowerUp)
                powerUpMultiplier = Powerups.JumpMultiplier;
            
            // Jump using the rigid body
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(transform.up * (jumpForce * powerUpMultiplier), ForceMode2D.Impulse);
            
            // reset variables
            _onGround = false;
            _jumpThisFrame = false;
        }
    }
    
    /// <summary>
    /// Function that contains the movement & jump logic
    /// </summary>
    protected override Vector2 MovementInput()
    {
        // Get the left and right movement input (A & D or Left & Right)
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        
        // Test if the player has pressed the jump button this frame.
        // If they have, set the variable that tells the script to jump during this frame
        bool jumpButtonPressed = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        if (jumpButtonPressed && _onGround)
            _jumpThisFrame = true;

        float powerUpMultiplier = 1;
        if (_powerUps.hasSpeedPowerUp)
            powerUpMultiplier = Powerups.SpeedMultiplier;
        
        return new Vector2(horizontalInput * powerUpMultiplier, 0);
    }
    
    /// <summary>
    /// Test if the player is shooting
    /// </summary>
    protected override bool FireInput()
    {
        if (!Input.GetKey(KeyCode.Space))
            return false;
        
        if (!_canFire)
            return false;

        // Set the flag that determines if the player is shooting this frame
        _shootingThisFrame = true;
        
        return true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            // Test if the player is currently touching a ground game object
            case "Ground":
                /* Test if the ground is below the player.
                 * If it is, then add it to the list of platforms the player is currently touching
                 */

                // if (_rb.velocity.y == 0)
                //     _onGround = true;
                
                
                if (other.contacts[0].point.y < transform.position.y)
                    _collidingPlatforms.Add(other.gameObject);
                
                break;
        }
        
        // DetermineIfOnGround();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        
        switch (other.gameObject.tag)
        {
            // Test if the player is currently touching a ground game object
            case "Ground":
                /* Test if the ground is below the player.
                 * If it is, then add it to the list of platforms the player is currently touching
                 */

                if (MathF.Abs(_rb.velocity.y) < 0.025f)
                    _onGround = true;
                
                
                // if (other.contacts[0].point.y < transform.position.y)
                //     _collidingPlatforms.Add(other.gameObject);
                
                break;
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            // Test if the player just stopped touching a ground game object
            // If they did, remove that platform from the list of colliding platforms
            case "Ground":
                _collidingPlatforms.Remove(other.gameObject);

                _onGround = false;
                
                break;
        }
        
        // DetermineIfOnGround();
    }

    /// <summary>
    /// Function used to determine if the player is currently on the ground.
    /// The player is on the ground if the list of colliding platforms is not empty.
    /// </summary>
    private void DetermineIfOnGround()
    {
        // The player is on the ground if the list of colliding platforms is not empty.
        _onGround = _collidingPlatforms.Count > 0;
        
        // Debugging help. Sprite changes color when on the ground.
        // Remove once the sprite is implemented.
        if (_onGround)
            _spriteRenderer.color = new Color(.6f, 1, .6f, 1);
        else
            _spriteRenderer.color = Color.white;
    }
    
    protected override void Die()
    {
        // TODO: Die
    }

    /// <summary>
    /// Determine which animation should currently be playing
    /// </summary>
    private void DetermineAnimation()
    {
        if (!_shootingThisFrame)
            return;
        
        // Force the shooting animation to play / restart if a bullet is fired this frame
        _animator.Play("Player Shooting Animation", -1, 0f);
    }
    
    private void UpdateHealthText()
    {
        if (healthText == null)
            return;
        
        if (GameSettings.IsHardcore)
        {
            healthText.text = "HARDCORE";
            return;
        }

        if (GameSettings.IsInfiniteHealth)
        {
            healthText.text = "Health : INFINITE";
            return;
        }

        healthText.text = $"Health : {Health}";
    }
    
}