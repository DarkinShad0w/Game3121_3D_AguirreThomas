using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Clips")]
    public AudioClip wallHit;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip backgroundMusic;

    [Header("Music")]
    [Range(0f, 1f)]
    public float musicVolume = 0.6f;

    private AudioSource audioSource;
    private AudioSource musicSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.playOnAwake = false;
    }

    void Start()
    {
        musicSource.Play();
    }

    public void PlayWallHit (float volume)
    {
        audioSource.PlayOneShot(wallHit, volume);
    }

    public void PlayWin()
    {
        audioSource.PlayOneShot(win);
    }

    public void PlayLose()
    {
        audioSource.PlayOneShot(lose);
    }
}
