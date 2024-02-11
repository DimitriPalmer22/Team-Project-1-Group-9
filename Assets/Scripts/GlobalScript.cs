using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance { get; private set; }

    private static GameObject _player;
    public static GameObject Player => _player;
    

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the instance
        Instance = this;

        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
