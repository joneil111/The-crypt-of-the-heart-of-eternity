using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private WeaponManager weaponManager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    public GameObject test1;
    private Animator test2;

    private Animator zoomCameraAnim;
    private bool Zoomed;

    private Camera mainCam;
    [SerializeField]
    private GameObject DoubleDamageText;


    private GameObject crossair;
    public float Ddamage = 1;
    private bool isAiming;


    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();

        //test2 = transform.Find(Tags.LOOK_ROOT).GetComponent<Animator>();


        zoomCameraAnim = transform.Find("LookRoot").transform.Find("FPCamera").GetComponent<Animator>();

        crossair = GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weapon_Shoot();
        ZoomInAndOut();

        if(weaponManager.GetCurrentSelectedWeapon().TypeWeapon == typeWeapon.HANDGUN)
        {
            fireRate = 15f;
            damage = 20f;
        }
        else if (weaponManager.GetCurrentSelectedWeapon().TypeWeapon == typeWeapon.SHOTGUN)
        {
            damage = 50f;
        }
        else if (weaponManager.GetCurrentSelectedWeapon().TypeWeapon == typeWeapon.RIFLE)
        {
            damage = 10f;
        }
    }

    void weapon_Shoot()
    {
        //assault rifle
        if(weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {

            //if we press and hold left mouse click And
            //if time is greater than nexttimetofire
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire){
                nextTimeToFire = Time.time + 1f / fireRate;

                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                //BulletFired();
            }
        }
        //regualar weapon.ie shoot once
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                    //print("here");

                    //BulletFired();
                }

            }
        }
    }

    void ZoomInAndOut()
    {

        //aim with camera on the weapon if weapon is aimable.
        if (weaponManager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM)
        {
            //if press and hold right mouse button
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);

                crossair.SetActive(false);
            }
            //When you release right mouse button 
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);

                crossair.SetActive(true);
            }
        }
    }

    public void BulletFired()
    {
        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward,out hit))
        {
            //print("We hit: " + hit.transform.gameObject.name);

            if(hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage* Ddamage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageBall")
        {

            other.gameObject.SetActive(false);
            Ddamage = 2;
            DoubleDamageText.SetActive(true);
            Invoke("DamageBack", 15f);
            
        }

    }

    void DamageBack()
    {
        Ddamage = 1;
        DoubleDamageText.SetActive(false);
    }
}
