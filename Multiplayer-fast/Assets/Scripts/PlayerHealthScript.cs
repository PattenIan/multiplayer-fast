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
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI textHealth;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        textHealth.text = Health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthUpdate(int Damage)
    {
        
        textHealth.text = Health.ToString();
        if (Health < 0)
        {
            
            text.enabled = true;
        }
        Health -= Damage;


    }
}
