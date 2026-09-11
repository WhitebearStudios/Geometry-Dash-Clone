using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Static instance allows other scripts to access this easily
    public static AudioManager instance;

    [Header("---- Audio Sources ----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("---- Audio Clips ----")]
    public AudioClip backgroundMusic;
    public AudioClip dieSFX;
    public AudioClip levelCompleteSFX;

    private void Awake()
    {
        // Singleton pattern to ensure only one manager exists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Play background music automatically on start
        musicSource.clip = backgroundMusic;
        musicSource.loop = false;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void RestartMusic()
    {
        musicSource.Play();
    }

    // Public method to trigger sound effects from any other script
    public void PlaySFX(AudioClip clip)
    {
        // PlayOneShot allows multiple SFX to overlap without cutting each other off
        sfxSource.PlayOneShot(clip);
    }
}