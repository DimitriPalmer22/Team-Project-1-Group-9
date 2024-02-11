using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance { get; private set; }

    private static GameObject _player;
    public static GameObject Player => _player;

    [SerializeField] private GameObject _loseUI;
    [SerializeField] private GameObject _winUI;
    

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the instance
        Instance = this;

        _player = GameObject.FindWithTag("Player");
        
        _loseUI.SetActive(false);
        _winUI.SetActive(false);
    }

    public void Win()
    {
        // Freeze the game time.
        Time.timeScale = 0f; 
        
        _loseUI.SetActive(false);
        _winUI.SetActive(true);
    }

    public void Lose()
    {
        // Freeze the game time.
        Time.timeScale = 0f; 
        
        _loseUI.SetActive(true);
        _winUI.SetActive(false);
    }

}
