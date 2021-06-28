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

    private GameObject ParticleEffect;


    ColorAdjustments colorAdjustments;
    ChromaticAberration chromatic;
    Volume CameraVolume;
    VolumeProfile profile;

    Light directionalLight;
    float DefaultIntensity;

    public int DrugPickedUp = 0;

    public High(Camera playerCam, Light DirectLight, GameObject particles, float SpeedMultiplier, float duration)
    {
        ParticleEffect = particles;
        directionalLight = DirectLight;
        DefaultIntensity = directionalLight.intensity;

        m_MovementSpeedMultiplier = SpeedMultiplier;
        m_Duration = duration;

        cam = playerCam;

        // The Camera Effects
        CameraVolume = cam.GetComponent<Volume>();
        profile = CameraVolume.profile;

        profile.TryGet<ChromaticAberration>(out chromatic);

        profile.TryGet<ColorAdjustments>(out colorAdjustments);
    }

    public void OnEnter()
    {
        ParticleEffect.SetActive(true);
        ParticleEffect.GetComponent<ParticleSystem>().Play();
        colorAdjustments.saturation.value = 60.0f;
        if (chromatic != null)
        {
            chromatic.intensity.overrideState = true;
        }
        T_DurationTimer = 0.0f;
        Debug.Log("STATE: HIGH");
    }

    public void OnExit()
    {
        ParticleEffect.SetActive(false);
        ParticleEffect.GetComponent<ParticleSystem>().Stop();
        colorAdjustments.saturation.value = 8.0f;
        chromatic.intensity.overrideState = false;
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

    public bool CanSprint()
    {
        return true;
    }
}
