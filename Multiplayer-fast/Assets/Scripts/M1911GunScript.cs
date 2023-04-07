using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using TMPro;

public class M1911GunScript : NetworkBehaviour
{
    [SerializeField] Animator gunAnimator;
    [SerializeField] private LayerMask EnemyLayer;
    bool ShootAgainTime;
    [SerializeField] ParticleSystem muzzleflash;

    [Header("Magazine")]
    [SerializeField] private int MagazineSize;
    [SerializeField] private int BulletsLeft;
    [SerializeField] private float ReloadCD;
    [SerializeField] private float ReloadCDTimer;
    [SerializeField] private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = gameObject.GetComponentInChildren<Animator>();
        ShootAgainTime = true;
        muzzleflash.Stop();
        BulletsLeft = MagazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }
        if(Input.GetMouseButtonDown(0) && ShootAgainTime && !PauseMenu.gameIsPaused && BulletsLeft>0)
        {
            BulletsLeft -= 1;
            Shoot();
            ShootAgainTime = false;
            muzzleflash.Play();
            
            gunAnimator.SetBool("isShooting", true);
            Invoke(nameof(ResetShot), 0.75f);
        }
        if(BulletsLeft<= 0 || Input.GetKeyDown(KeyCode.R))
        {
            Invoke(nameof(Reload), ReloadCD);
        }
            text.text = MagazineSize + "/" + BulletsLeft;
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

    void Reload()
    {
        BulletsLeft = MagazineSize;
        
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
