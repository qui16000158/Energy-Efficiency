using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource playerAudioSource;

    public AudioClip buttonSoundOne;

    public AudioClip buttonSoundTwo;


    public void PlayButtonSound1()
    {
        playerAudioSource.PlayOneShot(buttonSoundOne);
    }

    public void PlayButtonSound2()
    {
        playerAudioSource.PlayOneShot(buttonSoundTwo);
    }
}
