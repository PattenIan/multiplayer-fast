using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GrappelingHook : NetworkBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform ReleasePoint;
    [SerializeField] private PlayerNetworkMovement pm;
    [SerializeField] private LayerMask Grappable;
    [SerializeField] private LineRenderer lr;

    [Header("Grappeling")]
    [SerializeField] private float MaxGrappelingDistance;
    [SerializeField] private float GrappelDelayTime;
    [SerializeField] private float OverShootYAxis;

    Vector3 grapPoint;

    [Header("Cooldown")]
    [SerializeField] private float grappelingCD;
    [SerializeField] private float grappelingCDTimer;

    bool grappeling;
    void Start()
    {
        
        pm = GetComponent<PlayerNetworkMovement>();
        lr.positionCount = 2;
    }

    
    void Update()
    {
        if(!IsOwner) return;
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartGrappel();
        }

        if(grappelingCDTimer>0)
        {
            grappelingCDTimer-=Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if(grappeling)
        {

        lr.SetPosition(0, ReleasePoint.position);
        }
    }

    void StartGrappel()
    {
        if (grappelingCDTimer > 0) return;
        GetComponent<SwingingScript>().StopSwinging();
        grappeling = true;
        pm.freeze = true;
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, MaxGrappelingDistance, Grappable))
        {
            grapPoint= hit.point;

            Invoke(nameof(ExecuteGrappel), GrappelDelayTime);
        }
        else
        {
            grapPoint = cam.position + cam.forward * MaxGrappelingDistance;
            Invoke(nameof(StopGrappel), GrappelDelayTime);
        }
        lr.enabled= true;
        lr.SetPosition(1,grapPoint);
    }

    void ExecuteGrappel()
    {
        pm.freeze= false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grappelPointRelativeYPos = grapPoint.y - lowestPoint.y;
        float highestPointOnArc = grappelPointRelativeYPos + OverShootYAxis;
        if (grappelPointRelativeYPos < 0) highestPointOnArc = OverShootYAxis;
        pm.JumpToPos(grapPoint, highestPointOnArc);
        Invoke(nameof(StopGrappel), 1f);
    }

    public void StopGrappel()
    {
        pm.freeze = false;
        grappeling=false;

        grappelingCDTimer = grappelingCD;
        lr.enabled = false;
    }
}
