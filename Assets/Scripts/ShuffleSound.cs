using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shuffle;

    public void playShuffle()
    {
        audioSource.PlayOneShot(shuffle, 1);
    }
}
