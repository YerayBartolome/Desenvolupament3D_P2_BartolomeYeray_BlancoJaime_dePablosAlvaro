using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCube : MonoBehaviour
{
    [SerializeField] GameObject destroyedCube;
    public void Destroy()
    {
        Instantiate(destroyedCube, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
