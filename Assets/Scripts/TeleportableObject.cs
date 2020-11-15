using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportableObject : MonoBehaviour
{

    [SerializeField] float teleportOffset;

    Vector3 teleportPosition;
    Vector3 teleportForward;
    Vector3 teleportVelocity;
    Vector3 beforeTriggerVelocity;
    bool teleporting;
    float teleportSizeRatio;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Portal portal))
        {
            Debug.Log("Portal Collision");
            Vector3 l_Position = portal.virtualPortal.transform.InverseTransformPoint(transform.position);
            Vector3 l_Direction = portal.virtualPortal.transform.InverseTransformDirection(transform.forward);
            teleportPosition = portal.otherPortal.transform.TransformPoint(l_Position);
            teleportForward = portal.otherPortal.transform.TransformDirection(l_Direction);
            teleportPosition += portal.otherPortal.transform.forward * teleportOffset;
            var velocity = beforeTriggerVelocity;                         // transform.GetComponent<Rigidbody>().velocity;
            velocity = portal.virtualPortal.transform.InverseTransformDirection(velocity);
            velocity = portal.otherPortal.transform.TransformDirection(velocity);
            teleportVelocity = velocity;


            if (TryGetComponent(out CharacterController characterController)) characterController.enabled = false;
            

            if (transform.CompareTag("Cube")) teleportSizeRatio = (portal.otherPortal.transform.localScale.x / portal.transform.localScale.x);


            teleporting = true;

        }
    }
    private void FixedUpdate()
    {
        beforeTriggerVelocity = transform.GetComponent<Rigidbody>().velocity;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (teleporting)
        {
            transform.position = teleportPosition;
            transform.forward = teleportForward;
            //GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = teleportVelocity;
            if (transform.CompareTag("Cube")) transform.localScale *= teleportSizeRatio;
            teleporting = false;
            if (TryGetComponent(out FPSController controller)) controller.setYawAndPitch();
            if (TryGetComponent(out CharacterController characterController))
            {
                characterController.enabled = true;
            }
        }
    }
}
