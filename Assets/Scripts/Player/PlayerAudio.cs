using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource as_Footstep;
    public AudioClip ac_FootstepClip;

    public void PlayFootstep()
    {
        as_Footstep.PlayOneShot(ac_FootstepClip);
    }
}
