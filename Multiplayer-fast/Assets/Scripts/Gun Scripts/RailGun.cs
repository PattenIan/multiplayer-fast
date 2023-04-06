using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class RailGun : NetworkBehaviour
{
    [Header("Line Effect")]
    [SerializeField] private LineRenderer LR;
    [SerializeField] private float minLineSize;
    [SerializeField] private float maxLineSize;
    [SerializeField] private float lineSize;
    [SerializeField] private Transform ShootPoint;

    [Header("Ray itself")]
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private float ShotRadius;

    [Header("Camera Shake")]
    [SerializeField] private Camera cam;
    [SerializeField] private float Shake;
    [SerializeField] private float DecreaseFactor;
    [SerializeField] private float shakeAmount;

    [Header("Cooldown")]
    [SerializeField] private bool CanShoot;
    [SerializeField] private float ReloadCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.Alpha2) && CanShoot)
        {
            CanShoot= false;
            LineUpdater();
            
            Invoke(nameof(Shoot), 0.75f);
        }
        if (Shake > 0)
        {
            cam.transform.localPosition = Random.insideUnitCircle * shakeAmount;
            Shake -= Time.deltaTime * DecreaseFactor;
        }
        else
        {
            Shake = 0.0f;
            cam.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    Vector3 hitPoint;
    void LineUpdater()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 75))
        {
            hitPoint= hit.point;

        }
        else
        {
            hitPoint = transform.position + transform.forward * 75f;
        }
        LR.enabled = true;
        LR.positionCount = 2;
        
        LR.SetPosition(1, hitPoint);
        LR.startWidth = minLineSize;
        LR.endWidth = minLineSize;
        
        
    }
    private void LateUpdate()
    {
        if (!IsOwner) return;
        LR.SetPosition(0,ShootPoint.position);
    }

    void ReloadDone()
    {
        CanShoot = true;
    }

    void Shoot()
    {
        LR.positionCount = 0;
        LR.enabled = false;
        Shake = 1f;
       
        if(Physics.SphereCast(transform.position,ShotRadius, transform.forward, out RaycastHit hit, 75, EnemyLayer))
        {
            

        }

        Invoke(nameof(ReloadDone), ReloadCooldown);
    }
}
