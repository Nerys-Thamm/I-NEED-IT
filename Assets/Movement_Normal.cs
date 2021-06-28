using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_Normal : MonoBehaviour
{
    public Animator m_Anim;
    public AudioSource Audio;
    public AudioSource srcAudio;
    public AudioClip DeathAudio;
    public AudioClip PickupAudio;
    
   

    public GameObject m_CharModel;
    public CharacterController m_Controller;
    public float m_MoveSpeed;
    public float m_JumpForce;
    public float m_GravityForce;
    public float m_JumpDuration;
    public AnimationCurve m_JumpCurve;
    public AnimationCurve m_GravityCurve;
    float m_CurrentJumpDuration = 0;
    float m_ModifiedMoveSpeed;
    float m_SprintInput;

    
    [Range(1, 1000)]
    public float speed_mult;

    [Range(40, 100)]
    public float camera_rotate_sensitivity;

    public bool CanMove = false;

    [Header("Camera Settings:")]
    public Camera Cam;
    public Transform Camera;
    public Transform FollowCam;

    [Header("Respawning:")]
    public bool HasDied = false;
    public GameObject DeathPrefab;
    public Transform RespawnLocation;

    Vector2 moveVal;
    Vector3 direction;
    float cameraangledelta;

    // DRUG STATE MACHINE SETUP
    DrugStateMachine StateMachine;

    Sober m_soberState;
    High m_highState;
    Withdrawls m_withdrawlState;

    [Header("Drug State Machine Setup", order = 0)]
    public PersistentSceneData PersistentData;
    public GameObject PersistencePrefab;
    [Header("Sober Setup: ", order = 1)] 
    public float SoberMovementMultiplier = 1.0f;
    [Header("High Setup: ")]
    public float HighMovemnetMultiplier = 2.0f;
    public float HighDuration = 10.0f;
    public GameObject HighParticles;
<<<<<<< Updated upstream
=======
    public AudioClip HighAudio;
>>>>>>> Stashed changes
    [Header("Withdrawl Setup: ")]
    public float WithdrawlMinMovement = 0.5f;
    public float WithdrawlMaxMovement = 1.0f;
    public AnimationCurve WithdrawlRate = AnimationCurve.Linear(0.0f, 1.0f, 10.0f, 0.5f);
    public Light DirectionalLight;
    public AudioClip WithdrawalAudio;

    [Header("Pick Up Drug [Testing Purposes]")]
    public bool pickedUp = false;
    bool forceEndDrug = false;
    public DrugManager drugManager;
    public int DrugsPickedUp;
    public GameObject PickupParticle;
    public GameObject PickupPartcileLocation;

    [Header("Narration Audio")]
    public AudioClip FirstPickupAudio;
    public bool FirstPickup;


    public bool AnimationStartOveride;

    // Awake to Setup the State Machine
    private void Awake()
    {
        PersistentData = FindObjectOfType<PersistentSceneData>();

        if (PersistentData == null)
        {
            GameObject obj = Instantiate(PersistencePrefab);
            PersistentData = obj.GetComponent<PersistentSceneData>();
            Debug.LogWarning("NO PERSISTENT DATA FOUND");
        }

        FirstPickup = true;

        HighParticles.SetActive(false);

        Cam = UnityEngine.Camera.main;

        // Creates the State Machine
        StateMachine = new DrugStateMachine();

        // Creates the States and stores the variable information
        m_soberState = new Sober(SoberMovementMultiplier);
<<<<<<< Updated upstream
        m_highState = new High(Cam, DirectionalLight, HighParticles, HighMovemnetMultiplier, HighDuration);
=======
        m_highState = new High(Cam, DirectionalLight, HighParticles, srcAudio, HighAudio, HighMovemnetMultiplier, HighDuration);
>>>>>>> Stashed changes
        m_withdrawlState = new Withdrawls(WithdrawlRate, Cam, DirectionalLight, PersistentData, srcAudio, WithdrawalAudio, speed_mult, WithdrawlMinMovement, WithdrawlMaxMovement);

        // Add the Transition From High to Withdrawl
        StateMachine.AddTransition(m_highState, m_withdrawlState, () => m_highState.NoLongerHigh());
        // Add the Transition From Withdrawl to Sober
        StateMachine.AddTransition(m_highState, m_withdrawlState, () => ForceDisableDrug());
        //StateMachine.AddTransition(m_withdrawlState, m_soberState, () => m_withdrawlState.ReturnToSober());

        // Add the Transition From Any State to High [At the moment, it won't go from High to High again upon the Pickup of a new Powerup]
        StateMachine.AddAnyTransition(m_highState, () => EnableHigh());
        // Set the Start State to Sober

        if (PersistentData.GetDrugs() == 0)
        {
            StateMachine.SetState(m_soberState);
        }
        else
        {
            StateMachine.SetState(m_withdrawlState);
        }


        m_Anim.SetBool("IsGrounded", true);

        
    }


    void OnMove(InputValue value)
    {
        moveVal = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        m_SprintInput = value.Get<float>();
    }

    void OnJump()
    {
        if (m_Controller.isGrounded && CanMove)
        {
            m_CurrentJumpDuration = m_JumpDuration;
            m_Anim.SetTrigger("OnJump");
        }
        
    }

    void OnRotateCamera(InputValue value)
    {
        cameraangledelta = value.Get<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AnimationStartOveride)
        {
            m_Anim.SetTrigger("AnimOverride");
            CanMove = true;
            AnimationStartOveride = false;
        }

        m_CurrentJumpDuration -= Time.deltaTime;
        StateMachine.Tick();
        if(StateMachine.CanSprint())
        {
            m_ModifiedMoveSpeed = m_MoveSpeed + (StateMachine.SpeedMultiplier()*m_SprintInput);
        }
        else
        {
            m_ModifiedMoveSpeed = m_MoveSpeed + StateMachine.SpeedMultiplier();
        }

        FollowCam.Rotate(new Vector3(0, cameraangledelta * Time.deltaTime * camera_rotate_sensitivity, 0));

        if (CanMove)
        {

            var forward = Camera.forward;
            var right = Camera.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            direction = Vector3.Lerp(direction, (forward * moveVal.y + right * moveVal.x), Time.deltaTime * 5.0f);
            direction = direction.normalized * (forward * moveVal.y + right * moveVal.x).magnitude;
            Vector3 dirwithvertical = direction * m_ModifiedMoveSpeed;
            dirwithvertical.y = (m_GravityCurve.Evaluate(m_CurrentJumpDuration / m_JumpDuration) * -m_GravityForce) + (m_JumpCurve.Evaluate(m_CurrentJumpDuration / m_JumpDuration) * m_JumpForce);
            //Debug.Log(speed_mult + " : " + StateMachine.SpeedMultiplier());

            //body.AddForce(direction * Time.deltaTime * (speed_mult * StateMachine.SpeedMultiplier()), ForceMode.Force);
            m_Controller.Move(dirwithvertical * Time.deltaTime);
            if (moveVal.magnitude > 0)
            {
                m_CharModel.transform.rotation = Quaternion.RotateTowards(m_CharModel.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * 300);
            }

            

            if (direction.magnitude > 0.06 && m_Controller.isGrounded)
            {
                m_Anim.speed = (m_ModifiedMoveSpeed / 2.6f) * direction.magnitude;
            }
            else
            {
                m_Anim.speed = 1.0f;
            }
            m_Anim.SetBool("IsWalking", moveVal.magnitude > 0);
            m_Anim.SetBool("IsRunning", (m_ModifiedMoveSpeed > m_MoveSpeed) && (moveVal.magnitude > 0) && StateMachine.CanSprint());
            m_Anim.SetBool("IsGrounded", m_Controller.isGrounded);

            if (HasDied)
            {
                Die();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + (direction * speed_mult));
    }
        
    public void CollectDrug(Transform drug)
    {
        Vector3 lookPos = drug.position - transform.position;
        lookPos.y = 0;
        m_CharModel.transform.rotation = Quaternion.LookRotation(lookPos);
        CanMove = false;
        m_Anim.speed = 1.0f;
        m_Anim.SetTrigger("OnDrugPickup");
        GameObject obj = Instantiate(PickupParticle, PickupPartcileLocation.transform.position, Quaternion.identity);
        //obj.transform.SetParent(PickupPartcileLocation.transform);
        //Audio.PlayOneShot(PickupAudio);
    }

    // Picked Up Drug function used as the Predicate for the State Machine Transition 
    public bool EnableHigh()
    {
        if (pickedUp)
        {
            PersistentData.IncreasePickedup();
            DrugsPickedUp++;
            StateMachine.PickedUp();
            pickedUp = false;

            if (FirstPickup)
            {
                if (FirstPickupAudio != null)
                {
                    Audio.volume = 1.0f;
                    Audio.clip = FirstPickupAudio;
                    Audio.PlayDelayed(1.5f);
                    
                }
                FirstPickup = false;
            }

            return true;
        }
        return false;
    }

    public bool ForceDisableDrug()
    {
        if (forceEndDrug)
        {
            forceEndDrug = !forceEndDrug;
            return true;
        }
        else return false;
    }

    public void Die()
    {
        forceEndDrug = true;
        CanMove = false;
        HasDied = true;
        m_CharModel.SetActive(false);
        this.GetComponent<CapsuleCollider>().enabled = false;
        m_Controller.enabled = false;
        GameObject obj = Instantiate(DeathPrefab, transform.position, m_CharModel.transform.rotation);
        srcAudio.PlayOneShot(DeathAudio);

        StartCoroutine(DelayRespawn(5.0f, obj));
    }


    IEnumerator DelayRespawn(float time, GameObject DeathPrefab)
    {
        yield return new WaitForSeconds(time);

        drugManager.RespawnDrugs();

        transform.position = RespawnLocation.position;
        transform.rotation = Quaternion.identity;

        this.GetComponent<CapsuleCollider>().enabled = true;
        m_Controller.enabled = true;
        Object.Destroy(DeathPrefab);
        m_CharModel.SetActive(true);

        HasDied = false;
        CanMove = true;
    }

}
