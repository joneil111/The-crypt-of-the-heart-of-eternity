using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}
public enum typeWeapon
{
    HANDGUN,
    SHOTGUN,
    RIFLE
}


public class WeapoHandler : MonoBehaviour
{
    public typeWeapon TypeWeapon;
    private Animator anim;

    public WeaponAim weapon_Aim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound, reloadSound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attack_point;

    public GameObject fire;

    void Awake()
    {
        anim = GetComponent<Animator>();

    }


    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);

    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    void Turn_On_MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void Play_Shoot_Sound()
    {
        shootSound.Play();
    }

    void Play_Reload_Sound()
    {
        reloadSound.Play();    
    }

    void Turn_On_AttackPoint()
    {
        attack_point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }
    void bulletFire()
    {
        fire.GetComponent<PlayerAttack>().BulletFired();
    }


}
