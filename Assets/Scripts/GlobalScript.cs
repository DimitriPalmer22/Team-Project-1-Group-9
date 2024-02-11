using TMPro;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance { get; private set; }

    private static GameObject _player;
    public static GameObject Player => _player;

    [SerializeField] public WinLossManager winLossManager;

    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip pauseMusic;
    [SerializeField] private AudioClip winMusic;
    [SerializeField] private AudioClip loseMusic;

    private AudioSource _audioSource;
    
    [SerializeField] private TMP_Text scoreText;
    public TMP_Text ScoreText => scoreText;

    private int _score = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the instance
        Instance = this;

        _player = GameObject.FindWithTag("Player");

        Time.timeScale = 1;

        _audioSource = GetComponent<AudioSource>();
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

        // _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void PlayWinMusic() => PlayMusic(winMusic);
    public void PlayLoseMusic() => PlayMusic(loseMusic);

    public void PlayGameplayMusic() => PlayMusic(gameplayMusic);
    public void PlayPauseMusic() => PlayMusic(pauseMusic);

    public void AddScore(int amount)
    {
        if (scoreText == null)
            return;
        
        _score += amount;
        
        scoreText.text = $"Score : {_score}";
    }
    
}
