using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCube : MonoBehaviour
{
    [SerializeField] GameObject destroyedCube;
    public void Destroy()
    {
        destroyedCube = Instantiate(destroyedCube, transform.position, transform.rotation);
        destroyedCube.GetComponent<Transform>().localScale = transform.localScale;
        Destroy(gameObject);
    }
}
