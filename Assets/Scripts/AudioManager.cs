using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource, SFXSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip background;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ChangeBackground(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Stop();
        musicSource.Play();
    }

    public void StopAll()
    {
        musicSource.Stop();
        SFXSource.Stop();
    }
}
