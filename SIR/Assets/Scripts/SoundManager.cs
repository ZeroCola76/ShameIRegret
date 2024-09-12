using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 싱글톤으로 된 사운드매니저
/// </summary>
public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<SoundManager>();
                singletonObject.name = typeof(SoundManager).ToString() + " (Singleton)";
                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    public AudioSource musicSource;
    public AudioSource effectsSource;
    public AudioClip[] musicClips;
    public AudioClip[] songsClips;
    public AudioClip[] effectClips;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        AudioSource[] audioSources = GetComponents<AudioSource>();

        if (audioSources.Length >= 2)
        {
            musicSource = audioSources[0];
            effectsSource = audioSources[1];
        }
    }

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
    }

    public void PlaySong(int index)
    {
        if (index >= 0 && index < songsClips.Length)
        {
            musicSource.clip = songsClips[index];
            musicSource.Play();
        }
    }

    public void PlayEffect(int index)
    {
        if (index >= 0 && index < effectClips.Length)
        {
            effectsSource.PlayOneShot(effectClips[index]);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }


    public void StopEffects()
    {
        effectsSource.Stop();
    }
}
