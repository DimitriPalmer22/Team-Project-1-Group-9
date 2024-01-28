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
    
    private bool _onGround;

    private bool _jumpPressed;
    
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
        {
            _jumpPressed = true;
            Debug.Log("Jump Pressed");
        }
    }

    void FixedUpdate()
    {
        TestIfOnGround();

        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     Debug.Log("JUMPING");
        //     _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        // }

        // Debug.Log($"ON GROUND: {_onGround}");
        
        MovementInput();
    }

    private void TestIfOnGround()
    {
        // variable to determine how tall the sprite is
        float rayCastLength = .5f + .025f;

        // Layer mask to make sure the ray cast only detects platforms
        LayerMask mask = LayerMask.GetMask("Ground");
        
        // the ray cast starts in the middle of the sprite and goes down the height of the sprite to determine if the sprite is on the ground
        RaycastHit2D hit = Physics2D.Raycast(
            origin: transform.position, 
            direction:-transform.up,
            distance: rayCastLength,
            layerMask: mask);

        _onGround = hit;

        if (_onGround)
            _spriteRenderer.color = Color.red;
        else
            _spriteRenderer.color = Color.white;
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
}