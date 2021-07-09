using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChange : MonoBehaviour
{

    public AudioSource Source;
    public AudioClip TrackToSwapTo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Source.clip = TrackToSwapTo;
            Source.Play();
        }
    }
}
