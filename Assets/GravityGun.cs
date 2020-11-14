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
        Vector3 posicionAgarre = attachTransform.position;
        if ((pos - posicionAgarre).magnitude != 0)
        {
            Debug.Log("Antes era:"+ pos +" Ahora es: "+ posicionAgarre);
            pos = posicionAgarre;
            
        }
        attachedObject.transform.position = posicionAgarre;
        attachedObject.transform.rotation = attachTransform.rotation;
        
        

    }

    private void updateAttaching()
    {

        attachedObject.MovePosition(attachedObject.position +
            (attachTransform.position - attachedObject.position).normalized
            * moveSpeed * Time.deltaTime);
        attachedObject.rotation = Quaternion.Lerp(initialRot, attachTransform.rotation, (attachedObject.position - initialPos).magnitude / (attachTransform.position - initialPos).magnitude);
        if ((attachedObject.position - attachTransform.position).magnitude < 0.1f)
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
