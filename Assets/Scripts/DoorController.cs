using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] string animName = "open_door";
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public void OpenDoor()
    {
        anim.Play(animName);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim[animName].speed = -1;
            anim[animName].time = anim[animName].length;
            anim.Play(animName);
        }
    }
}
