using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Transform firstSpawn;
    Transform currentSpawn;
    void Start()
    {
        currentSpawn = firstSpawn;
    }

    // Update is called once per frame
    public void Die()
    {
        GetComponent<CharacterController>().enabled= false;
        transform.position = currentSpawn.position;
        GetComponent<CharacterController>().enabled = true;
    }
    public void setSpawn(Transform newSpawn)
    {
        currentSpawn = newSpawn;
    }
}
