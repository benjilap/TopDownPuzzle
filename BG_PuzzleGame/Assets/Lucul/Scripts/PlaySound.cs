using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public AudioClip audioClip;
    public AudioSource audioSource;

    void DoorSound()
    {
        audioSource.PlayOneShot(audioClip);

    }
}