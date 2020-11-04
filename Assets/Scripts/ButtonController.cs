using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false, isCube = false;
    Animation anim;
    [SerializeField] string animName = "button_pressed";
    [SerializeField] CubeLauncher cubeLauncher;
    private bool cubeDestroyed = false;
    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayer && !isCube && other.CompareTag("Player"))
        {
            isPlayer = true;
            playAnim(false);
        }

        if (!isPlayer && !isCube && other.CompareTag("Cube"))
        {
            isCube = true;
            playAnim(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = false;
        }

        if (other.CompareTag("Cube"))
        {
            isCube = false;
        }

        if (!isPressed())
        {
            playAnim(true);
        }
    }

    public bool isPressed()
    {
        return isPlayer || isCube;
    }

    private void playAnim(bool backwards)
    {
        anim[animName].speed = backwards ? -1 : 1;
        anim[animName].time = backwards ? anim[animName].length : 0.0f;
        anim.Play(animName);
        if (!backwards)
        {
            cubeLauncher.LaunchCube(out bool destroyed);
            cubeDestroyed = destroyed;
            if (cubeDestroyed) isCube = false;
        }
    }

    private void Update()
    {
        if (cubeDestroyed)
        {
            playAnim(true);
            cubeDestroyed = false;
        }
    }

}
