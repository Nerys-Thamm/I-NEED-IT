using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class High : IState
{
    // The Movement Speed increase/Decrease that the Drug State Gives
    private float m_MovementSpeedMultiplier;

    // The Length for the High period to last
    private float m_Duration;
    // The Timer for being high
    private float T_DurationTimer;

    private Camera cam;


    float DefaultVignette;
    Vignette vignette;
    Volume CameraVolume;
    VolumeProfile profile;

    Light directionalLight;
    float DefaultIntensity;

    public int DrugPickedUp = 0;

    public High(Camera playerCam, Light DirectLight, float SpeedMultiplier, float duration)
    {
        directionalLight = DirectLight;
        DefaultIntensity = directionalLight.intensity;

        m_MovementSpeedMultiplier = SpeedMultiplier;
        m_Duration = duration;

        cam = playerCam;

        // The Camera Effects
        CameraVolume = cam.GetComponent<Volume>();
        profile = CameraVolume.profile;
        profile.TryGet<Vignette>(out vignette);

        DefaultVignette = vignette.intensity.value;
    }

    public void OnEnter()
    {
        directionalLight.intensity = DefaultIntensity;
        vignette.intensity.value = DefaultVignette;
        T_DurationTimer = 0.0f;
        Debug.Log("STATE: HIGH");
    }

    public void OnExit()
    {
        
    }

    public void OnTick()
    {
        // Increase the Duration Timer
        T_DurationTimer += Time.deltaTime;
    }


    public float getMovementMultiplier()
    {
        return (m_MovementSpeedMultiplier);
    }



    // Returns true for when the timer is up
    // Used in the Transition
    public bool NoLongerHigh()
    {
        if (T_DurationTimer >= m_Duration)
        {
            return true;
        }
        return false;
    }

    public bool TransitionToSelf()
    {
        return true;
    }
    public void SetPickedUpValue(int Value)
    {
        DrugPickedUp = Value;
    }
}
