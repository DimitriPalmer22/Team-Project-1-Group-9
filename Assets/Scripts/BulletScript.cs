using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private const float Duration = 5;
    
    protected  Vector2 _velocity;
    protected  string _shooterTag;
    protected int _damage;

    protected float _remainingDuration = Duration;
    
    // Update is called once per frame
    protected virtual void Update()
    {
        // if the bullet lives for too long, destroy it
        _remainingDuration -= Time.deltaTime;
        if (_remainingDuration <= 0)
            Destroy(gameObject);
        
        transform.position += new Vector3(_velocity.x, _velocity.y, 0) * Time.deltaTime;
    }

    /// <summary>
    /// Start the movement of the bullet after its instantiated.
    /// Also used to store variables within the BulletScript class.
    /// </summary>
    /// <param name="velocity">How fast the bullet moves</param>
    /// <param name="shooterTag">Tag of the person who shot the bullet</param>
    /// <param name="damage">How much damage the bullet does</param>
    public virtual void MoveBullet(Vector2 velocity, string shooterTag, int damage)
    {
        _shooterTag = shooterTag;
        _velocity = velocity;
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // skip the function if friendly fire
        if (other.gameObject.CompareTag(_shooterTag))
            return;

        // Debug.Log($"TAGS: _{other.gameObject.tag} ({other.gameObject}) _{_shooterTag}");
        
        // skip this function if the bullet collides with something other than a player or enemy
        switch (other.gameObject.tag)
        {
            case "Enemy":
            case "Player":
                // Get the actor script of the player / enemy
                var actorScript = other.GetComponent<Actor>();
                
                // take away health
                actorScript.LoseHealth(_damage);
                break;
            
            case "Ground":
                // destroy the bullet if it hits the tile map
                Destroy(gameObject);
                break;
            
            default:
                return;
        }
        
        // destroy the bullet if it hits an enemy or player
        Destroy(gameObject);
    }
}
