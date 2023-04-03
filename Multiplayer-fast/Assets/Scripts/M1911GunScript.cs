using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class M1911GunScript : NetworkBehaviour
{

    [SerializeField] private LayerMask EnemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, Mathf.Infinity, EnemyLayer)) 
        {
            
            var EnemyHit = hitInfo.transform.gameObject;
            if(EnemyHit!=this.gameObject)
            {

            EnemyHit.GetComponent<PlayerHealthScript>().HealthUpdate(15);
            }

        }

    }
}
