using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    /*public Text currentAmmo;
    public Text maxAmmo;
    */
    float mYaw;
    float mPitch;

    float currentDispersion, timeUntilNextShot;

    bool m_AngleLocked, m_AimLocked, reloading; //m

    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockkeyCode = KeyCode.O;

    [Header("Rotation")]

    [SerializeField] GameObject mPitchController;
    [SerializeField] float mPitchSpeed;
    [SerializeField] float mYawSpeed;
    [SerializeField] float mMinPitch;
    [SerializeField] float mMaxPitch;

    [Header("Movement")]

    [SerializeField] CharacterController mCharacterController;
    [SerializeField] float mSpeed;
    public KeyCode mForwardKey = KeyCode.W;
    public KeyCode mLeftKey = KeyCode.A;
    public KeyCode mBackKey = KeyCode.S;
    public KeyCode mRightKey = KeyCode.D;
    public KeyCode mJumpKey = KeyCode.Space;
    public KeyCode mRunKey = KeyCode.LeftShift;
    [SerializeField] float mRunMultiplier;
    [SerializeField] bool mOnGround;
    [SerializeField] bool mContactAbove;

    [Header("Jump")]

    [SerializeField] float mHeightJump;
    [SerializeField] float mHalfLengthJump;
    [SerializeField] float mDownGravityMultiplier;
    float mVerticalSpeed;

    /*[Header("Shoot")]

    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform gunPositioner;
    [SerializeField] WeaponStats currentWeaponStats;
    [SerializeField] int maxImpactParticles = 25;
    private int impactCounter;
    private GameObject[] impactParticles;*/

    /*[Header("Animations")]
    [SerializeField] AnimWeaponController weaponAnim;*/

    private void Awake()
    {
        mPitch = mPitchController.transform.rotation.eulerAngles.x;
        mYaw = transform.rotation.eulerAngles.y;

        /*impactCounter = 0;
        impactParticles = new GameObject[maxImpactParticles];
        for (int i = 0; i < maxImpactParticles; i++)
        {
            impactParticles[i] = Instantiate(currentWeaponStats.impactParticles[(i%currentWeaponStats.impactParticles.Length)], transform.position, Quaternion.identity);
            impactParticles[i].SetActive(false);
        }*/
    }

    private void Start()
    {
        /*currentAmmo.text = ""+currentWeaponStats.currentAmmo; //m
        maxAmmo.text = "" + currentWeaponStats.totalAmmo; //m

        weaponAnim.setIdle();
        currentDispersion = 0f;
        timeUntilNextShot = 0f;
        reloading = false; */
    }

    private void Update()
    {
        /*
        if (reloading && Time.time> timeUntilNextShot)
        {
            reloading = false;
           
            if(currentWeaponStats.totalAmmo-currentWeaponStats.maxAmmoInMag <= 0)
            {
                currentWeaponStats.currentAmmo = currentWeaponStats.totalAmmo;
                currentWeaponStats.totalAmmo = 0;

            }
            else{
                currentWeaponStats.currentAmmo = currentWeaponStats.maxAmmoInMag;
            }
        }*/
        float mouseAxisX = Input.GetAxis("Mouse X");
        float mouseAxisY = Input.GetAxis("Mouse Y");

        //currentAmmo.text = "" + currentWeaponStats.currentAmmo; //m
        //maxAmmo.text = "" + currentWeaponStats.totalAmmo; //m



        CheckLockCursor();

        Rotate(mouseAxisX, mouseAxisY);

        //recoverPrecision();

        /*if (Input.GetMouseButtonDown(0))
        {
            if (currentWeaponStats.currentAmmo > 0) Shoot();
            else reloadWeapon();

        }*/

        Move();

        
    }

    private void Rotate(float mouseAxisX, float mouseAxisY)
    {
        if (!m_AngleLocked)
        {
            mYaw += mouseAxisX * mYawSpeed;
            mPitch += -mouseAxisY * mPitchSpeed;
        }

        mPitch = Mathf.Clamp(mPitch, mMinPitch, mMaxPitch);
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, mYaw, 0.0f));
        mPitchController.transform.localRotation = Quaternion.Euler(new Vector3(mPitch, 0.0f, 0.0f));

        float rotationVelocity = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(mouseAxisX), 2) + Mathf.Pow(Mathf.Abs(mouseAxisY), 2)); //m

        //modifyDispersion(rotationVelocity * currentWeaponStats.camRotationDisp * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 lMovement = new Vector2();
        Vector3 forward = new Vector3(Mathf.Sin(mYaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(mYaw * Mathf.Deg2Rad)); //en funcion del yaw
        Vector3 right = new Vector3(Mathf.Sin((mYaw + 90.0f) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((mYaw + 90.0f) * Mathf.Deg2Rad)); //en funcion del yaw + 90

        if (Input.GetKey(mForwardKey)) lMovement += forward;
        else if (Input.GetKey(mBackKey)) lMovement -= forward;
        if (Input.GetKey(mRightKey)) lMovement += right;
        else if (Input.GetKey(mLeftKey)) lMovement -= right;

        //if (lMovement.magnitude != 0) weaponAnim.setWalk();
        //else weaponAnim.setIdle();

        float lCurrentRunMultiplier = 1.0f;

        if (Input.GetKey(mRunKey))
        {
            lCurrentRunMultiplier = mRunMultiplier;
            //weaponAnim.setRun();
        }

        lMovement.Normalize();
        lMovement *= mSpeed * lCurrentRunMultiplier * Time.deltaTime;

        float gravity = -2 * mHeightJump * mSpeed * mRunMultiplier * mSpeed * mRunMultiplier / (mHalfLengthJump * mHalfLengthJump);
        if (mVerticalSpeed < 0.0f) gravity *= mDownGravityMultiplier;
        mVerticalSpeed += gravity * Time.deltaTime;
        lMovement.y += mVerticalSpeed * Time.deltaTime + 0.5f * gravity * Time.deltaTime * Time.deltaTime;

        CollisionFlags colls = mCharacterController.Move(lMovement);

        mOnGround = (colls & CollisionFlags.Below) != 0;
        if (mContactAbove && mVerticalSpeed > 0.0f) mVerticalSpeed = 0.0f;
        if (mOnGround) mVerticalSpeed = 0.0f;

        if (Input.GetKeyDown(mJumpKey) && mOnGround) mVerticalSpeed = 2 * mHeightJump * mPitchSpeed * mRunMultiplier / mHalfLengthJump;

        //modifyDispersion(lMovement.magnitude * currentWeaponStats.moveDisp * Time.deltaTime);
    }
/*[SerializeField] float decalOffsetFromPlane = 0.01f;
    private void Shoot()
    {
        //fire ratio checker
        weaponAnim.setShoot();

        float randomAngle = Random.Range(0f, currentDispersion); 
        Vector3 axis = new Vector3(1, 1, 0);                    
        var rotation = Quaternion.AngleAxis(randomAngle, axis);
        Ray myRay = new Ray(Camera.main.transform.position, rotation * Camera.main.transform.forward);

        if (Physics.Raycast(myRay, out RaycastHit hitInfo, currentWeaponStats.distance)) ;
        {
            ShootParticles();
            ImpactParticles(hitInfo.point + hitInfo.normal * decalOffsetFromPlane, hitInfo.normal);

            if (hitInfo.transform != null && hitInfo.transform.gameObject.TryGetComponent<DamageTaker>(out DamageTaker dmg))
            {
                dmg.TakeDamage(getDamageFromDistance(currentWeaponStats.damage, currentWeaponStats.distance));   
            }

        }
        modifyDispersion(currentWeaponStats.addDispPerShot); 
        float vRecoil = Random.Range(currentWeaponStats.minHorizontalRecoil, currentWeaponStats.maxHorizontalRecoil);
        float hRecoil = Random.Range(currentWeaponStats.minVerticalRecoil, currentWeaponStats.maxVerticalRecoil);
        Rotate(vRecoil, hRecoil);
    }

    private float getDamageFromDistance(float totalDamage, float distance)
    {
        if (distance > 80) return 0.0f;
        if (distance < 10) return totalDamage;
        return (distance / 80.0f) * totalDamage;
    }

    [SerializeField] float bulletTrailDuration = 0.06f;
    private void ShootParticles()
    {
        GameObject obj01 = Instantiate(currentWeaponStats.muzzleFlashParticles[Random.Range(0, currentWeaponStats.muzzleFlashParticles.Length)], gunPositioner.position, Quaternion.LookRotation(gunPositioner.forward));
        Destroy(obj01, 1f);
        if (Random.Range(0, 3) == 1)
        {
            GameObject obj02 = Instantiate(currentWeaponStats.bulletTrail, gunPositioner.position, Quaternion.LookRotation(gunPositioner.forward));
            Destroy(obj02, bulletTrailDuration);
        }
    }

    private void ImpactParticles(Vector3 point, Vector3 normal)
    {
        GameObject obj01 = Instantiate(currentWeaponStats.flashImpactParticles, point, Quaternion.LookRotation(normal));
        Destroy(obj01, 1f);
        if (impactCounter == 25) impactCounter = 0;
        impactParticles[impactCounter].transform.SetPositionAndRotation(point, Quaternion.LookRotation(normal));
        impactParticles[impactCounter].SetActive(true);
        impactCounter++;
    }*/

    private void OnApplicationFocus(bool focus)
    {
        if (m_AimLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void CheckLockCursor()
    {
        if (Input.GetKeyDown(m_DebugLockAngleKeyCode)) m_AngleLocked = !m_AngleLocked;
        if (Input.GetKeyDown(m_DebugLockkeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
        }
    }
    /*
    private void recoverPrecision()
    {
        modifyDispersion(currentWeaponStats.recoveryRatio);
    }
    private void modifyDispersion(float x)
    {
        float newdisp = currentDispersion + x;
        if (newdisp > currentWeaponStats.maxDispersion) newdisp = currentWeaponStats.maxDispersion;
        if (newdisp < currentWeaponStats.minDispersion) newdisp = currentWeaponStats.minDispersion;

        currentDispersion = newdisp;
    }
    private void reloadWeapon()
    {
        timeUntilNextShot = Time.time + currentWeaponStats.reloadTime;
        // Do reload animation

        
    }*/
}


//maxVerticalRecoil, minVerticalRecoil, maxHorizontalRecoil, minHorizontalRecoil;

//maxAmmoInMag, totalAmmo;