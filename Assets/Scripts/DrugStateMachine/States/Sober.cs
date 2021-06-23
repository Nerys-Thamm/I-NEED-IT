using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sober : IState
{
    // The Movement Speed increase/Decrease that the Drug State Gives
    private float m_MovementSpeedMultiplier;

    private int DrugPickedUp = 0;

    public Sober(float SpeedMultiplier)
    {
        m_MovementSpeedMultiplier = SpeedMultiplier;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void OnTick()
    {

    }

    public float getMovementMultiplier()
    {
        return (m_MovementSpeedMultiplier);
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
