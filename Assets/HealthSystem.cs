using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, DamageTaker
{

    //[SerializeField] Text TcurrentHealth, TmaxHealth, TcurrentShiedl, TmaxShield;
    [SerializeField] float currentHealth, maxHealth, currentShield, maxShield;



    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage * 0.75f;
            currentHealth -= damage * 0.25f;
        } else
        {
            currentHealth -= damage;
        }
    }

    public void Heal(float hp)
    {
        currentHealth = Mathf.Clamp(currentHealth+hp, 0f, maxHealth);
    }

    public void restoreShield(float shield)
    {
        currentShield = Mathf.Clamp(currentHealth + shield, 0f, maxShield);
    }

    void Update()
    {
        /*TcurrentHealth.text = currentHealth.ToString();
        TmaxHealth.text = maxHealth.ToString();
        TcurrentShiedl.text = currentShield.ToString();
        TmaxShield.text = maxShield.ToString();
        */
        if (currentHealth <= 0)
        {
            //GAME OVER
        }
    }
}
