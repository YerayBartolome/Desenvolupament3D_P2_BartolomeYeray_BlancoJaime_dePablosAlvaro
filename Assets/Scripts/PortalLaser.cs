using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLaser : MonoBehaviour
{
    [SerializeField]
    LineRenderer m_LineRenderer;
    [SerializeField]
    float m_MaxDistance;
    [SerializeField]
    LayerMask m_CollisionLayerMask;
    [SerializeField]
    float offset = 0.5f;
    bool isActive = true;
    Vector3 direction = Vector3.zero;
    Vector3 origin = Vector3.zero;


    void Update()
    {

        Vector3 lastPoint =  (direction * m_MaxDistance);
        //m_LineRenderer.SetPosition(0, origin);
        
        if (Physics.Raycast(
            new Ray(m_LineRenderer.transform.position, m_LineRenderer.transform.TransformDirection(direction)),
            out RaycastHit l_RaycastHit, m_MaxDistance, m_CollisionLayerMask))
        {
            lastPoint =  (direction * l_RaycastHit.distance);


            if (l_RaycastHit.transform.gameObject.TryGetComponent(out GameController player))
            {
                player.Die();
            }
            else if (l_RaycastHit.transform.gameObject.CompareTag("Turret"))
            {
                Destroy(l_RaycastHit.transform.gameObject);
            }
            else if (l_RaycastHit.collider.TryGetComponent(out RefractionCube rc)) //codigo facilito.com
            {
                //Reflect ray
                rc.CreateRefraction();
            }
            else if (l_RaycastHit.collider.TryGetComponent(out LaserDetector ls))
            {
                ls.Activate();
            }
            else if (l_RaycastHit.collider.TryGetComponent(out RefractionPortal rp))
            {
                rp.CreateRefraction(m_LineRenderer);
            }
        }
        m_LineRenderer.SetPosition(1, lastPoint);
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }
    public void setOrigin(Vector3 point)
    {
        origin = point + Vector3.forward * offset;
    }
}
