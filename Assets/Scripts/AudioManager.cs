using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxButton;  // ← tambah
    [SerializeField] private AudioClip sfxGameOver;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlayJump()
    {
        sfxSource.PlayOneShot(sfxJump, 2f);
    }

    public void PlayButton()
    {
        sfxSource.PlayOneShot(sfxButton);
    }

    public void PlayGameOver()
    {
        sfxSource.PlayOneShot(sfxGameOver);
    }
}