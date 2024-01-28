using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    // boolean used to determine if the player is on the ground
    private bool _onGround;

    // boolean used to keep track of if the jump buttons are pressed
    private bool _jumpPressed;

    private List<GameObject> _collidingPlatforms = new();
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool jumpButtonPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        if (jumpButtonPressed)
            _jumpPressed = true;
    }

    void FixedUpdate()
    {
        MovementInput();
    }

    void MovementInput()
    {
        // Get the left and right movement input (A & D or Left & Right)
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        
        // Flip sprite depending on which direction the player is moving
        // Going left
        if (horizontalInput < 0)
            _spriteRenderer.flipX = true;
        // Going right
        else if (horizontalInput > 0)
            _spriteRenderer.flipX = false;

        // Move the player horizontally
        transform.position += new Vector3(movementSpeed * horizontalInput, 0, 0) * Time.deltaTime;
        // _rb.AddForce(new Vector2(horizontalInput, 0) * movementSpeed);
        
        // Jump only if the player is on the ground
        // bool jumpButtonPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        // if (_onGround)
        if (_jumpPressed && _onGround)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            
            _onGround = false;
            _jumpPressed = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                if (other.transform.position.y < transform.position.y)
                    _collidingPlatforms.Add(other.gameObject);
                break;
            default:
                break;
        }
        
        DetermineIfOnGround();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                _collidingPlatforms.Remove(other.gameObject);
                break;
            default:
                break;
        }
        
        DetermineIfOnGround();
    }

    private void DetermineIfOnGround()
    {
        _onGround = _collidingPlatforms.Count > 0;
        
        if (_onGround)
            _spriteRenderer.color = Color.red;
        else
            _spriteRenderer.color = Color.white;
    }
}