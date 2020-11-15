using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [Header("Portal Positioning")]
    [SerializeField]
    LayerMask portalRaycastMask;
    [SerializeField]
    float maxShootDistance;
    [SerializeField]
    Transform portalPreview;
    [SerializeField]
    GameObject portalBlue;
    [SerializeField]
    GameObject portalOrange;
    [SerializeField]
    Camera cameraPlayer;
    bool isValid;
    Transform portalA;
    Transform portalB;
    bool isBlue, isOrange;

    [Header("Diegetics")]
    [SerializeField]
    float minSize;
    [SerializeField]
    float maxSize;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite crosshair;
    [SerializeField]
    Sprite crosshairB;
    [SerializeField]
    Sprite crosshairO;
    [SerializeField]
    Sprite crosshairBO;
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    Material materialB, materialO;
    [SerializeField]
    Light light;
    [SerializeField]
    Color colorB, colorO;


    private void Awake()
    {
        spriteRenderer.sprite = crosshair;
        light.color = Color.white;
        bool isValid = false;
        portalA = portalBlue.transform;
        portalB = portalOrange.transform;
    }

    void Update()
    {
        spriteRenderer.sprite = crosshair;
        

        if ( Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            isValid = putPreview();
            spriteRenderer.sprite = Input.GetMouseButton(0) ? crosshairB : crosshairO;
            meshRenderer.material = Input.GetMouseButton(0) ? materialB : materialO;
            light.color = Input.GetMouseButton(0) ? colorB : colorO;
            
        } else
        {
            if (isBlue && isOrange) spriteRenderer.sprite = crosshairBO;
            else if (isBlue) spriteRenderer.sprite = crosshairB;
            else if (isOrange) spriteRenderer.sprite = crosshairO;
            else spriteRenderer.sprite = crosshair;
        }

        portalPreview.gameObject.SetActive(isValid);

        if (isValid && Input.GetMouseButtonUp(1))
        {
            isOrange = true;
            putPortal(portalB);
            spriteRenderer.sprite = crosshairB;
        }
        if (isValid && Input.GetMouseButtonUp(0))
        {
            isBlue = true;
            putPortal(portalA);
            spriteRenderer.sprite = crosshairO;
        }

    }



    private void putPortal(Transform portal)
    {
        portal.gameObject.SetActive(true);
        portal.position = portalPreview.position;
        portal.forward = portalPreview.forward;
        portal.transform.localScale = new Vector3(portalPreview.localScale.x, portalPreview.localScale.y, portal.transform.localScale.z);
        isValid = false;
    }

    bool putPreview()
    {
        Ray r = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(r, out RaycastHit hit, maxShootDistance, portalRaycastMask))
        {
            portalPreview.position = hit.point;
            portalPreview.forward = hit.normal;
            float newScaleX = Mathf.Clamp(portalPreview.localScale.x + portalPreview.localScale.x * Input.GetAxis("Mouse ScrollWheel"), minSize, maxSize);
            float newScaleY = Mathf.Clamp(portalPreview.localScale.y + portalPreview.localScale.y * Input.GetAxis("Mouse ScrollWheel"), minSize, maxSize);
            portalPreview.localScale = new Vector3(newScaleX, newScaleY, portalPreview.localScale.z);
            return portalPreview.GetComponent<PortalPreview>().isValidPosition(cameraPlayer.transform);
        }

        return false;
    }
}
