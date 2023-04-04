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

    void Shoot()
    {
       var rocket = Instantiate(RocketPrefab, FirePoint.position, Quaternion.identity);
       Rigidbody rb= rocket.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * RocketSpeed, ForceMode.Impulse);
    }
}
