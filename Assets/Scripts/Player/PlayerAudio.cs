using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip ac_FootstepClip;
    public AudioClip ac_LandingClip;
    public AudioClip ac_JumpingClip;

    public void PlayFootstep()
    {
        audioSource.PlayOneShot(ac_FootstepClip);
    }

    public void PlayLanding()
    {
        audioSource.PlayOneShot(ac_LandingClip);
    }

    public void PlayJumping()
    {
        audioSource.PlayOneShot(ac_JumpingClip);
    }
}
