using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Actor : MonoBehaviour
{

    #region Variables

    protected bool _isPlayer;
    
    // Unity components
    protected Rigidbody2D _rb;
    protected SpriteRenderer _spriteRenderer;

    private Vector2 _movementInput;
    
    // Movement variables
    [Header("Movement")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float jumpForce;
    
    
    // Shooting variables
    // A boolean used to determine if the player can shoot again
    protected bool _canFire = true;
    
    [Header("Shooting")]
    
    // Variable used to determine how fast the player's gun should fire
    [SerializeField] protected float _bulletsPerMinute;

    // the speed at which the bullet travels
    [SerializeField] protected float _bulletVelocity;

    [SerializeField] protected int _bulletDamage;

    // Game object to use as bullets
    [SerializeField] protected GameObject bulletPrefab;
    
    // A transform to use as a starting point for projectiles
    [SerializeField] protected Transform firingPoint;
    
    [SerializeField] private int _health;
    public int Health => _health;
    
    // Determine which firing vector to use
    [FormerlySerializedAs("_shooting")] [SerializeField] private ShootingDirection _shootingDirection;
    
    // a vector2 to determine how far away the 
    protected Vector2 _firingPointOffset;
    protected bool _rightOrLeft;
    
    // Particles variables
    
    // Variable for the particle system used when shooting
    protected ParticleSystem _shootingParticleSystem;

    // Variable for the particle system used when jumping
    private ParticleSystem _jumpingParticleSystem;
    
    // Audio Variables
    
    [Header("Audio")]

    // Sound that plays when this actor takes damage
    [SerializeField] private AudioClip hurtSound;
    
    // Sound that plays when this actor shoots
    [SerializeField] private AudioClip shootSound;

    // Sound that plays when this actor jumps
    [SerializeField] private AudioClip jumpSound;

    // Audio source that is responsible for playing hit and shoot sounds
    private AudioSource _audioSource;

    
    #endregion Variables

    #region Unity Event Methods

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Initialize components
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Save the offset of the firing point so that it flips when the player changes directions
        _firingPointOffset = firingPoint.localPosition;
        
        // Get particle systems
        _shootingParticleSystem = firingPoint.GetComponent<ParticleSystem>();
        _jumpingParticleSystem = GetComponent<ParticleSystem>();
        
        // Get audio source
        _audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (PauseMenuManager.Paused)
            return;
        
        _movementInput = MovementInput();
        DetermineSpriteDirection(_movementInput.x);
        
        // Shoot the gun if the actor is firing
        if (FireInput())
            Fire();
    }

    protected virtual void FixedUpdate()
    {
        MoveRigidBody();
    }

    #endregion
    
    #region Movement Methods
    
    /// <summary>
    /// Function that contains the movement & jump logic
    /// </summary>
    /// <returns></returns>
    protected abstract Vector2 MovementInput();

    /// <summary>
    /// Flip the way the sprite is facing based on the actor's horizontal movement
    /// </summary>
    /// <param name="horizontalInput">The actor's horizontal movement input</param>
    private void DetermineSpriteDirection(float horizontalInput)
    {
        // Flip sprite depending on which direction the player is moving
        // This code assumes that the sprite is facing right by default
        // Going left
        if (horizontalInput < 0)
        {
            _spriteRenderer.flipX = _rightOrLeft = true;
            firingPoint.localPosition = new Vector3(-_firingPointOffset.x, _firingPointOffset.y, 0);
            
            // Flip the shooting particle emitter
            var shape = _shootingParticleSystem.shape;
            shape.rotation = new Vector3(shape.rotation.x, -90, shape.rotation.z);
        }
        // Going right
        else if (horizontalInput > 0)
        {
            _spriteRenderer.flipX = _rightOrLeft = false;
            firingPoint.localPosition = new Vector3(_firingPointOffset.x, _firingPointOffset.y, 0);

            // Flip the shooting particle emitter
            var shape = _shootingParticleSystem.shape;
            shape.rotation = new Vector3(shape.rotation.x, 90, shape.rotation.z);
            
        }
    }

    private void MoveRigidBody()
    {
        // Move the player horizontally
        // Preserve the actor's vertical velocity
        // Use the RigidBody.velocity to move the player for movement similar to the original metal slug
        _rb.velocity = new Vector2(movementSpeed * _movementInput.x, _rb.velocity.y);
    }

    protected void Jump()
    {
        // Emit the shooting particle system
        _jumpingParticleSystem.Emit(100);
        
        // Play the jump sound
        PlaySound(jumpSound);
    }
    
    #endregion
    
    #region Shooting and Combat Methods

    protected abstract bool FireInput();

    /// <summary>
    /// Shoot a bullet
    /// </summary>
    private void Fire()
    {
        // create a new bullet and access its script
        var bulletObject = Instantiate(bulletPrefab, parent: null, position: firingPoint.position, rotation: Quaternion.identity);
        var bulletScript = bulletObject.GetComponent<BulletScript>();

        // start moving the bullet
        bulletScript.MoveBullet(GetFiringDirection().normalized * _bulletVelocity, tag, _bulletDamage);
        
        // Stop the actor from being able to fire again
        // Start a coroutine to tick the gun's fire rate
        ResetCanFire();

        // Emit the shooting particle system
        _shootingParticleSystem.Emit(100);
        
        // Play the shoot sound
        PlaySound(shootSound);
    }

    protected void ResetCanFire()
    {
        // Stop the actor from being able to fire again
        _canFire = false;
        
        // Start a coroutine to tick the gun's fire rate
        StartCoroutine(TickFireRate());

    }

    private Vector2 GetFiringDirection()
    {
        Vector2 shootingVector;
        
        switch (_shootingDirection)
        {
            case ShootingDirection.Normal:
                shootingVector = new Vector2(1, 0);
                break;
            case ShootingDirection.Artillery:
                shootingVector = new Vector2(1, 1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        // determine which direction vector the bullet is going to use
        // Going left
        if (_rightOrLeft)
            shootingVector = new Vector2(-shootingVector.x, shootingVector.y);
        
        return shootingVector;
    }
    
    /// <summary>
    /// A coroutine that determines when the player can fire again based on
    /// their gun's fire rate
    /// </summary>
    /// <returns></returns>
    private IEnumerator TickFireRate()
    {
        float timeToWait = 60f / _bulletsPerMinute;
        yield return new WaitForSeconds(timeToWait);

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
        Debug.Log($"IS PLAYER?: {_isPlayer} {GameSettings.IsHardcore} {GameSettings.IsInfiniteHealth}");
        
        // Don't lose health if this is the player and there is infinite health
        if (GameSettings.IsInfiniteHealth && _isPlayer)
            return;
        
        // Lose health
        _health -= amount;

        // If hardcore, die
        if (GameSettings.IsHardcore && _isPlayer)
            _health = 0;
        
        // Play the hurt sound
        PlaySound(hurtSound);
        
        // Die if health is too low 
        if (_health <= 0)
            Die();
    }
    
    #endregion
    
    private void PlaySound(AudioClip clip)
    {
        // skip this function if the clip is null
        if (clip == null)
            return;
        
        // Swap the clip
        _audioSource.clip = clip;
        
        // Play the clip
        _audioSource.Play();
    }

    public bool IsAlive => _health > 0;

}

public enum ShootingDirection
{
    Normal, 
    Artillery
}