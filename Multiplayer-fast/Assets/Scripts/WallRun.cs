using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class WallRun : NetworkBehaviour
{

    [SerializeField] private PlayerNetworkMovement pm;
    [SerializeField] private float MoveSpeedOnWallRun;
    [SerializeField] private bool Wallrunning;
    [SerializeField] private LayerMask WallrunLayer;
    [SerializeField] private float MinJumpHeight;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private float WallClimbSpeed;
    bool UpwardsRunning;
    bool DownwardsRunning;
    

    private Rigidbody rb;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    bool leftWall;
    bool rightWall;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm=GetComponent<PlayerNetworkMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        CheckForWall();
        StateMachine();
        
    }
    private void FixedUpdate()
    {
        if(pm.IsWallrunning)
        {
            WallRunMovement();
        }
    }

    void CheckForWall()
    {
        rightWall = Physics.Raycast(transform.position, transform.right, out rightWallHit, 1f, WallrunLayer);
        leftWall = Physics.Raycast(transform.position, -transform.right, out leftWallHit, 1f, WallrunLayer);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, MinJumpHeight, WhatIsGround);
    }
    float HorizontalInput;
    float VerticalInput;
    private void StateMachine()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
        UpwardsRunning = Input.GetKey(KeyCode.Space);
        DownwardsRunning= Input.GetKey(KeyCode.LeftControl);
        if((leftWall||rightWall) && VerticalInput>0 && AboveGround())
        {
            if (!pm.IsWallrunning)
            {
                StartWallRun();
            }
        }
        else
        {
            if(pm.IsWallrunning) 
            {
                StopWallRun();
            }
        }
    }

    void StartWallRun()
    {
        pm.IsWallrunning = true;
    }

    void WallRunMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        Vector3 wallNormal = rightWall ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);
        if((transform.forward - wallForward).magnitude >(transform.forward- -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }
        rb.AddForce(wallForward * MoveSpeedOnWallRun, ForceMode.Force);
        if(UpwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, WallClimbSpeed, rb.velocity.z);
        }
        if (DownwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, -WallClimbSpeed, rb.velocity.z);
        }
        if (!(leftWall && HorizontalInput>0) && !(rightWall && HorizontalInput<0)) 
        { 

        rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }
    }

    void StopWallRun()
    {
        rb.useGravity = true;
        pm.IsWallrunning = false;
    }
}
