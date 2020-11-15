using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OntriggerEnterSetSpawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out GameController gc))
        {
            gc.setSpawn(transform);
        }
           
    }
}
