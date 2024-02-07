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
        
    }
}
