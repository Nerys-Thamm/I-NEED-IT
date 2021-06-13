﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class High : IState
{
    // The Movement Speed increase/Decrease that the Drug State Gives
    private float m_MovementSpeedMultiplier;

    // The Length for the High period to last
    private float m_Duration;
    // The Timer for being high
    private float T_DurationTimer;

    public High(float SpeedMultiplier, float duration)
    {
        m_MovementSpeedMultiplier = SpeedMultiplier;
        m_Duration = duration;
    }

    public void OnEnter()
    {
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

}