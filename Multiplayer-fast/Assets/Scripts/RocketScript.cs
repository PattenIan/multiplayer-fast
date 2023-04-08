using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [Header("Blast settings")]
    [SerializeField] private float BlastRadius;
    [SerializeField] private float BlastForce;
    [SerializeField] private LayerMask Blastable;
    [SerializeField] private float RocketLength;
    [SerializeField] private float RocketRadius;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
            Collider[] Players = Physics.OverlapSphere(transform.position, BlastRadius, Blastable);

            foreach (var obj in Players)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null) continue;
            Vector3 DirToBombFromTarget = (rb.gameObject.transform.position - transform.position).normalized;
            rb.gameObject.GetComponent<PlayerNetworkMovement>().ExplosionDirection(DirToBombFromTarget);
            
            }
        Destroy(gameObject);
        
    }
    
}
