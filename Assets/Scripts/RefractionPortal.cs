using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionPortal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    LineRenderer m_LineRenderer;
    [SerializeField]
    Transform virtualPortal;
    [SerializeField]
    Transform otherPortal;
    bool m_CreateRefraction = false;
    void Update()
    {
        m_LineRenderer.gameObject.SetActive(m_CreateRefraction);
        m_CreateRefraction = false;
    }
    public void CreateRefraction(LineRenderer ray)
    {
        Vector3 origin = ray.GetPosition(0);
        origin = ray.transform.TransformPoint(origin);
        Vector3 final = ray.GetPosition(1);
        final = ray.transform.TransformPoint(final);
        Vector3 direction = (final - origin);
        direction = virtualPortal.InverseTransformDirection(direction);
        

        final = virtualPortal.InverseTransformPoint(final);
        //final = otherPortal.TransformPoint(final);
        Debug.Log("Other Portal Local final: " + direction);
        if (m_LineRenderer.transform.TryGetComponent(out PortalLaser pl))
        {
            //pl.setOrigin(final);
            pl.setDirection(direction);
           
        }
        /*
         Vector3 otherOrigin = m_LineRenderer.GetPosition(0);
        Vector3 otherFinal = direction * 50 + origin;
        Debug.Log(direction);
        //otherFinal = otherPortal.InverseTransformDirection(otherFinal);
        m_LineRenderer.SetPosition(1, otherFinal);
        */
        m_CreateRefraction = true;

    }
}

