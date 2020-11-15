using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    LineRenderer m_LineRenderer;
    [SerializeField]
    float m_MaxDistance;
    [SerializeField]
    LayerMask m_CollisionLayerMask;
    bool isActive = true;

      
    void Update()
    {
        m_LineRenderer.enabled = isActive;

        if (!isActive) return;

        Vector3 lastPoint = Vector3.forward * m_MaxDistance;
        if(Physics.Raycast(
            new Ray(m_LineRenderer.transform.position,m_LineRenderer.transform.forward),
            out RaycastHit l_RaycastHit, m_MaxDistance, m_CollisionLayerMask))
        {
            lastPoint = Vector3.forward * l_RaycastHit.distance;

            
            if(l_RaycastHit.transform.gameObject.TryGetComponent(out FPSController player))
            {
                //Kill player
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
        }
        m_LineRenderer.SetPosition(1, lastPoint);
    }
    public void activateLaser()
    {
        isActive = true;
    }
    public void deactivateLaser()
    {
        isActive = false;
    }
}
