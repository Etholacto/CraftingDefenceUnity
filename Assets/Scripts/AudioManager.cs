using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource, SFXSource; 

    [Header("Audio Clips")]
    [SerializeField] private AudioClip background;
}
