using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private const float Duration = 5;
    
    private Vector2 _velocity;
    private string _shooterTag;

    private float _remainingDuration = Duration;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the bullet lives for too long, destroy it
        _remainingDuration -= Time.deltaTime;
        if (_remainingDuration <= 0)
            Destroy(gameObject);
        
        transform.position += new Vector3(_velocity.x, _velocity.y, 0) * Time.deltaTime;
    }

    /// <summary>
    /// Start the movement of the bullet after its instantiated
    /// </summary>
    /// <param name="velocity">How fast the bullet moves</param>
    /// <param name="shooterTag">Tag of the person who shot the bullet</param>
    public void MoveBullet(Vector2 velocity, string shooterTag)
    {
        _shooterTag = shooterTag;
        
        _velocity = velocity;
        Debug.Log($"SET VELOCITY TO: {velocity}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // skip the function if friendly fire
        if (other.gameObject.CompareTag(_shooterTag))
            return;
        
        // skip this function if the bullet collides with something other than a player or enemy
        switch (other.gameObject.tag)
        {
            case "Enemy":
                var enemyScript = other.GetComponent<EnemyScript>();
                enemyScript.LoseHealth(1);
                break;
            case "Player":
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
