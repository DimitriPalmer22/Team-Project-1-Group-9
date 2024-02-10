using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance { get; private set; }
    
    private bool _isHardcore;
    private bool _isInfiniteHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the instance
        if (Instance == null)
            Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

}
