using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField, HideInInspector] private float spread;
    [SerializeField, HideInInspector] private float TimeBetweenShots;
    [SerializeField, HideInInspector] private int BulletsPerTap;
    [SerializeField, HideInInspector] private float CamShakeAmout;
    [SerializeField, HideInInspector] private float CamShakeTime;
    [SerializeField, HideInInspector] private int Sdamage;
    [SerializeField, HideInInspector] private int Mdamage;
    [SerializeField, HideInInspector] private int Ldamage;
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
    [SerializeField, HideInInspector] private int ShortRange;
    [SerializeField, HideInInspector] private int MediumRange;
    [SerializeField, HideInInspector] private int LongRange;
    [Header("Shotgun")]
    [SerializeField] private float SpreadS;
    [SerializeField] private float CamShakeAmoutS;
    [SerializeField] private float CamShakeTimeS;
    [Header("Damage")]
    [SerializeField] private int SdamageS;
    [SerializeField] private int MdamageS;
    [SerializeField] private int LdamageS;
    [SerializeField] private float fireRateS;
    [SerializeField] private int BulletsPerTapS;
    [SerializeField] private float TimeBetweenShotsS;
    [SerializeField] private int ShortRangeS;
    [SerializeField] private int MediumRangeS;
    [SerializeField] private int LongRangeS;
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
    [Header("Damage")]
    [SerializeField] private int SdamageAR;
    [SerializeField] private int MdamageAR;
    [SerializeField] private int LdamageAR;
    [SerializeField] private float fireRateAR;
    [SerializeField] private int BulletsPerTapAR;
    [SerializeField] private float TimeBetweenShotsAR;
    [SerializeField] private int ShortRangeAR;
    [SerializeField] private int MediumRangeAR;
    [SerializeField] private int LongRangeAR;
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
    [Header("Damage")]
    [SerializeField] private int SdamageP;
    [SerializeField] private int MdamageP;
    [SerializeField] private int LdamageP;
    [SerializeField] private float fireRateP;
    [SerializeField] private int BulletsPerTapP;
    [SerializeField] private float TimeBetweenShotsP;
    [SerializeField] private int ShortRangeP;
    [SerializeField] private int MediumRangeP;
    [SerializeField] private int LongRangeP;
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
    [Header("Damage")]
    [SerializeField] private int SdamageRL;
    [SerializeField] private int MdamageRL;
    [SerializeField] private int LdamageRL;
    [SerializeField] private float fireRateRL;
    [SerializeField] private int BulletsPerTapRL;
    [SerializeField] private float TimeBetweenShotsRL;
    [SerializeField] private int ShortRangeRL;
    [SerializeField] private int MediumRangeRL;
    [SerializeField] private int LongRangeRL;
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
    [Header("Damage")]
    [SerializeField] private int SdamageSG;
    [SerializeField] private int MdamageSG;
    [SerializeField] private int LdamageSG;
    [SerializeField] private float fireRateSG;
    [SerializeField] private int BulletsPerTapSG;
    [SerializeField] private float TimeBetweenShotsSG;
    [SerializeField] private int ShortRangeSG;
    [SerializeField] private int MediumRangeSG;
    [SerializeField] private int LongRangeSG;
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
    [Header("Damage")]
    [SerializeField] private int SdamageRG;
    [SerializeField] private int MdamageRG;
    [SerializeField] private int LdamageRG;
    [SerializeField] private float fireRateRG;
    [SerializeField] private int BulletsPerTapRG;
    [SerializeField] private float TimeBetweenShotsRG;
    [SerializeField] private int ShortRangeRG;
    [SerializeField] private int MediumRangeRG;
    [SerializeField] private int LongRangeRG;
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
    [SerializeField] private TextMeshProUGUI BulletText;
    [SerializeField] private CamShake camShake;
    

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
        BulletText.text = currentAmmo + " / " + magSize;
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
            Sdamage = SdamageP;
            Mdamage= MdamageP;
            Ldamage= LdamageP;
            fireRate= fireRateP;
            magSize=magSizeP;
            ReloadTime=ReloadTimeP;
            BulletsPerTap= BulletsPerTapP;
            IsAllowedToHold = false;
            CanHaveStabilRecoil = true;
            LongRange = LongRangeP;
            MediumRange= MediumRangeP;
            ShortRange= ShortRangeP;
            currentAmmo = magSize;

        } else if(Input.GetKeyDown(KeyCode.Alpha2) && gun != ActiveGun.AssultRifle)
        {
            gun = ActiveGun.AssultRifle;
            spread = SpreadAR;
            CamShakeAmout = CamShakeAmoutAR;
            CamShakeTime = CamShakeTimeAR;
            Sdamage = SdamageAR;
            Mdamage= MdamageAR;
            Ldamage= LdamageAR;
            fireRate = fireRateAR;
            magSize = magSizeAR;
            ReloadTime = ReloadTimeAR;
            BulletsPerTap= BulletsPerTapAR;
            IsAllowedToHold = true;
            CanHaveStabilRecoil = true;
            ShortRange=ShortRangeAR;
            MediumRange= MediumRangeAR;
            LongRange= LongRangeAR;
            currentAmmo = magSize;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gun != ActiveGun.Shotgun)
        {
            gun = ActiveGun.Shotgun;
            spread = SpreadS;
            CamShakeAmout = CamShakeAmoutS;
            CamShakeTime = CamShakeTimeS;
            Sdamage = SdamageS;
            Mdamage= MdamageS;
            Ldamage= LdamageS;
            fireRate = fireRateS;
            magSize = magSizeS;
            ReloadTime = ReloadTimeS;
            BulletsPerTap = BulletsPerTapS;
            IsAllowedToHold = false;
            CanHaveStabilRecoil = false;
            ShortRange = ShortRangeS;
            MediumRange= MediumRangeS;
            LongRange= LongRangeS;
            currentAmmo = magSize;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && gun != ActiveGun.SubmachineGun)
        {
            gun = ActiveGun.SubmachineGun;
            spread = SpreadSG;
            CamShakeAmout = CamShakeAmoutSG;
            CamShakeTime = CamShakeTimeSG;
            Sdamage = SdamageSG;
            Mdamage= MdamageSG;
            Ldamage= LdamageSG;
            fireRate = fireRateSG;
            magSize = magSizeSG;
            ReloadTime = ReloadTimeSG;
            BulletsPerTap = BulletsPerTapSG;
            IsAllowedToHold = true;
            CanHaveStabilRecoil = false;
            ShortRange= ShortRangeSG;
            MediumRange= MediumRangeSG;
            LongRange=LongRangeSG;
            currentAmmo = magSize;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && gun != ActiveGun.RocketLauncher)
        {
            gun= ActiveGun.RocketLauncher;
            spread = SpreadRL;
            CamShakeAmout = CamShakeAmoutRL;
            CamShakeTime = CamShakeTimeRL;
            Sdamage = SdamageRL;
            Mdamage = MdamageRL;
            Ldamage = LdamageRL;
            fireRate = fireRateRL;
            magSize = magSizeRL;
            ReloadTime = ReloadTimeRL;
            BulletsPerTap = BulletsPerTapRL;
            IsAllowedToHold = false;
            CanHaveStabilRecoil = true;
            ShortRange= ShortRangeRL;
            MediumRange= MediumRangeRL;
            LongRange= LongRangeRL;
            currentAmmo = magSize;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && gun != ActiveGun.RailGun)
        {
            gun=ActiveGun.RailGun;
            spread = SpreadRG;
            CamShakeAmout = CamShakeAmoutRG;
            CamShakeTime = CamShakeTimeRG;
            Sdamage = SdamageRG;
            Mdamage = MdamageRG;
            Ldamage = LdamageRG;
            fireRate = fireRateRG;
            magSize = magSizeRG;
            ReloadTime = ReloadTimeRG;
            BulletsPerTap = BulletsPerTapRG;
            IsAllowedToHold = false;
            CanHaveStabilRecoil = true;
            ShortRange= ShortRangeRG;
            MediumRange= MediumRangeRG;
            LongRange= LongRangeRG;
            currentAmmo = magSize;
        }



    }
    Vector3 direction;
    void Shoot()
    {
        readyToShoot = false;
        
        Vector3 allAxisSpread = Random.insideUnitSphere * spread;
        if (FirstShot && CanHaveStabilRecoil) 
        {
            direction = transform.forward;
            FirstShot= false;
        }
        else
        {
         direction = transform.forward + allAxisSpread;
        }
        
        if(Physics.Raycast(transform.position,direction,out RaycastHit hitInfo, Mathf.Infinity, WhatIsEnemy)) 
        {
            float DistToTarget = Vector3.Distance(transform.position, hitInfo.point);
            if (hitInfo.transform.GetComponent<PlayerHealthScript>())
            {

            if (DistToTarget < ShortRange)
            {
           hitInfo.transform.GetComponent<PlayerHealthScript>().HealthUpdate(Sdamage);

            } else if(DistToTarget>ShortRange && DistToTarget < MediumRange)
            {
            hitInfo.transform.GetComponent<PlayerHealthScript>().HealthUpdate(Mdamage);
            } else if (DistToTarget > MediumRange)
            {
                hitInfo.transform.GetComponent<PlayerHealthScript>().HealthUpdate(Ldamage);
            }
            }
        }

        StartCoroutine(camShake.Shake(CamShakeAmout, CamShakeTime));
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
