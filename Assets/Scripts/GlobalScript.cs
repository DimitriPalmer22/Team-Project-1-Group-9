using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance { get; private set; }

    private static GameObject _player;
    public static GameObject Player => _player;

    [SerializeField] public WinLossManager winLossManager;

    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip winMusic;
    [SerializeField] private AudioClip loseMusic;

    private AudioSource _audioSource;
    
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

}
