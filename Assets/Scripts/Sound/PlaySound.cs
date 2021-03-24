using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource playerAudioSource;

    public AudioClip buttonSoundOne;

    public AudioClip buttonSoundTwo;

    public AudioClip buttonSoundThree;

    public AudioClip monitorSound;

    public AudioClip movePointSound;


    public void PlayButtonSound1()
    {
        playerAudioSource.PlayOneShot(buttonSoundOne);
    }

    public void PlayButtonSound2()
    {
        playerAudioSource.PlayOneShot(buttonSoundTwo);
    }

    public void PlayMonitorSound()
    {
        playerAudioSource.PlayOneShot(monitorSound);
    }

    public void PlayButtonSound3()
    {
        playerAudioSource.PlayOneShot(buttonSoundThree);
    }

    public void MovePointSound()
    {
        playerAudioSource.PlayOneShot(movePointSound);
    }
}
