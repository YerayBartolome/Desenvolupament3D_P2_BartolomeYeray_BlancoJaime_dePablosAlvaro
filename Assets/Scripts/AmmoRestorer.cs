using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRestorer : MonoBehaviour
{
    /*[SerializeField]
    private int ammoRestore;
    private bool used = false;

    private void FixedUpdate()
    {
        if (used && !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (!used && other.CompareTag("Player"))
        {
            GameController gameController = other.GetComponent<GameController>();
            if (gameController.totalAmmo + ammoRestore < gameController.maxTotalAmmo)
            {
                gameController.totalAmmo += ammoRestore;
            } else
            {
                gameController.totalAmmo = gameController.maxTotalAmmo;
            }
            gameObject.GetComponent<AudioSource>().Play(0);
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            used = true;
        }
    }*/
}
