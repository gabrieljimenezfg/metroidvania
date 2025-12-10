using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Play()
    {
        audioSource.Play();
    }
}