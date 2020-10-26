using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Header("Dispersion")]
    public float addDispPerShot;
    public float camRotationDisp;
    public float moveDisp;
    public float recoveryRatio;
    public float maxDispersion;
    public float minDispersion;
    [Header("Recoil")]
    public float maxVerticalRecoil;
    public float minVerticalRecoil;
    public float maxHorizontalRecoil;
    public float minHorizontalRecoil;
    [Header("Stats")]
    public float damage;
    public float distance;
    public float currentAmmo; 
    public float reloadTime;
    public float maxAmmoInMag;
    public float totalAmmo;
    [Header("Particles")]
    [SerializeField]
    public GameObject[] impactParticles;
    [SerializeField]
    public GameObject flashImpactParticles;
    [SerializeField]
    public GameObject[] muzzleFlashParticles;
    [SerializeField]
    public GameObject bulletTrail;

}
