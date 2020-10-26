using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionController : MonoBehaviour
{

    [SerializeField] GameObject Player;
    [SerializeField] Transform playerSpawn;
    private bool nextLevelUnlocked = false;

    public void Awake()
    {
        Player.transform.position = playerSpawn.position;
    }

    public void CheckPoint(Transform checkPointPosition)
    {
        playerSpawn.position = checkPointPosition.position;
    }

    public void UnlockNextLevel()
    {
        nextLevelUnlocked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nextLevelUnlocked && other.tag.Equals("Player"))
        {
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("Traveling to next area...");
        }
    }
}
