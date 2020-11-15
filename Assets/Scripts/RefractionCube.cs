using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionCube : MonoBehaviour
{
    [SerializeField]
    LineRenderer m_LineRenderer;
    bool m_CreateRefraction = false;
    void Update()
    {
        m_LineRenderer.gameObject.SetActive(m_CreateRefraction);
        m_CreateRefraction = false;
    }
    public void CreateRefraction()
    {
        m_CreateRefraction = true;
        
    }
}
/*
    [SerializeField]
    float m_MaxDistance = 20f;
    [SerializeField]
    LayerMask m_CollisionLayerMask;
  Vector3 l_EndRaycastPosition = Vector3.forward * m_MaxDistance;
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(new Ray(m_LineRenderer.transform.position, m_LineRenderer.transform.forward), out l_RaycastHit, m_MaxDistance, m_CollisionLayerMask.value))
        {
            l_EndRaycastPosition = Vector3.forward * l_RaycastHit.distance;
            if (l_RaycastHit.collider.TryGetComponent(out RefractionCube rc)) //tag == "RefractionCube"
            {
                //Reflect ray
                rc.CreateRefraction();
            }
            //Other collisions
        }
        m_LineRenderer.SetPosition(1, l_EndRaycastPosition);
 */
