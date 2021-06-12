using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    public int health;
    
    protected override void Update()
    {
        base.Update();
        SwitchStates();
        if(health <= 0)
            Destroy(transform.parent.gameObject);
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
                    GameManager.Instance.NotifyObservers();
                }

                firstRun = true;
                break;
        }

    }
}
