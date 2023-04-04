using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using Unity.VisualScripting;

public class SwingingScript : NetworkBehaviour
{

    [SerializeField, HideInInspector] private Rigidbody rb;
    [SerializeField] private Camera cam;
    [SerializeField] LayerMask Grappable;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform GrapplingHookTip;

    SpringJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) { return; }
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
     if (!IsOwner) { return; }
      if (Input.GetMouseButtonDown(1))
      {
            StartSwing();
      }  

      if(Input.GetMouseButtonUp(1))
        {
            StopSwinging();
        }
    }

    Vector3 HitPoint;
    void StartSwing()
    {
        RaycastHit HitInfo;
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out HitInfo, 20f, Grappable))
        {
            HitPoint = HitInfo.point;
            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = HitPoint;
            float RopeDistance = Vector3.Distance(transform.position , HitPoint);
            joint.maxDistance = RopeDistance * 0.8f;
            joint.minDistance = RopeDistance * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
            lr.positionCount = 2;
        }
    }

    private void LateUpdate()
    {
        drawRope();
    }
    void drawRope()
    {
        if (!joint)
        {
            lr.enabled = false;
        }
        else
        {
            lr.enabled = true;
            lr.SetPosition(0, GrapplingHookTip.position);
            lr.SetPosition(1, HitPoint);
        }
    }

    void StopSwinging()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
