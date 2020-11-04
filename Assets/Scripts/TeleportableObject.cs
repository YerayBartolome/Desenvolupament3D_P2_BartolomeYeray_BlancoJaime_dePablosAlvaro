using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportableObject : MonoBehaviour
{

    [SerializeField] float teleportOffset;

    Vector3 teleportPosition;
    Vector3 teleportForward;
    Vector3 teleportVelocity;
    bool teleporting;
    Vector3 teleportSize;

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
            teleportVelocity = Vector3.Reflect(transform.GetComponent<Rigidbody>().velocity, portal.otherPortal.transform.forward);

            if (TryGetComponent(out CharacterController characterController))
            {
                characterController.enabled = false;
            }

            if (transform.CompareTag("Cube"))
            {
                float teleportSizeX = transform.localScale.x * portal.otherPortal.getScale().x / portal.getScale().x;
                float teleportSizeY = transform.localScale.y * portal.otherPortal.getScale().y / portal.getScale().y;
                float teleportSizeZ = transform.localScale.z * portal.otherPortal.getScale().y / portal.getScale().y;
                
                teleportSize = new Vector3(teleportSizeX, teleportSizeY, teleportSizeZ);
                teleportPosition += portal.otherPortal.transform.forward * teleportSize.z;
            }

            teleporting = true;

        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (teleporting)
        {
            transform.position = teleportPosition;
            transform.forward = teleportForward;
            GetComponent<Rigidbody>().velocity = teleportVelocity;
            if (transform.CompareTag("Cube")) transform.localScale = teleportSize;
            teleporting = false;
            if (TryGetComponent(out FPSController controller)) controller.setYawAndPitch();
            if (TryGetComponent(out CharacterController characterController))
            {
                characterController.enabled = true;
            }
        }
    }
}
