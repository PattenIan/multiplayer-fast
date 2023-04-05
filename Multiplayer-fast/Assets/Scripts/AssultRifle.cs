using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using System.Diagnostics;

public class AssultRifle : NetworkBehaviour
{

    [SerializeField] private LayerMask EnemyLayer;

    [SerializeField] private float ShootCooldown;
    [SerializeField] private float ShootTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        if(ShootTimer< ShootCooldown)
        {
            ShootTimer += Time.deltaTime;
        }
        else
        {
            ShootTimer = ShootCooldown;
        }
        if (Input.GetKey(KeyCode.Alpha3) && ShootTimer >= ShootCooldown)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(Physics.Raycast(transform.position,transform.forward,out RaycastHit hit, EnemyLayer))
        {

        }
    }
}
