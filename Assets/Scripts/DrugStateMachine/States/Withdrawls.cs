using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Withdrawls : IState
{
    // The Number of Times High which is used to decide the severity of symptoms.
    public int TimesHigh;

    // The Movement Speed increase/Decrease that the Drug State Gives
    private float minMovementMultiplier;
    private float maxMovemnentMultiplier;
    private float m_MovementSpeedMultiplier;

    // The Rate of Withdrawl
    private AnimationCurve RateOfWithdrawl;

    // Timer for handling how long withdrawl lasts for
    private float WithdrawlTimeStart;
    private float WithdrawlTimeMax;
    private float WithdrawlTimer;
    private float WithdrawlCurrentTimer;

    // Constructor that sets all the variables
    public Withdrawls(float minMultiplier, float maxMultiplier, AnimationCurve Rate, float minTime, float maxTime)
    {
        minMovementMultiplier = minMultiplier;
        maxMovemnentMultiplier = maxMultiplier;
        RateOfWithdrawl = Rate;

        WithdrawlTimeStart = minTime;
        WithdrawlTimeMax = maxTime;

        m_MovementSpeedMultiplier = Mathf.Clamp(Rate.Evaluate(TimesHigh), minMovementMultiplier, maxMovemnentMultiplier);
    }

    public void OnEnter()
    {
        TimesHigh += 1;

        WithdrawlTimer = Mathf.Clamp(TimesHigh * WithdrawlTimeStart, WithdrawlTimeStart, WithdrawlTimeMax);
        WithdrawlCurrentTimer = 0.0f;

        m_MovementSpeedMultiplier = Mathf.Clamp(RateOfWithdrawl.Evaluate(TimesHigh), minMovementMultiplier, maxMovemnentMultiplier);
        Debug.Log("STATE: WITHDRAWL");
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
}
