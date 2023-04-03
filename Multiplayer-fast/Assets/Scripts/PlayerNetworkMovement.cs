using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;


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

    [Header("Refrences")]
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        
        rb= GetComponent<Rigidbody>();
        rb.freezeRotation= true;
        MoveSpeed = 7;
        if (IsLocalPlayer) return;
            cam.enabled= false;
        
        
    }
    private void Update()
    {
        if (!IsOwner) { return; }
        rb.drag = GroundDrag;
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
    }

    void MovePlayer()
    {

        Vector3 MoveDir = transform.forward * VerticalInput + transform.right * HorizontalInput;
        rb.AddForce(MoveDir * MoveSpeed * 10f, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 CurVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(CurVel.magnitude > MoveSpeed)
        {
            Vector3 LimitedVel = CurVel.normalized* MoveSpeed;
            rb.velocity = LimitedVel;
        }
    }
}
