using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;
using System.Net.NetworkInformation;

public class PlayerNetworkMovement : NetworkBehaviour
{
    [SerializeField, HideInInspector] private float MoveSpeed;
    [SerializeField, HideInInspector] private float HorizontalInput;
    [SerializeField, HideInInspector] private float VerticalInput;

    [Header("Movement")]
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

    [Header("Refrences")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] Camera cam;

    enum MovementState
    {
        Walk,
        Sprint,
        Air,
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
        if (state == MovementState.Walk || state == MovementState.Sprint)
            rb.drag = GroundDrag;
        else
            rb.drag = 0f;
        MyInputs();
        SpeedControl();
        StateHandler();
    }

    void StateHandler()
    {
        if(IsGrounded && Input.GetKey(KeyCode.LeftShift))
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
        Vector3 MoveDir = transform.forward * VerticalInput + transform.right * HorizontalInput;
        if (IsGrounded)
        {
        rb.AddForce(MoveDir.normalized * MoveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(MoveDir.normalized * MoveSpeed * 10f * AirMultiplier, ForceMode.Force);
        }
    }

    void SpeedControl()
    {
        Vector3 CurVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(CurVel.magnitude > MoveSpeed)
        {
            Vector3 LimitedVel = CurVel.normalized* MoveSpeed;
            rb.velocity = new Vector3(LimitedVel.x, rb.velocity.y, LimitedVel.z);
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
}
