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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (!wasActivated && activated) Debug.Log("Abrir");//activate.Invoke();
        if(wasActivated && !activated) Debug.Log("Cerrar");//deactivate.Invoke();
        wasActivated = activated;
        activated = false;
        
    }

    public void Activate()
    {
        activated = true;
    }
}
