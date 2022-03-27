using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager instance = null;

    public AudioClip startSound;
    public AudioClip closeSound;
    public AudioClip crashSound;
    public AudioClip collectibleSound;
    public AudioClip finishSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayStartSound()
    {
        Play(startSound);
    }

    public void PlayCloseSound()
    {
        Play(closeSound);
    }

    public void PlayCrashSound()
    {
        Play(crashSound);
    }

    public void PlayCollectibleSound()
    {
        Play(collectibleSound);
    }

    public void PlayFinishSound()
    {
        Play(finishSound);
    }

    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public static void PlayStatic(AudioClip clip)
    {
        instance.Play(clip);
    }

    public static void PlayStartStatic()
    {
        instance.PlayStartSound();
    }

    public static void PlayCloseStatic()
    {
        instance.PlayCloseSound();
    }

    public static void PlayCrashStatic()
    {
        instance.PlayCrashSound();
    }

    public static void PlayCollectibleStatic()
    {
        instance.PlayCollectibleSound();
    }
    public static void PlayFinishStatic()
    {
        instance.PlayFinishSound();
    }
}
