using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingRangeController : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] float maxGameTime = 20f;
    [SerializeField] float timeBetweenTargets = 2f;
    [SerializeField] int maxTargetsAtTime = 3;
    [SerializeField] GameObject[] targets;

    [Header("Display")]
    [SerializeField] Canvas canvas;
    [SerializeField] Text displayInfo, displayCurrentPoints, displayHighScore;
    [SerializeField] string welcomeText, startText, endText, winText;

    [Header("Unlock Next Level")]
    [SerializeField] int pointsToUnlock = 20;
    [SerializeField] GameObject doorToNextLevel;
    [SerializeField] Transform closedPosition, openedPosition, playerCheckPointPosition;

    [SerializeField] ProgressionController progressionController;

    private int targetIndex = 0, targetCounter = 0;
    private int currentPoints, highScore = 0;
    private bool play = false;
    private float gameStartTime = 0, lastTargetStartTime = 0;

    private void Awake()
    {
        doorToNextLevel.transform.SetPositionAndRotation(closedPosition.position, closedPosition.rotation);
        canvas.enabled = false;
    }

    private void Update()
    {
        if (play && Input.GetKeyDown(KeyCode.E))
        {
            gameStartTime = Time.time;
        }
        if (play)
        {
            if (Time.time - gameStartTime < maxGameTime) ShootingRangeGame();
            else StopShootingRangeGame();
        }
        if (highScore >= pointsToUnlock)
        {
            UnlockNextLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!play)
            {
                canvas.enabled = true;
                displayHighScore.enabled = true;
                displayCurrentPoints.enabled = true;
                displayInfo.text = welcomeText;
                
            } else
            {

            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") && !play)
        {
            canvas.enabled = false;
        }
    }

    private void ShootingRangeGame()
    {
        if (targetCounter < maxTargetsAtTime && Time.time - lastTargetStartTime >= timeBetweenTargets)
        {
            if (targetIndex >= targets.Length) targetIndex = 0;
            GameObject target = Instantiate(targets[targetIndex], transform);
            lastTargetStartTime = Time.time;
            targetCounter++;
            targetIndex++;
            Destroy(target, target.GetComponent<Animation>().clip.averageDuration);
            targetCounter--;
        }
    }

    private void StopShootingRangeGame()
    {

    }

    private void UnlockNextLevel()
    {
        doorToNextLevel.transform.SetPositionAndRotation(openedPosition.position, openedPosition.rotation);
        progressionController.CheckPoint(playerCheckPointPosition);
        progressionController.UnlockNextLevel();
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
    }
}
