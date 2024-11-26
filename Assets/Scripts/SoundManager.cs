using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private bool isSoundOn = true;

    public void TurnOnSound()
    {
        isSoundOn = true;
        AudioListener.volume = 1f;
    }

    public void TurnOffSound()
    {
        isSoundOn = false;
        AudioListener.volume = 0f;
    }

    public bool IsSoundOn()
    {
        return isSoundOn;
    }
}

