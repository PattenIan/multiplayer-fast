using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;


public class PlayerNetwork : NetworkBehaviour
{
    private Camera cam;
    private Camera notHostCam;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (IsLocalPlayer) return;
        
            cam.enabled= false;
        
        
    }
    private void Update()
    {
        if (!IsOwner) { return; }


        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) { moveDir.z += 1f; }
        if (Input.GetKey(KeyCode.S)) { moveDir.z -= 1f; }
        if (Input.GetKey(KeyCode.A)) { moveDir.x -= 1f; }
        if (Input.GetKey(KeyCode.D)) { moveDir.x += 1f; }

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
