using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Withdrawls : IState
{
    // The Number of Times High which is used to decide the severity of symptoms.
    public int DrugPickedUp;

    // The Movement Speed increase/Decrease that the Drug State Gives
    private float DefaultMovement;
    private float minMovementMultiplier;
    private float maxMovemnentMultiplier;
    private float m_MovementSpeedMultiplier;

    // The Rate of Withdrawl
    private AnimationCurve RateOfWithdrawal;

    private AnimationCurve VignetteCurve;

    // Camera Information
    private Camera camera;
    Volume CameraVolume;
    VolumeProfile profile;

    // Vignette
    float DefaultVignette;
    Vignette vignette;
    
    // Saturation

    // Directional Light
    Light DirectionalLight;
    float DefaultIntensity;

    // Constructor that sets all the variables
    public Withdrawls( AnimationCurve Rate, Camera playerCam, Light DirectionalLight, float defaultMovement, float minMove, float maxMove)      //float minMultiplier, float maxMultiplier, AnimationCurve Rate, float minTime, float maxTime, Camera playerCam)
    {
        // Apply The Movement Values
        DefaultMovement = defaultMovement;
        minMovementMultiplier = minMove;
        maxMovemnentMultiplier = maxMove;
        // Apply the Withdrawal Rate
        RateOfWithdrawal = Rate;
        // Store the Camera
        camera = playerCam;

        this.DirectionalLight = DirectionalLight;
        DefaultIntensity = DirectionalLight.intensity;

        // The Camera Effects
        CameraVolume = camera.GetComponent<Volume>();
        profile = CameraVolume.profile;
        profile.TryGet<Vignette>(out vignette);

        DefaultVignette = vignette.intensity.value;

        VignetteCurve = AnimationCurve.Linear(0.0f, DefaultVignette, 1.0f, 1.0f);
    }

    public void OnEnter()
    {
        float IntensityValue = RateOfWithdrawal.Evaluate(DrugPickedUp);

        Debug.Log(DrugPickedUp);
        m_MovementSpeedMultiplier = Mathf.Clamp((DefaultMovement * (1 - RateOfWithdrawal.Evaluate(DrugPickedUp))) / DefaultMovement, minMovementMultiplier, maxMovemnentMultiplier);


        DirectionalLight.intensity = Mathf.Clamp(DefaultIntensity * (1 - RateOfWithdrawal.Evaluate(DrugPickedUp)), 1000, DefaultIntensity);

        vignette.intensity.value = Mathf.Clamp(VignetteCurve.Evaluate(IntensityValue), DefaultVignette, 0.9f);
        
        CameraVolume.sharedProfile = profile;
        Debug.Log("STATE: WITHDRAWL");
    }

    public void OnExit()
    {
        vignette.intensity.value = DefaultVignette;
        DirectionalLight.intensity = DefaultIntensity; 
    }

    public void OnTick()
    {
    }

    public float getMovementMultiplier()
    {
        return (m_MovementSpeedMultiplier);
    }

    // A transition to return to sober. Currently not used.
    public bool ReturnToSober()
    {
        return false;
    }

    public bool TransitionToSelf()
    {
        return false;
    }

    public void SetPickedUpValue(int Value)
    {
        DrugPickedUp = Value;
    }

    public bool CanSprint()
    {
        return false;
    }
}
