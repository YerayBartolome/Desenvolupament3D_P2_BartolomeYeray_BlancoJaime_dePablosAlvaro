using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] string animName = "open_door";
    Animation anim;
    bool permaClosed = false;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public void OpenDoor()
    {
        anim[animName].speed = 1f;
        anim[animName].time = 0f;
        anim.Play(animName);
    }

    public void CloseDoor()
    {
        anim[animName].speed = -1;
        anim[animName].time = anim[animName].length;
        anim.Play(animName);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!permaClosed && other.CompareTag("Player"))
        {
            CloseDoor();
            permaClosed = true;
        }
    }
}
