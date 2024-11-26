using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip clickSoundParchment; 
    public AudioClip clickSoundHome; 


    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void PlayClickSoundParchment()
    {
        if (audioSource != null && clickSoundParchment != null)
        {
            audioSource.PlayOneShot(clickSoundParchment);
        }
    }

    public void PlayClickSoundHome()
    {
        if (audioSource != null && clickSoundHome != null)
        {
            audioSource.PlayOneShot(clickSoundHome); 
        }
    }


}
