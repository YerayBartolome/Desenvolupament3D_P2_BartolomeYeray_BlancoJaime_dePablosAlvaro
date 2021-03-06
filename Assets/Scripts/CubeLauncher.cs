﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLauncher : MonoBehaviour
{
    [SerializeField] GameObject companionCube;
    [SerializeField] Transform cubeSpawn;
    [SerializeField] float spawnForce;
    GameObject currentCube = null;
    public void LaunchCube()
    {
        if (currentCube != null)
        {
            currentCube.transform.parent = null;
            currentCube.GetComponent<CompanionCube>().Destroy();
            currentCube = null;
        }
        currentCube = Instantiate(companionCube, cubeSpawn.position, cubeSpawn.rotation);
        currentCube.GetComponent<Rigidbody>().AddForce(cubeSpawn.forward * spawnForce);
    }


}
