using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  The Interface for the Different Stages of Drug Addiction.
///  A Basic implimentation of a State Machine Abstraction
/// </summary>
public interface IState
{
    // Gets Called on the enter of a State
    void OnEnter();
    // This is the Update() for the State
    void OnTick();
    // Gets Called on the exit of a State
    void OnExit();

    float getMovementMultiplier();

    void SetPickedUpValue(int Value);

    bool CanSprint();

    bool TransitionToSelf();
}
