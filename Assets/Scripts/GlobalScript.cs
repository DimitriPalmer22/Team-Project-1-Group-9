using System;
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

    private int _score;

    [SerializeField] private TMP_Text timerText;

    private float _timeRemaining = 90;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the instance
        Instance = this;

        _player = GameObject.FindWithTag("Player");

        Time.timeScale = 1;

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (winLossManager != null && winLossManager.GameOver)
            return;
        
        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining < 0)
            _timeRemaining = 0;
        
        UpdateTimerText();
        
        if (_timeRemaining <= 0)
            winLossManager.ShowLossScreen();
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

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

    public void UpdateTimerText()
    {
        if (timerText == null)
            return;
        
        float divTime = _timeRemaining;
        
        int minutes = (int) (divTime / 60);
        divTime %= 60;

        int seconds = (int) divTime;

        timerText.text = $"{minutes.ToString().PadLeft(2, '0')}:{seconds.ToString().PadLeft(2, '0')}";
    }
    
}
