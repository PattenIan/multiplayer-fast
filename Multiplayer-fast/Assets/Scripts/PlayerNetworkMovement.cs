using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;
using System.Net.NetworkInformation;

public class PlayerNetworkMovement : NetworkBehaviour
{
    public Camera cam;
    [SerializeField, HideInInspector] private float MoveSpeed;
    [SerializeField, HideInInspector] private float HorizontalInput;
    [SerializeField, HideInInspector] private float VerticalInput;

    [Header("Movement")]
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float SpringSeed;
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

    [Header("Refrences")]
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        JumpResat = true;
        rb= GetComponent<Rigidbody>();
        rb.freezeRotation= true;
        MoveSpeed = 7;
        if (IsLocalPlayer) return;
            cam.enabled= false;
        
        
    }
    private void Update()
    {
        if (!IsOwner) { return; }
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, WhatIsGround);
        if (IsGrounded)
            rb.drag = GroundDrag;
        else
            rb.drag = 0f;
        MyInputs();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) { return; }
        MovePlayer();
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
}
