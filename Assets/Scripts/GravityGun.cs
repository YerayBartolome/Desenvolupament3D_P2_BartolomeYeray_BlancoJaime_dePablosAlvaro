using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    Rigidbody attachedObject;
    [SerializeField] GameObject transformPoint;
    Transform attachTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] float throwForce;

    Quaternion initialRot;
    Vector3 initialPos;
    Vector3 finalPos;
    enum GravityState
    {
        attaching, attached
    }

    GravityState stateGun;

    // Update is called once per frame
    private void Awake()
    {
        attachTransform = transformPoint.transform;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && attachedObject == null)
        {
            GetComponent<PortalGun>().enabled = false;
            attachedObject = shootGravity();
        }

        else if (Input.GetKeyDown(KeyCode.E) && attachedObject != null)
        {
            detachObject(throwForce);
            GetComponent<PortalGun>().enabled = true;

        }
        else if ((Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) && attachedObject != null)
        {
            detachObject(0);
            GetComponent<PortalGun>().enabled = true;
        }
        else
        {
            if (attachedObject != null)
            {
                /* ---THERE WAS AN ATTEMPT --- */
                /*Ray[] checkers = new Ray[] {
                    new Ray(Camera.main.transform.position, attachTransform.position - Camera.main.transform.position),
                    new Ray(attachTransform.position, attachTransform.up),
                    new Ray(attachTransform.position, attachTransform.up * -1),
                    new Ray(attachTransform.position, attachTransform.right * -1),
                    new Ray(attachTransform.position, attachTransform.right)
                };
                float distance = (attachTransform.position - Camera.main.transform.position).magnitude;
                foreach (Ray ray in checkers)
                {
                    if (Physics.Raycast(ray, out RaycastHit hit, distance, LayerMask.GetMask("Scenary")))
                    {
                        GameObject collision = hit.transform.gameObject;
                        Debug.Log(collision.name);
                        finalPos = hit.point + (attachedObject.transform.lossyScale.x * Mathf.Sqrt(3) / 2) * hit.normal;
                    }
                    else finalPos = attachTransform.position;
                    distance = Mathf.Max(((SphereCollider) attachedObject.GetComponent<Collider>()).radius / 2, ((BoxCollider)attachedObject.GetComponent<Collider>()).size.y * Mathf.Sqrt(3) / 2);
                }*/

                Ray ray = new Ray(Camera.main.transform.position, attachTransform.position - Camera.main.transform.position);
                float distance = (attachTransform.position - Camera.main.transform.position).magnitude;

                if (Physics.Raycast(ray, out RaycastHit hit, distance, LayerMask.GetMask("Scenary")))
                {
                    GameObject collision = hit.transform.gameObject;
                    Debug.Log(collision.name);
                    finalPos = hit.point + (attachedObject.transform.lossyScale.x * Mathf.Sqrt(3) / 2) * hit.normal;
                }
                else finalPos = attachTransform.position;


                switch (stateGun)
                {
                    case GravityState.attaching:
                        updateAttaching();
                        break;
                    case GravityState.attached:
                        updateAttached();
                        break;
                }
            }
        }

        GetComponent<PortalGun>().enabled = attachedObject == null;
    }

    private void detachObject(float force)
    {
        attachedObject.isKinematic = false;
        attachedObject.AddForce(attachTransform.forward * force);
        attachedObject = null;
    }

    Vector3 pos = new Vector3(0,0,0);

    private void updateAttached()
    {
        Vector3 posicionAgarre = finalPos;
        if ((pos - posicionAgarre).magnitude != 0)
        {
            pos = posicionAgarre;
            
        }
        attachedObject.transform.position = posicionAgarre;
        attachedObject.transform.rotation = Quaternion.LookRotation(new Vector3(transform.forward.x, Vector3.forward.y, transform.forward.z));
        
        

    }

    private void updateAttaching()
    {

        attachedObject.MovePosition(attachedObject.position +
            (finalPos - attachedObject.position).normalized
            * moveSpeed * Time.deltaTime);
        attachedObject.rotation = Quaternion.Lerp(initialRot, Quaternion.LookRotation(new Vector3(transform.forward.x, Vector3.forward.y, transform.forward.z)), (attachedObject.position - initialPos).magnitude / (finalPos - initialPos).magnitude);
        if ((attachedObject.position - finalPos).magnitude < 0.1f)
            stateGun = GravityState.attached;
    }

    Rigidbody shootGravity()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out RaycastHit hitInfo, 150.0f))
        {
            Rigidbody rb = hitInfo.transform.GetComponent<Rigidbody>();
            if (rb == null)
            {
                return null;
            }
            rb.isKinematic = true;
            initialPos = rb.transform.position;
            initialRot = rb.transform.rotation;
            stateGun = GravityState.attaching;
            return rb;
        }
        return null;
    }
}
