using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTransform : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    Vector3 prePosition = new Vector3(0, 0, 0);
    // Update is called once per frame
    void Update()
    {
        Vector3 nowPosition = transform.position;
        if (prePosition != nowPosition)
        {
            Debug.Log(transform.position);
  
            prePosition = nowPosition;
        }
    }
}
