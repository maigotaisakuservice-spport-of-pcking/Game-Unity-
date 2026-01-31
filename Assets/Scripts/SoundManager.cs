using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip footsteps;
    public AudioClip busSound;
    public AudioClip lightFlicker;
    public AudioClip jkChaseStartSound; // 女子高生の追跡開始音
    // 他のサウンドクリップを追加

    private void Awake()
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

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // --- Specific Sound Players ---
    public void PlayFootsteps()
    {
        PlaySFX(footsteps);
    }

    public void PlayBusSound()
    {
        PlaySFX(busSound);
    }

    public void PlayJkChaseSound()
    {
        PlaySFX(jkChaseStartSound);
    }
}
