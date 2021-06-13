using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    protected override void Update()
    {
        base.Update();
        SwitchStates();
    }
    
    void SwitchStates()
    {

        if (isChase)
            enemyStates = EnemyStates.CHASE;

        switch (enemyStates)
        {
            case EnemyStates.CHASE:
                agent.destination = playerTrans.position;
                firstRun = true;
                if (Vector3.Distance(transform.position, playerTrans.position) < attackRange)
                {
                    LookAway.Instance.target = transform;
                    PlayerDeath.Instance.killBy = PlayerDeath.KillBy.NormalEnemy;
                    GameManager.Instance.NotifyObservers();
                }

                firstRun = true;
                break;
        }

    }

    public void Die()
    {
        Debug.Log("Boss Die!!!");
        Destroy(gameObject);
    }
}
