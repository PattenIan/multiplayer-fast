using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using TMPro;

public class PlayerHealthScript : NetworkBehaviour
{

    public int Health;
    private int MaxHealth = 100;
    [SerializeField] TextMeshProUGUI text;
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
            
            text.enabled = true;
        }
        Health -= Damage;


    }
}
