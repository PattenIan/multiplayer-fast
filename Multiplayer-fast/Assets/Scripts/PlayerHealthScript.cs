using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class PlayerHealthScript : NetworkBehaviour
{

    private int Health;
    private int MaxHealth = 100;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthUpdate(int Damage)
    {
        

        if (Health < 0)
        {
            cam.enabled= false;
        }
        Health -= Damage;


    }
}
