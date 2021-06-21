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
    private float minMovementMultiplier;
    private float maxMovemnentMultiplier;
    private float m_MovementSpeedMultiplier;

    // The Rate of Withdrawl
    private AnimationCurve RateOfWithdrawal;

    // Timer for handling how long withdrawl lasts for
    private float WithdrawlTimeStart;
    private float WithdrawlTimeMax;
    private float WithdrawlTimer;
    private float WithdrawlCurrentTimer;

    // Camera Information
    private Camera camera;


    float DefaultVignette;
    Vignette vignette;
    Volume CameraVolume;
    VolumeProfile profile;

    // Constructor that sets all the variables
    public Withdrawls(float minMultiplier, float maxMultiplier, AnimationCurve Rate, float minTime, float maxTime, Camera playerCam)
    {
        minMovementMultiplier = minMultiplier;
        maxMovemnentMultiplier = maxMultiplier;
        RateOfWithdrawal = Rate;

        WithdrawlTimeStart = minTime;
        WithdrawlTimeMax = maxTime;

        m_MovementSpeedMultiplier = Mathf.Clamp(Rate.Evaluate(DrugPickedUp), minMovementMultiplier, maxMovemnentMultiplier);

        camera = playerCam;

        CameraVolume = camera.GetComponent<Volume>();
        profile = CameraVolume.profile;

        profile.TryGet<Vignette>(out vignette);

        DefaultVignette = vignette.intensity.value;

    }

    public void OnEnter()
    {
        WithdrawlTimer = Mathf.Clamp(DrugPickedUp * WithdrawlTimeStart, WithdrawlTimeStart, WithdrawlTimeMax);
        WithdrawlCurrentTimer = 0.0f;

        float IntensityValue = RateOfWithdrawal.Evaluate(DrugPickedUp);

        Debug.Log(IntensityValue);

        m_MovementSpeedMultiplier = Mathf.Clamp(RateOfWithdrawal.Evaluate(DrugPickedUp), minMovementMultiplier, maxMovemnentMultiplier);


        //vignette.intensity.value = 

        CameraVolume.sharedProfile = profile;
        Debug.Log("STATE: WITHDRAWL");
        //Debug.Log(DrugPickedUp);
    }

    public void OnExit()
    {
        
    }

    public void OnTick()
    {
        WithdrawlCurrentTimer += Time.deltaTime;
    }

    public float getMovementMultiplier()
    {
        return (m_MovementSpeedMultiplier );
    }

    // Returns true for when the timer is up
    // Used in the Transition
    public bool ReturnToSober()
    {
        if (WithdrawlCurrentTimer >= WithdrawlTimer)
        {
            return true;
        }
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
}
