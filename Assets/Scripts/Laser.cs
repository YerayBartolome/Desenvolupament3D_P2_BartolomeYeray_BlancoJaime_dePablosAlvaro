﻿using System.Collections;
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
            lastPoint = Vector3.forward * l_RaycastHit.distance/2 ;
            Debug.Log(lastPoint);
            if(l_RaycastHit.transform.gameObject.TryGetComponent(out FPSController player))
            {
                //Kill player
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