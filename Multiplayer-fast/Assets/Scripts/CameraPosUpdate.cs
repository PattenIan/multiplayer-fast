using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class CameraPosUpdate : NetworkBehaviour
{
    public Camera cam;
    public Transform CameraPos;
    // Start is called before the first frame update
    void Start()
    {
        CameraPos.position = FindObjectOfType<PlayerNetworkMovement>().transform.position + new Vector3(0,0.25f,0);
        if (IsLocalPlayer) return;
        cam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer) return;
        cam.transform.position = CameraPos.position;
    }
}
