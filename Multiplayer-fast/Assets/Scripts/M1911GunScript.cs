using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class M1911GunScript : NetworkBehaviour
{
    [SerializeField] Animator gunAnimator;
    [SerializeField] private LayerMask EnemyLayer;
    bool ShootAgainTime;
    [SerializeField] ParticleSystem muzzleflash;
    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = gameObject.GetComponentInChildren<Animator>();
        ShootAgainTime = true;
        muzzleflash.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }
        if(Input.GetMouseButtonDown(0) && ShootAgainTime && !PauseMenu.gameIsPaused)
        {
            Shoot();
            ShootAgainTime = false;
            muzzleflash.Play();
            
            gunAnimator.SetBool("isShooting", true);
            Invoke(nameof(ResetShot), 0.75f);
        }
    }

            Vector3 Hitpoint;
    void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, Mathf.Infinity, EnemyLayer)) 
        {
            Hitpoint= hitInfo.point;
            var EnemyHit = hitInfo.transform.gameObject;
            
            if(EnemyHit!=this.gameObject)
            {
            EnemyHit.GetComponent<PlayerHealthScript>().HealthUpdate(15);
            }

        }

    }

    
    void ResetShot()
    {
        
        muzzleflash.Stop();
        gunAnimator.SetBool("isShooting", false);
        ShootAgainTime = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Hitpoint);
    }
}
