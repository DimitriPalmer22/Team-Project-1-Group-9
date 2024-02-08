using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{

    // Unity components
    protected Rigidbody2D _rb;
    protected SpriteRenderer _spriteRenderer;
    
    [SerializeField] private int _health;
    
    // Shooting variables
    
    // A boolean used to determine if the player can shoot again
    protected bool _canFire = true;
    
    // Variable used to determine how fast the player's gun should fire
    [SerializeField] protected float _bulletsPerMinute;

    [SerializeField] protected float _bulletVelocity;

    // Game object to use as bullets
    [SerializeField] protected GameObject bulletPrefab;
    
    // A transform to use as a starting point for projectiles
    [SerializeField] protected Transform firingPoint;
    
    // a vector2 to determine how far away the 
    private Vector2 _firingPointOffset;
    private bool rightOrLeft = false;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Initialize components
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Save the offset of the firing point so that it flips when the player changes directions
        _firingPointOffset = firingPoint.localPosition;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        var movementInput = MovementInput();
        DetermineSpriteDirection(movementInput.x);
        
        // Shoot the gun if the actor is firing
        if (FireInput())
            Fire();
    }

    protected virtual void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Function that contains the movement & jump logic
    /// </summary>
    protected abstract Vector2 MovementInput();

    private void DetermineSpriteDirection(float horizontalInput)
    {
        // Flip sprite depending on which direction the player is moving
        // This code assumes that the sprite is facing right by default
        // Going left
        if (horizontalInput < 0)
        {
            _spriteRenderer.flipX = rightOrLeft = true;
            firingPoint.localPosition = new Vector3(-_firingPointOffset.x, _firingPointOffset.y, 0);
        }
        // Going right
        else if (horizontalInput > 0)
        {
            _spriteRenderer.flipX = rightOrLeft = false;
            firingPoint.localPosition = new Vector3(_firingPointOffset.x, _firingPointOffset.y, 0);
        }
    }

    protected abstract bool FireInput();

    /// <summary>
    /// Shoot a bullet
    /// </summary>
    private void Fire()
    {
        // Stop the actor from being able to fire again
        _canFire = false;
        
        // create a new bullet and access its script
        var bulletObject = Instantiate(bulletPrefab, parent: null, position: firingPoint.position, rotation: Quaternion.identity);
        var bulletScript = bulletObject.GetComponent<BulletScript>();

        // determine which direction vector the bullet is going to use
        var bulletVelocity = (rightOrLeft) ? -_bulletVelocity : _bulletVelocity;
        
        // start moving the bullet
        bulletScript.MoveBullet(new Vector2(bulletVelocity, 0), tag);
        
        // Start a coroutine to tick the gun's fire rate
        StartCoroutine(TickFireRate());
    }
    
    /// <summary>
    /// A coroutine that determines when the player can fire again based on
    /// their gun's fire rate
    /// </summary>
    /// <returns></returns>
    private IEnumerator TickFireRate()
    {
        yield return new WaitForSeconds(_bulletsPerMinute / 60f);

        _canFire = true;
    }

    /// <summary>
    /// Handle what happens when the enemy dies
    /// </summary>
    protected abstract void Die();
    
    /// <summary>
    /// Handle what happens when the enemy gets hit and takes damage
    /// </summary>
    /// <param name="amount"></param>
    public void LoseHealth(int amount)
    {
        // Lose health
        _health -= amount;
        
        // Die if health is too low
        if (_health <= 0)
            Die();
    }
}
