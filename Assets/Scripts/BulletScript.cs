using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private const float Duration = 5;
    
    
    private Vector2 _velocity;

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

    public void MoveBullet(Vector2 velocity)
    {
        _velocity = velocity;
        Debug.Log($"SET VELOCITY TO: {velocity}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
                // destroy the bullet if it hits the tilemap
                Destroy(gameObject);
                break;
            default:
                return;
        }
        
        // destroy the bullet if it hits an enemy or player
        Destroy(gameObject);
    }
}
