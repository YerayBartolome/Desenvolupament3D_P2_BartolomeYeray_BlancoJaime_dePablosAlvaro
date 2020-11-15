using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressureButton : MonoBehaviour
{
    [SerializeField] string animName = "button_pressed";
    [SerializeField] UnityEvent pressureDetected, pressureReleased;
    Animation anim;

    private List<Collider> colliders = new List<Collider>();
    private List<Collider> collidersToIgnore = new List<Collider>();

    private bool isEnabled = false;

    public List<Collider> GetColliders()
    {
        return colliders;
    }

    private void Awake()
    {
        anim = GetComponent<Animation>();
        foreach (Collider col in Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius))
        {
            collidersToIgnore.Add(col);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) foreach (var col in colliders) Debug.Log(col.gameObject.name);
        if (Input.GetKeyDown(KeyCode.Y)) foreach (var col in collidersToIgnore) Debug.Log(col.gameObject.name);
        if (CheckForeignColliders())
        {
            if (!isEnabled)
            {
                isEnabled = true;
                pressureDetected.Invoke();
                playAnim(false);
            }
        } else
        {
            if (isEnabled)
            {
                isEnabled = false;
                pressureReleased.Invoke();
                playAnim(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collidersToIgnore.Contains(other) && !colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

    private bool CheckForeignColliders()
    {
        List<Collider> colIter = colliders; 
        foreach(Collider col in colIter)
        {
            if (col == null)
            {
                colliders.Remove(col);
                return false;
            } else
            {
                return true;
            }
        }
        return false;
    }

    private void playAnim(bool backwards)
    {
        anim[animName].speed = backwards ? -1 : 1;
        anim[animName].time = backwards ? anim[animName].length : 0.0f;
        anim.Play(animName);
    }

}
