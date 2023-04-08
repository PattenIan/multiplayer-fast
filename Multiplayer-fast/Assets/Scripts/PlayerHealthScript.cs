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
        textHealth.text = textHealth.ToString();
        if (IsLocalPlayer) return;
        textHealth.enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
        
        textHealth.text = Health.ToString();
    }

    public void HealthUpdate(int Damage)
    {
        
        if (Health < 0)
        {
            
            text.enabled = true;
        }
        Health -= Damage;
        textHealth.text = Health.ToString();


    }
}
