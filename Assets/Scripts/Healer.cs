using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{

    [SerializeField]
    private int hpRestore;
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
        if (collision.gameObject.CompareTag("Player") && !used)
        {
            collision.gameObject.GetComponent<DamageTaker>().TakeDamage(hpRestore);
            gameObject.GetComponent<AudioSource>().Play(0);
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            used = true;
        }
    }
}
