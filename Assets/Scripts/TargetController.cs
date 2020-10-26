using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour, DamageTaker
{

    [SerializeField] ShootingRangeController shootingRange;
    [SerializeField] int points;

    void Start()
    {
        shootingRange = GameObject.FindObjectOfType<ShootingRangeController>();
    }

    public void TakeDamage(float damage)
    {
       shootingRange.AddPoints(points);
    }

}
