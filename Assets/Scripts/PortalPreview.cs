using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPreview : MonoBehaviour
{
    [SerializeField] List<Transform> validPoints;
    [SerializeField] string portalEnabledTag;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float checkSorroundingDistance = 0.1f, maxDepthOffset = 0.1f;

    public bool isValidPosition(Transform cameraPlayer)
    {

        foreach(Transform valid in validPoints)
        {
            Ray ray0 = new Ray(cameraPlayer.position, valid.position - cameraPlayer.position);
            Ray ray1 = new Ray(transform.position, valid.position - transform.position);
            if (Physics.Raycast(ray0, out RaycastHit hit0, float.MaxValue, layerMask))
            {
                if (!hit0.transform.CompareTag(portalEnabledTag)) return false;
                else if (Mathf.Abs((hit0.point - valid.position).magnitude) > maxDepthOffset) return false;
            } else
            {
                return false;
            }
            if (Physics.Raycast(ray1, out RaycastHit hit1, (valid.position - transform.position).magnitude + checkSorroundingDistance))
            {
                return false;
            }
        }
        return true;
    }
}
