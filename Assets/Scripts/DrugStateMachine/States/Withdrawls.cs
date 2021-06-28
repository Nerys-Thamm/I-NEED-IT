using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Withdrawls : IState
{
    // The Number of Times High which is used to decide the severity of symptoms.
    public int DrugPickedUp;
    PersistentSceneData PersistentData;
    public AudioSource Audio;
    public AudioSource Audio2;
    public AudioClip WithdrawalAudio;
    public AudioClip FirstPickup;

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

    bool DebugAudio;

    float Timer = 1.5f;
    float time = 0.0f;

    // Constructor that sets all the variables
    public Withdrawls(AnimationCurve Rate, Camera playerCam, Light DirectionalLight, PersistentSceneData TimesPickedup, AudioSource source, AudioClip clip, AudioSource source2, AudioClip Clip2, float defaultMovement, float minMove, float maxMove)      //float minMultiplier, float maxMultiplier, AnimationCurve Rate, float minTime, float maxTime, Camera playerCam)
    {
        PersistentData = TimesPickedup;
        DrugPickedUp = TimesPickedup.GetDrugs();

        DebugAudio = DrugPickedUp == 0 ? true : false;

        Audio = source;
        Audio2 = source2;
        WithdrawalAudio = clip;
        FirstPickup = Clip2;

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

        VignetteCurve = AnimationCurve.Linear(0.0f, DefaultVignette, 1.0f, 0.6f);
    }

    public void OnEnter()
    {
        Debug.LogError(DebugAudio);
        
        DrugPickedUp = PersistentData.GetDrugs();
        float IntensityValue = RateOfWithdrawal.Evaluate(DrugPickedUp);

        Audio.volume =  Mathf.Clamp(IntensityValue, 0, 0.6f);
        Audio.loop = true;
        Audio.clip = WithdrawalAudio;
        Audio.Play();

        Debug.Log(DrugPickedUp);
        m_MovementSpeedMultiplier = Mathf.Clamp((1 - RateOfWithdrawal.Evaluate(DrugPickedUp) / 2), minMovementMultiplier, maxMovemnentMultiplier);


        DirectionalLight.intensity = Mathf.Clamp(DefaultIntensity * (1 - RateOfWithdrawal.Evaluate(DrugPickedUp)), 1000, DefaultIntensity);

        vignette.intensity.value = Mathf.Clamp(VignetteCurve.Evaluate(IntensityValue), DefaultVignette, 0.6f);
        
        CameraVolume.sharedProfile = profile;
        Debug.Log("STATE: WITHDRAWL");
    }

    public void OnExit()
    {
        Audio.loop = false;
        Audio.volume = 0.181f;
        Audio.Stop();
        vignette.intensity.value = DefaultVignette;
        DirectionalLight.intensity = DefaultIntensity; 
    }

    public void OnTick()
    {
        if (DebugAudio)
        {
            time += Time.deltaTime;
            if (time > Timer)
            {
                Debug.Log("rawrxd");
                Audio2.volume = 1.0f;
                Audio2.PlayOneShot(FirstPickup, 1.0f);
                DebugAudio = false;
            }
        }
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
