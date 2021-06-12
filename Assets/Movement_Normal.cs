using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_Normal : MonoBehaviour
{
    public Rigidbody body;
    [Range(1, 1000)]
    public float speed_mult;

    [Range(40, 100)]
    public float camera_rotate_sensitivity;

    public Transform Camera;
    public Transform FollowCam;

    Vector2 moveVal;
    Vector3 direction;
    float cameraangledelta;

    // DRUG STATE MACHINE SETUP
    DrugStateMachine StateMachine;

    Sober m_soberState;
    High m_highState;
    Withdrawls m_withdrawlState;

    [Header("Drug State Machine Setup", order = 0)]
    [Header("Sober Setup: ", order = 1)] 
    public float SoberMovementMultiplier = 1.0f;
    [Header("High Setup: ")]
    public float HighMovemnetMultiplier = 2.0f;
    public float HighDuration = 10.0f;
    [Header("Withdrawl Setup: ")]
    public float WithdrawlMinMovement = 0.5f;
    public float WithdrawlMaxMovement = 1.0f;
    public AnimationCurve WithdrawlRate = AnimationCurve.Linear(0.0f, 1.0f, 10.0f, 0.5f);
    public float WithdrawlTimeStartLength = 0.5f;
    public float WithdrawlTimeMaxLength = 5.0f;


    [Header("Pick Up Drug [Testing Purposes]")]
    public bool pickedUp = false;

    // Awake to Setup the State Machine
    private void Awake()
    {
        // Creates the State Machine
        StateMachine = new DrugStateMachine();

        // Creates the States and stores the variable information
        m_soberState = new Sober(SoberMovementMultiplier);
        m_highState = new High(HighMovemnetMultiplier, HighDuration);
        m_withdrawlState = new Withdrawls(WithdrawlMinMovement, WithdrawlMaxMovement, WithdrawlRate, WithdrawlTimeStartLength, WithdrawlTimeMaxLength);

        // Add the Transition From High to Withdrawl
        StateMachine.AddTransition(m_highState, m_withdrawlState, () => m_highState.NoLongerHigh());
        // Add the Transition From Withdrawl to Sober
        StateMachine.AddTransition(m_withdrawlState, m_soberState, () => m_withdrawlState.ReturnToSober());

        // Add the Transition From Any State to High [At the moment, it won't go from High to High again upon the Pickup of a new Powerup]
        StateMachine.AddAnyTransition(m_highState, () => PickupDrug());
        // Set the Start State to Sober
        StateMachine.SetState(m_soberState);
    }


    void OnMove(InputValue value)
    {
        moveVal = value.Get<Vector2>();
    }

    void OnJump()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
    }

    void OnRotateCamera(InputValue value)
    {
        cameraangledelta = value.Get<float>();
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Tick();

        var forward = Camera.forward;
        var right = Camera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        direction = forward * moveVal.y + right * moveVal.x;

        Debug.Log(speed_mult + " : " + StateMachine.SpeedMultiplier());
        //transform.Translate(direction * Time.deltaTime * speed_mult);
        body.AddForce(direction * Time.deltaTime * (speed_mult * StateMachine.SpeedMultiplier()), ForceMode.Force);
        FollowCam.Rotate(new Vector3(0, cameraangledelta * Time.deltaTime * camera_rotate_sensitivity, 0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + (direction * speed_mult));
    }
        
    // Picked Up Drug function used as the Predicate for the State Machine Transition 
    public bool PickupDrug()
    {
        if (pickedUp)
        {
            pickedUp = false;
            return true;
        }
        return false;
    }

}
