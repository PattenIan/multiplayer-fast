using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
public class RocketLauncherScript : NetworkBehaviour
{
    [Header("Rocket settings")]
    [SerializeField] private GameObject RocketPrefab;
    [SerializeField] private float RocketSpeed;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private float BlastRadius;
    [SerializeField] private LayerMask Blastable;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Shoot();
        }
    }
    Vector3 hitPoint;
    void Shoot()
    {
       if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            hitPoint= hit.point;
            float Dist = Vector3.Distance(transform.position, hitPoint);
            print(Dist);
            Invoke(nameof(DelayedRocketImpact), Dist * 0.01f);
        }

    }

    void DelayedRocketImpact()
    {
        Collider[] Players = Physics.OverlapSphere(hitPoint, BlastRadius, Blastable);

        foreach (var obj in Players)
        {
            print(obj.name);

            Vector3 DirToBombFromTarget = (transform.position - hitPoint).normalized;


            var pmComp = obj.GetComponentInParent<PlayerNetworkMovement>();
            
            pmComp.GetComponent<PlayerNetworkMovement>().ExplosionDirection(DirToBombFromTarget);
            

        }
    }
}
