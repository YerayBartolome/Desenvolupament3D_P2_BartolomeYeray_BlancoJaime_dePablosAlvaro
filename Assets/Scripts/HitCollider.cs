using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour, DamageTaker
{
    [SerializeField]
    Enemy enemy;

    [SerializeField]
    float damagePercentage;

    public void TakeDamage(float damage)
    {
        Debug.Log("Take " + damage * damagePercentage + " damage");
        enemy.takeDamage(damage * damagePercentage);
        
    }
}
