using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int health;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Handle what happens when the enemy gets hit and takes damage
    /// </summary>
    /// <param name="amount"></param>
    public void LoseHealth(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    /// <summary>
    /// Handle what happens when the enemy dies
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }
    
}
