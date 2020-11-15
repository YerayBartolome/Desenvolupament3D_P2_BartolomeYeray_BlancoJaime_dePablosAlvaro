using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserDetector : MonoBehaviour
{
    [SerializeField]
    UnityEvent activate;
    [SerializeField]
    UnityEvent deactivate;
    bool activated = false;
    bool wasActivated = false;

    void Start()
    {
        
    }

    void Update()
    {


        if (!wasActivated && activated) activate.Invoke();
        if(wasActivated && !activated) deactivate.Invoke();
        wasActivated = activated;
        activated = false;
        
    }

    public void Activate()
    {
        activated = true;
    }
}
