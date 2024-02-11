using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArtilleryBulletScript : BulletScript
{

    [SerializeField] private Rigidbody2D rb;

    protected override void Update()
    {
        // if the bullet lives for too long, destroy it
        _remainingDuration -= Time.deltaTime;
        if (_remainingDuration <= 0)
            Destroy(gameObject);
        
    }

    public override void MoveBullet(Vector2 velocity, string shooterTag, int damage)
    {
        base.MoveBullet(velocity, shooterTag, damage);

        rb.AddForce(velocity, ForceMode2D.Impulse);
    }
}
