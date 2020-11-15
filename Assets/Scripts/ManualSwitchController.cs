using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManualSwitchController : MonoBehaviour
{
    [SerializeField] string animName = "switch_anim";
    [SerializeField] KeyCode switchKey = KeyCode.E;
    [SerializeField] UnityEvent switchPressed;
    private bool isInteractable = false;
    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        if (isInteractable && Input.GetKeyDown(switchKey))
        {
            switchPressed.Invoke();
            anim.Play(animName);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = false;
        }
    }

}
