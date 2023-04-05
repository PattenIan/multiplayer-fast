using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using System;

public class PlayerNetworkCameraLook : NetworkBehaviour
{
    [Range(25f, 250f)]
    [SerializeField] private float Sens;
    
    [SerializeField] private Transform PlayerRef;

     float xRotation;
    public float MouseY;
   
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) { return; }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }
         MouseY = Input.GetAxis("Mouse Y") * Sens*Time.deltaTime;
        float MouseX = Input.GetAxis("Mouse X") * Sens * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation,0,0);
        PlayerRef.Rotate(Vector3.up * MouseX);


    }
}
