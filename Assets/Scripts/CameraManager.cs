using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

// Sets all the Camera Effects to default at the start of the game
public class CameraManager : MonoBehaviour
{
    public Camera Camera;

    public Volume CameraVolume;

    [Header("Camera Volumetric Settings:")]
    VolumeProfile OriginalProfile;

    // Start is called before the first frame update
    void Start()
    {
        Camera = Camera.main;
        CameraVolume = Camera.GetComponent<Volume>();

        OriginalProfile = CameraVolume.profile;
    }


    private void OnDisable()
    {
        CameraVolume.sharedProfile = OriginalProfile;
        //vignette.intensity.value = VignetteIntensity;
    }
}
