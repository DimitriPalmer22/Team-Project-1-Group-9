using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Actor
{
    protected override Vector2 MovementInput()
    {
        return Vector2.zero;
    }

    protected override bool FireInput()
    {
        if (!_canFire)
            return false;
        
        return true;
    }


    protected override void Die()
    {
        Destroy(gameObject);
    }
    
}
