using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance { get; private set; }
    

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the instance
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
