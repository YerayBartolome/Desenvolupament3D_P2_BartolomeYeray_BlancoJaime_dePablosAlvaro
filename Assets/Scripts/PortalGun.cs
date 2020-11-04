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
    Transform portalA;
    [SerializeField]
    Transform portalB;
    [SerializeField]
    Camera cameraPlayer;
    bool isValid;

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

    [Header("CompanionCubeGrabber")]
    [SerializeField] Transform cubePositioner;
    [SerializeField] float cubeForwardForce;
    [SerializeField] float cubeTravelTime, cubeDisableTime;
    [SerializeField] private bool cubeGrabbed = false;
    Transform companionCube;
    private float timeToGrab;

    private void Awake()
    {
        spriteRenderer.sprite = crosshair;
        light.color = Color.white;
        timeToGrab = 0f;
    }

    void Update()
    {
        spriteRenderer.sprite = crosshair;
        bool isValid = false;

        if (!cubeGrabbed && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) && !grabCube())
        {
            
            isValid = putPreview();
            spriteRenderer.sprite = Input.GetMouseButton(0) ? crosshairB : crosshairO;
            meshRenderer.material = Input.GetMouseButton(0) ? materialB : materialO;
            light.color = Input.GetMouseButton(0) ? colorB : colorO;
            
        }

        if (cubeGrabbed && Input.GetMouseButtonDown(1) && Time.time > timeToGrab)
        {
            releaseCube(false);
        }

        if (cubeGrabbed && Input.GetMouseButtonDown(0) && Time.time > timeToGrab)
        {
            releaseCube(true);
        }

        portalPreview.gameObject.SetActive(isValid);

        if (isValid && Input.GetMouseButtonUp(1))
        {
            putPortal(portalA);
            spriteRenderer.sprite = crosshairB;
        }
        if (isValid && Input.GetMouseButtonUp(0))
        {
            putPortal(portalB);
            spriteRenderer.sprite = crosshairO;
        }
    }

    private bool grabCube()
    {
        if (cubeGrabbed) return false;
        Ray r = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (!cubeGrabbed && Time.time > timeToGrab && Physics.Raycast(r, out RaycastHit hit, maxShootDistance))
        {
            if (!cubeGrabbed && hit.transform.gameObject.CompareTag("Cube"))
            {
                companionCube = hit.transform;
                timeToGrab = Time.time + cubeDisableTime;
                cubeGrabbed = true;
                companionCube.GetComponent<Rigidbody>().useGravity = false;
                companionCube.GetComponent<Rigidbody>().isKinematic = true;
                companionCube.GetComponent<TeleportableObject>().enabled = false;
                companionCube.SetParent(this.transform);
                companionCube.localPosition = Vector3.Lerp(hit.transform.localPosition, cubePositioner.localPosition, cubeTravelTime);
                companionCube.rotation.SetLookRotation(Camera.main.transform.forward);
                Debug.Log("Cube Grabbed");
                return true;
            }
        }
        return false;
    }

    private void releaseCube(bool shoot)
    {
        if (companionCube != null)
        {
            Vector3 worldPos = transform.TransformPoint(companionCube.localPosition);
            companionCube.parent = null;
            companionCube.GetComponent<Rigidbody>().useGravity = true;
            companionCube.GetComponent<Rigidbody>().isKinematic = false;
            companionCube.GetComponent<TeleportableObject>().enabled = true;
            companionCube.SetPositionAndRotation(worldPos, Quaternion.LookRotation(Camera.main.transform.forward));
            if (shoot)
            {
                companionCube.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * cubeForwardForce);
                Debug.Log("Cube Shot");
            }
            companionCube = null;
            timeToGrab = Time.time + cubeDisableTime;
            cubeGrabbed = false;
            Debug.Log("Cube Released");
        } else
        {
            cubeGrabbed = false;
        }
        
    }

    private void putPortal(Transform portal)
    {
        portal.gameObject.SetActive(true);
        portal.position = portalPreview.position;
        portal.forward = portalPreview.forward;
        portal.transform.localScale = new Vector3(portalPreview.localScale.x, portalPreview.localScale.y, portal.transform.localScale.z);
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
