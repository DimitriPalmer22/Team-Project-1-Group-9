using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    // boolean used to determine if the player is on the ground
    private bool _onGround;

    // boolean used to keep track of if the jump buttons are pressed
    private bool _jumpThisFrame;

    /// <summary>
    /// List that keeps track of if the player is currently on a platform or not.
    /// Used strictly to set the value of the _onGround variable.
    /// </summary>
    private readonly List<GameObject> _collidingPlatforms = new();

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        FireInput();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        // Jump if the variable to jump this frame is true 
        if (_jumpThisFrame)
        {
            // Jump using the rigid body
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            
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
        
        // Move the player horizontally
        // Use the transform.position to move the player for movement similar to the original metal slug
        transform.position += new Vector3(movementSpeed * horizontalInput, 0, 0) * Time.deltaTime;
        
        /* Test if the player has pressed the jump button this frame.
         * If they have, set the variable that tells the script to jump during this frame
         */
        bool jumpButtonPressed = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        if (jumpButtonPressed && _onGround)
            _jumpThisFrame = true;

        return new Vector2(horizontalInput, 0);
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
                
                if (other.transform.position.y < transform.position.y)
                    _collidingPlatforms.Add(other.gameObject);
                break;
        }
        
        DetermineIfOnGround();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            // Test if the player just stopped touching a ground game object
            // If they did, remove that platform from the list of colliding platforms
            case "Ground":
                _collidingPlatforms.Remove(other.gameObject);
                break;
        }
        
        DetermineIfOnGround();
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
}