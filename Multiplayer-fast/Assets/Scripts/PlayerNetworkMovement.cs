using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;
using System.Net.NetworkInformation;

public class PlayerNetworkMovement : NetworkBehaviour
{
    [SerializeField, HideInInspector] private float HorizontalInput;
    [SerializeField, HideInInspector] private float VerticalInput;

    [Header("Movement")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float SprintSpeed;
    [SerializeField] private float GroundDrag;

    [Header("Jump")]
    [SerializeField] private float JumpForce;
    [SerializeField] private float AirMultiplier;
    [SerializeField] private float JumpResetCooldown;
    [SerializeField] private bool JumpResat;

    [Header("Ground check")]
    [SerializeField] private float PlayerHeight;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private bool IsGrounded;

    [Header("Movement States")]
    [SerializeField] private MovementState state;

    [Header("Camera Rotation")]
    [SerializeField] private Transform OriginalCamTransform;
    [SerializeField] private Transform CamHolder;
    [SerializeField] private float OriginalFOV;
    [SerializeField] private float CameraRotationAmoutHorizontal;
    [SerializeField] private float CameraRotationAmoutVertical;
    [SerializeField] private float CameraFOVAmout;
    [SerializeField] private PlayerNetworkCameraLook pncl;

    [Header("Swinging")]
    [SerializeField] private float SwingingSpeed;
    [SerializeField] public bool IsSwinging;
    

    [Header("Refrences")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField, HideInInspector] public bool freeze;
    [SerializeField, HideInInspector] public bool activeGrappel;
    enum MovementState
    {
        Freeze,
        Walk,
        Sprint,
        Air,
        Swinging,
        Crouch
    }
    private void Start()
    {
        transform.position = new Vector3(0, 2, 0);
        JumpResat = true;
        rb= GetComponent<Rigidbody>();
        rb.freezeRotation= true;
        MoveSpeed = 7;
        OriginalFOV = cam.fieldOfView;
        OriginalCamTransform.rotation = cam.transform.rotation;
        if (IsLocalPlayer) return;
        cam.enabled = false;



    }
    private void Update()
    {
        if (!IsOwner) { return; }
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, WhatIsGround);
        if (IsGrounded && !activeGrappel)
            rb.drag = GroundDrag;
        else
            rb.drag = 0f;
        MyInputs();
        SpeedControl();
        StateHandler();
        
    }

    void StateHandler()
    {
        if(freeze)
        {
            state = MovementState.Freeze;
            MoveSpeed = 0;
            rb.velocity = Vector3.zero;
        }
        else if (IsSwinging)
        {
            state = MovementState.Swinging;
            MoveSpeed = SwingingSpeed;
        }
        else if(IsGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            state = MovementState.Sprint;
            MoveSpeed = SprintSpeed;
        } else if (IsGrounded)
        {
            state = MovementState.Walk;
            MoveSpeed = WalkSpeed;
        }
        else
        {
            state = MovementState.Air;
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) { return; }
        MovePlayer();
        CameraMovement();
    }

    void MyInputs()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && IsGrounded && JumpResat)
        {
            Jump();
            JumpResat = false;
            Invoke(nameof(ResetJump), JumpResetCooldown);
        }
    }

    void MovePlayer()
    {
        if (activeGrappel) return;
        Vector3 MoveDir = transform.forward * VerticalInput + transform.right * HorizontalInput;
        
         if (IsGrounded)
            {
        rb.AddForce(MoveDir.normalized * MoveSpeed * 10f, ForceMode.Force);
         }
        else
        {
            rb.AddForce(MoveDir * MoveSpeed * 10f * AirMultiplier, ForceMode.Force);
        }
    }

    void SpeedControl()
    {
        if (activeGrappel) return;
        Vector3 CurVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 CurVelY = new Vector3(0f,rb.velocity.y, 0f);
        
        if(CurVel.magnitude > MoveSpeed)
        {
            Vector3 LimitedVel = CurVel.normalized* MoveSpeed;
            rb.velocity = new Vector3(LimitedVel.x, rb.velocity.y, LimitedVel.z);
        }
        if(CurVelY.magnitude > SwingingSpeed)
        {
            Vector3 LimitedVelY = CurVelY.normalized * SwingingSpeed;
            rb.velocity = new Vector3(rb.velocity.x, LimitedVelY.y, rb.velocity.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        JumpResat = true;
    }

    private bool EnableMovementOnNextTouch;
    public void JumpToPos(Vector3 targetPos, float TrajectoryHeight)
    {
        activeGrappel = true;
        velocityToSet = CalculateJumpVelocity(transform.position, targetPos, TrajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(resetRestriction), 3f);
    }

    Vector3 velocityToSet;
    void SetVelocity()
    {
        EnableMovementOnNextTouch = true;
        rb.velocity =velocityToSet;
    }
    public void resetRestriction()
    {
        activeGrappel = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (EnableMovementOnNextTouch)
        {
            EnableMovementOnNextTouch = false;
            resetRestriction();
            GetComponent<GrappelingHook>().StopGrappel();
        }
    }

    void CameraMovement()
    {
        if(VerticalInput < 0 && state == MovementState.Walk || VerticalInput < 0 && state == MovementState.Sprint)
        {
            
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, CameraFOVAmout, .1f);
            

        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, OriginalFOV, .1f);
            
            
        }
        if(HorizontalInput < 0 && state == MovementState.Sprint || HorizontalInput < 0 && state == MovementState.Walk )
        {
            CamHolder.transform.localRotation = Quaternion.Lerp(CamHolder.transform.localRotation, Quaternion.Euler(0f,0f,CameraRotationAmoutHorizontal), .1f);
        } else if (HorizontalInput > 0 && state == MovementState.Sprint || HorizontalInput > 0 && state == MovementState.Walk)
        {
            CamHolder.transform.localRotation = Quaternion.Lerp(CamHolder.transform.localRotation, Quaternion.Euler(0f, 0f, -CameraRotationAmoutHorizontal), .1f);

        }
        else
        {
            CamHolder.transform.localRotation = Quaternion.Lerp(CamHolder.transform.localRotation, Quaternion.Euler(0f, 0f, 0f), .1f);
        }
    }

    public Vector3 CalculateJumpVelocity(Vector3 StartPos, Vector3 EndPos, float TrajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = EndPos.y - StartPos.y;
        Vector3 displacementXZ = new Vector3(EndPos.x - StartPos.x, 0f, EndPos.z - StartPos.z);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * TrajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2*TrajectoryHeight/gravity) + Mathf.Sqrt(2*(displacementY-TrajectoryHeight)/gravity));

        return velocityXZ + velocityY;
    }
}
