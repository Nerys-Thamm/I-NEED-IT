using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugStateMachine
{
    // The Current State the player is in.
    public IState m_CurrentDrugState;

    // A dictionary containing all different States and every Transition from that State
    private Dictionary<System.Type, List<Transition>> dictionaryTransitions = new Dictionary<System.Type, List<Transition>>();

    // A List containing all the Transitions for the Current State
    private List<Transition> listCurrentTransitions = new List<Transition>();

    // A List Containing all the Transitions from Any State.
    private List<Transition> listAnyTransitions = new List<Transition>();

    // An Empty List of Transitions used if there is no available transitions
    private static List<Transition> listEmptyTransitions = new List<Transition>();

    public int TimesPickedUp = 0;

    // The Tick Function for the State
    // Checks if there is a transition available and if so, does the transition
    // Otherwise does the Tick function for the State
    public void Tick()
    {
        DrugStateMachine.Transition transition = GetTransition();

        if (transition != null)
        {
            SetState(transition.To);
        }

        // Uses the Null-Conditional Operator "?." to not cause issues if the state does not have a Defined OnTick()
        m_CurrentDrugState?.OnTick();
    }

    // Set the New State
    // Calls the On Exit of current state before setting new state and calling on enter
    // Sets the list of transitions to be the new states transitions
    public void SetState(IState state)
    {
        // If the current State is the one we are trying to set to,
        // Just Return instead of resetting the state
        // Checks if the state is allowed to transition to itself.
        if (state == m_CurrentDrugState && !state.TransitionToSelf())
        {
            return;
        }

        // Uses the Null-Conditional Operator "?." to not cause issues if the state does not have a Defined OnExit()
        m_CurrentDrugState?.OnExit();
        m_CurrentDrugState = state;

        m_CurrentDrugState.SetPickedUpValue(TimesPickedUp);
        // Get the Transitions for this state from the Dictionary and store it in the current transitions
        dictionaryTransitions.TryGetValue(m_CurrentDrugState.GetType(), out listCurrentTransitions);
        // Null Check to ensure there is no issues
        if (listCurrentTransitions == null)
        {
            listCurrentTransitions = listEmptyTransitions;
        }

        // Uses the Null-Conditional Operator "?." to not cause issues if the state does not have a Defined OnEnter()
        m_CurrentDrugState?.OnEnter();
    }

    public float SpeedMultiplier()
    {
        return m_CurrentDrugState.getMovementMultiplier();
    }

    public void PickedUp()
    {
        TimesPickedUp += 1;
    }

    public bool CanSprint()
    {
        return m_CurrentDrugState.CanSprint();
    }

    // Add a new State from One State to Another using a Predicate Function
    public void AddTransition(IState from, IState to, System.Func<bool> predicate)
    {
        // Checks if the Current State has any transitions in the dictionary,
        // And if not, creates a new list and stores it in the dictionary
        if (dictionaryTransitions.TryGetValue(from.GetType(), out List<Transition> transitions) == false)
        {
            transitions = new List<Transition>();
            dictionaryTransitions[from.GetType()] = transitions;
        }

        // Adds the New Transiton to the List for the current state
        transitions.Add(new Transition(to, predicate));
    }


    // Add a new State that goes from Any State to the Specified State
    public void AddAnyTransition(IState state, System.Func<bool> predicate)
    {
        // Add the State to the List of Any Transitions.
        listAnyTransitions.Add(new Transition(state, predicate));
    }

    /// <summary>
    /// A Class to handle the Transitions from one state to another
    /// </summary>
    private class Transition
    {
        // A function to get the "CONDITION" for the Transition
        public System.Func<bool> Condition { get; }
        // A Function to get the "TO" state oof the Trandition
        public IState To { get; }
        // Constructor for the Transition.
        // "IState to" Is the state to Transition to
        // "System.Func<bool> condition" is the Condition for the Transition
        public Transition(IState to, System.Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    // Returns a Transition if it is valid.
    private Transition GetTransition()
    {
        // Go through the list of Any Transitions first and Return if the transition is valid
        foreach (Transition transition in listAnyTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        // Go through the list the Current State's Transitions and return if the transition is valid
        foreach (Transition transition in listCurrentTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        // Otherwise Return Null.
        // It handles the NULL out of this function
        return null;
    }
}
