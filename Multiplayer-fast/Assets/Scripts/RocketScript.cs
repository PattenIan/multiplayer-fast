using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
       Physics.IgnoreCollision(FindObjectOfType<PlayerNetworkMovement>().GetComponentInChildren<CapsuleCollider>(), transform.GetComponent<Collider>(), true);
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        
            Collider[] Players = Physics.OverlapSphere(transform.position, BlastRadius, Blastable);

            foreach (var obj in Players)
            {
            print(obj.name);
                
            Vector3 DirToBombFromTarget = (obj.gameObject.transform.position - transform.position).normalized;


             var pmComp = obj.GetComponentInParent<PlayerNetworkMovement>();
                print("Hej");
              //  pmComp.GetComponent<PlayerNetworkMovement>().ExplosionDirection(DirToBombFromTarget);
                print("Med");

            }
        Destroy(gameObject);
        
    }
    
}
