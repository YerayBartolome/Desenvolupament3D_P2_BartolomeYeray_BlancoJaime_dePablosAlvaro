using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    enum EnemyStates { IDLE, PATROL, ALERT, CHASE, ATTACK, HIT, DIE }
    EnemyStates currentState;
    [SerializeField]
    private float health;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //SetState(EnemyStates.IDLE);           
    }

    private void Update()
    {
        if (health <= 0) SetState(EnemyStates.DIE);

      /*  switch (currentState)
        {
            case EnemyStates.IDLE:
                UpdateIdle();
                break;
            case EnemyStates.PATROL:
                UpdatePatrol();
                break;
            case EnemyStates.ALERT:
                UpdateAlert();
                break;
            case EnemyStates.CHASE:
                UpdateChase();
                break;
            case EnemyStates.ATTACK:
                UpdateAttack();
                break;
            case EnemyStates.HIT:
                UpdateHit();
                break;
            case EnemyStates.DIE:
                UpdateDie();
                break;
        } */
    }

    private void SetState(EnemyStates newState)
    {
        switch (currentState)
        {
            case EnemyStates.IDLE:
                EndIdle();
                break;
            case EnemyStates.PATROL:
                EndPatrol();
                break;
            case EnemyStates.ALERT:
                EndAlert();
                break;
            case EnemyStates.CHASE:
                EndChase();
                break;
            case EnemyStates.ATTACK:
                EndAttack();
                break;
            case EnemyStates.HIT:
                EndHit();
                break;
            case EnemyStates.DIE:
                EndDie();
                break;
        }
        currentState = newState;
        switch (currentState)
        {
            case EnemyStates.IDLE:
                StartIdle();
                break;
            case EnemyStates.PATROL:
                StartPatrol();
                break;
            case EnemyStates.ALERT:
                StartAlert();
                break;
            case EnemyStates.CHASE:
                StartChase();
                break;
            case EnemyStates.ATTACK:
                StartAttack();
                break;
            case EnemyStates.HIT:
                StartHit();
                break;
            case EnemyStates.DIE:
                StartDie();
                break;
        }
    }

    private void StartIdle()
    {
        throw new NotImplementedException();
    }

    private void StartPatrol()
    {
        throw new NotImplementedException();
    }

    private void StartAlert()
    {
        throw new NotImplementedException();
    }

    private void StartChase()
    {
        throw new NotImplementedException();
    }

    private void StartAttack()
    {
        throw new NotImplementedException();
    }

    private void StartHit()
    {
        throw new NotImplementedException();
    }

    private void StartDie()
    {
        throw new NotImplementedException();
    }

    private void UpdateIdle()
    {
        throw new NotImplementedException();
    }

    private void UpdatePatrol()
    {
        throw new NotImplementedException();
    }

    private void UpdateAlert()
    {
        throw new NotImplementedException();
    }

    private void UpdateChase()
    {
        throw new NotImplementedException();
    }

    private void UpdateAttack()
    {
        throw new NotImplementedException();
    }

    private void UpdateHit()
    {
        throw new NotImplementedException();
    }

    private void UpdateDie()
    {
        throw new NotImplementedException();
    }

    private void EndIdle()
    {
        throw new NotImplementedException();
    }

    private void EndPatrol()
    {
        throw new NotImplementedException();
    }

    private void EndAlert()
    {
        throw new NotImplementedException();
    }

    private void EndChase()
    {
        throw new NotImplementedException();
    }

    private void EndAttack()
    {
        throw new NotImplementedException();
    }

    private void EndHit()
    {
        throw new NotImplementedException();
    }

    private void EndDie()
    {
        throw new NotImplementedException();
    }



    public void takeDamage(float damage)
    {
        health -= damage;
 
    }
}
