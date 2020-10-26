using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimWeaponController : MonoBehaviour
{

    Animation anim;
     
    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public void setIdle()
    {
        anim.CrossFade("idle_weapon");
    }

    public void setShoot()
    {
        anim.CrossFade("shoot_weapon");
        anim.CrossFadeQueued("idle_weapon");
    }

}
