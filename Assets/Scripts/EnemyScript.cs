using UnityEngine;

public class EnemyScript : Actor
{
    // If the player is farther away than this many units, then this enemy will not shoot 
    private const float ShootPlayerProximity = 15f;
    
    // A reference to the player
    private GameObject _player;

    // used to test if the enemy is being rendered by the camera
    private bool _onScreen;

    protected override void Start()
    {
        base.Start();

        // Get the player. There should only be one in the scene
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override Vector2 MovementInput()
    {
        return Vector2.zero;
    }

    protected override bool FireInput()
    {
        if (!_canFire)
            return false;

        float distance = Vector2.Distance(transform.position, _player.transform.position);

        // Skip if the player is too far away
        if (distance >= ShootPlayerProximity)
            return false;

        if (!_onScreen)
            return false;
        
        return true;
    }


    protected override void Die()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Used when the enemy comes on screen
    /// </summary>
    private void OnBecameVisible()
    {
        _onScreen = true;
        
        // Temporarily stop the enemy from shooting so they don't immediately fire at the player
        // when this enemy becomes visible again
        ResetCanFire();
    }

    private void OnBecameInvisible()
    {
        _onScreen = false;
    }
}
