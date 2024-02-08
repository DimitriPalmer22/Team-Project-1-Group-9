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
        return false;
    }


    protected override void Die()
    {
        Destroy(gameObject);
    }
    
}
