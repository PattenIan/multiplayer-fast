using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField, HideInInspector] private float spread;
    [SerializeField, HideInInspector] private float TimeBetweenShots;
    [SerializeField, HideInInspector] private int BulletsPerTap;
    [SerializeField, HideInInspector] private float CamShakeAmout;
    [SerializeField, HideInInspector] private float CamShakeTime;
    [SerializeField, HideInInspector] private int damage;
    [SerializeField, HideInInspector] private float fireRate;
    [SerializeField, HideInInspector] private int magSize;
    [SerializeField, HideInInspector] private int currentAmmo;
    [SerializeField, HideInInspector] private float ReloadTime;
    [SerializeField, HideInInspector] private bool Reloading;
    [SerializeField, HideInInspector] private bool readyToShoot;
    [SerializeField, HideInInspector] private bool IsAllowedToHold;
    [SerializeField, HideInInspector] private bool Shooting;
    [SerializeField, HideInInspector] private bool FirstShot;
    [SerializeField, HideInInspector] private int bulletsShots;
    [SerializeField, HideInInspector] private bool CanHaveStabilRecoil;


    [Header("Shotgun")]
    [SerializeField] private float SpreadS;
    [SerializeField] private float CamShakeAmoutS;
    [SerializeField] private float CamShakeTimeS;
    [SerializeField] private int BulletsPerTapS;
    [SerializeField] private float TimeBetweenShotsS;
    [Header("Damage")]
    [SerializeField] private int damageS;
    [SerializeField] private float fireRateS;
    [Header("Magazine size")]
    [SerializeField] private int magSizeS;
    [SerializeField] private int currentAmmoS;
    [Header("Reload")]
    [SerializeField] private float ReloadTimeS;
    [SerializeField] private bool ReloadingS;
    
    
    
    [Header("Assult rifle")]
    [SerializeField] private float SpreadAR;
    [SerializeField] private float CamShakeAmoutAR;
    [SerializeField] private float CamShakeTimeAR;
    [SerializeField] private int BulletsPerTapAR;
    [SerializeField] private float TimeBetweenShotsAR;
    [Header("Damage")]
    [SerializeField] private int damageAR;
    [SerializeField] private float fireRateAR;
    [Header("Magazine size")]
    [SerializeField] private int magSizeAR;
    [SerializeField] private int currentAmmoAR;
    [Header("Reload")]
    [SerializeField] private float ReloadTimeAR;
    [SerializeField] private bool ReloadingAR;
    
    

    [Header("Pistol")]
    [SerializeField] private float SpreadP;
    [SerializeField] private float CamShakeAmoutP;
    [SerializeField] private float CamShakeTimeP;
    [SerializeField] private int BulletsPerTapP;
    [SerializeField] private float TimeBetweenShotsP;
    [Header("Damage")]
    [SerializeField] private int damageP;
    [SerializeField] private float fireRateP;
    [Header("Magazine size")]
    [SerializeField] private int magSizeP;
    [SerializeField] private int currentAmmoP;
    [Header("Reload")]
    [SerializeField] private float ReloadTimeP;
    [SerializeField] private bool ReloadingP;
   


    [Header("Rocket launcher")]
    [SerializeField] private float SpreadRL;
    [SerializeField] private float CamShakeAmoutRL;
    [SerializeField] private float CamShakeTimeRL;
    [SerializeField] private int BulletsPerTapRL;
    [SerializeField] private float TimeBetweenShotsRL;
    [Header("Damage")]
    [SerializeField] private int damageRL;
    [SerializeField] private float fireRateRL;
    [Header("Magazine size")]
    [SerializeField] private int magSizeRL;
    [SerializeField] private int currentAmmoRL;
    [Header("Reload")]
    [SerializeField] private float ReloadTimeRL;
    [SerializeField] private bool ReloadingRL;
    
    

    [Header("Submachine gun")]
    [SerializeField] private float SpreadSG;
    [SerializeField] private float CamShakeAmoutSG;
    [SerializeField] private float CamShakeTimeSG;
    [SerializeField] private int BulletsPerTapSG;
    [SerializeField] private float TimeBetweenShotsSG;
    [Header("Damage")]
    [SerializeField] private int damageSG;
    [SerializeField] private float fireRateSG;
    [Header("Magazine size")]
    [SerializeField] private int magSizeSG;
    [SerializeField] private int currentAmmoSG;
    [Header("Reload")]
    [SerializeField] private float ReloadTimeSG;
    [SerializeField] private bool ReloadingSG;
    
    

    [Header("Rail gun")]
    [SerializeField] private float SpreadRG;
    [SerializeField] private float CamShakeAmoutRG;
    [SerializeField] private float CamShakeTimeRG;
    [SerializeField] private int BulletsPerTapRG;
    [SerializeField] private float TimeBetweenShotsRG;
    [Header("Damage")]
    [SerializeField] private int damageRG;
    [SerializeField] private float fireRateRG;
    [Header("Magazine size")]
    [SerializeField] private int magSizeRG;
    [SerializeField] private int currentAmmoRG;
    [Header("Reload")]
    [SerializeField] private float ReloadTimeRG;
    [SerializeField] private bool ReloadingRG;



    [Header("Gun manager")]
    [SerializeField] private ActiveGun gun;
    [SerializeField] private LayerMask WhatIsEnemy;
    [SerializeField] private float RecoilStabilizationCD;
    [SerializeField,HideInInspector] private float RecoilStabilizationCDTimer;
    [SerializeField] private GameObject BulletHole;
    

    public enum ActiveGun
    {
    Shotgun,
    AssultRifle,
    Pistol,
    RocketLauncher,
    SubmachineGun,
    RailGun
    }

    
    void Start()
    {
        currentAmmo = magSize;
        readyToShoot = true;
    }

    
    void Update()
    {
        MyInput();
        StateHandler();
        if(RecoilStabilizationCDTimer<= 0)
        {
            FirstShot = true;
        }
        else
        {
            RecoilStabilizationCDTimer-= Time.deltaTime;
            FirstShot = false;
        }
    }

    void MyInput()
    {
        if (IsAllowedToHold) Shooting = Input.GetMouseButton(0);
        else Shooting = Input.GetMouseButtonDown(0);

        if(readyToShoot && Shooting && !Reloading && currentAmmo > 0)
        {
            bulletsShots = BulletsPerTap;
            Shoot();
        }
        if(currentAmmo<=0 && !Reloading)
        {
            Reload();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    
    void StateHandler()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gun != ActiveGun.Pistol)
        {
            gun = ActiveGun.Pistol;
            spread = SpreadP;
            CamShakeAmout = CamShakeAmoutP;
            CamShakeTime = CamShakeTimeP;
            damage = damageP;
            fireRate= fireRateP;
            magSize=magSizeP;
            ReloadTime=ReloadTimeP;
            BulletsPerTap= BulletsPerTapP;
            IsAllowedToHold = false;
            CanHaveStabilRecoil = true;

        } else if(Input.GetKeyDown(KeyCode.Alpha2) && gun != ActiveGun.AssultRifle)
        {
            gun = ActiveGun.AssultRifle;
            spread = SpreadAR;
            CamShakeAmout = CamShakeAmoutAR;
            CamShakeTime = CamShakeTimeAR;
            damage = damageAR;
            fireRate = fireRateAR;
            magSize = magSizeAR;
            ReloadTime = ReloadTimeAR;
            BulletsPerTap= BulletsPerTapAR;
            IsAllowedToHold = true;
            CanHaveStabilRecoil = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gun != ActiveGun.Shotgun)
        {
            gun = ActiveGun.Shotgun;
            spread = SpreadS;
            CamShakeAmout = CamShakeAmoutS;
            CamShakeTime = CamShakeTimeS;
            damage = damageS;
            fireRate = fireRateS;
            magSize = magSizeS;
            ReloadTime = ReloadTimeS;
            BulletsPerTap = BulletsPerTapS;
            IsAllowedToHold = false;
            CanHaveStabilRecoil = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && gun != ActiveGun.SubmachineGun)
        {
            gun = ActiveGun.SubmachineGun;
            spread = SpreadSG;
            CamShakeAmout = CamShakeAmoutSG;
            CamShakeTime = CamShakeTimeSG;
            damage = damageSG;
            fireRate = fireRateSG;
            magSize = magSizeSG;
            ReloadTime = ReloadTimeSG;
            BulletsPerTap = BulletsPerTapSG;
            IsAllowedToHold = true;
            CanHaveStabilRecoil = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && gun != ActiveGun.RocketLauncher)
        {
            gun= ActiveGun.RocketLauncher;
            spread = SpreadRL;
            CamShakeAmout = CamShakeAmoutRL;
            CamShakeTime = CamShakeTimeRL;
            damage = damageRL;
            fireRate = fireRateRL;
            magSize = magSizeRL;
            ReloadTime = ReloadTimeRL;
            BulletsPerTap = BulletsPerTapRL;
            IsAllowedToHold = false;
            CanHaveStabilRecoil = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && gun != ActiveGun.RailGun)
        {
            gun=ActiveGun.RailGun;
            spread = SpreadRG;
            CamShakeAmout = CamShakeAmoutRG;
            CamShakeTime = CamShakeTimeRG;
            damage = damageRG;
            fireRate = fireRateRG;
            magSize = magSizeRG;
            ReloadTime = ReloadTimeRG;
            BulletsPerTap = BulletsPerTapRG;
            IsAllowedToHold = false;
            CanHaveStabilRecoil = true;
        }



    }
    Vector3 direction;
    void Shoot()
    {
        readyToShoot = false;
        float x = Random.Range(-spread,spread);
        float y = Random.Range(-spread,spread);
        float z = Random.Range(-spread, spread);
        if (FirstShot && CanHaveStabilRecoil) 
        {
            direction = transform.forward;
            FirstShot= false;
        }
        else
        {
         direction = transform.forward + new Vector3(x,y,z);
        }

        if(Physics.Raycast(transform.position,direction,out RaycastHit hitInfo, Mathf.Infinity, WhatIsEnemy)) 
        {
           //hitInfo.transform.GetComponent<PlayerHealthScript>().HealthUpdate(damage);
        }


        Instantiate(BulletHole, hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal));
        RecoilStabilizationCDTimer = RecoilStabilizationCD;
        currentAmmo--;
        bulletsShots--;
        Invoke(nameof(ResetShot), fireRate);

        if(bulletsShots > 0 && bulletsShots > 0)
        {
            Invoke(nameof(Shoot), TimeBetweenShots);
        }
    }

    void ResetShot()
    {
        readyToShoot = true;
    }

    void Reload()
    {
        Reloading= true;
        Invoke(nameof(ReloadFinished), ReloadTime);

    }

    void ReloadFinished()
    {
        Reloading = false;
        currentAmmo = magSize;
    }
}
