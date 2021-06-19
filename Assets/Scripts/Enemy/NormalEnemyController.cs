using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemyController : EnemyController
{
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        SwitchStates();
    }
    
    void SwitchStates()
    {
        #region SwitchStates

        // if (isDead)
        //     enemyStates = EnemyStates.DEAD;
        if (isCaught)
            enemyStates = EnemyStates.CATCH;
        else if (isChase)
            enemyStates = EnemyStates.CHASE;
        else if (isEscape)
            enemyStates = EnemyStates.ESCAPE;

        switch (enemyStates)
        {
            case EnemyStates.CATCH:
                transform.forward = new Vector3(playerTrans.position.x - transform.position.x,0, playerTrans.position.z - transform.position.z).normalized;
                //agent.destination = transform.position;
                //agent.isStopped = true;
                agent.enabled = false;
                Debug.Log(gameObject.name);
                break;
            case EnemyStates.CHASE:
                agent.destination = playerTrans.position;
                firstRun = true;
                if (Vector3.Distance(transform.position, playerTrans.position) < attackRange && PlayerDeath.Instance.isDeath == false)
                {
                    LookAway.Instance.target = transform;
                    PlayerDeath.Instance.killBy = PlayerDeath.KillBy.NormalEnemy;
                    GameManager.Instance.NotifyObservers();
                }
                firstRun = true;
                break;
            case EnemyStates.ESCAPE:
                // if (firstRun)
                // {
                //     agent.destination = firstEscapePoint.position;
                //     firstRun = !firstRun;
                // }
                if (firstRun == true)
                {
                    GetNewWayPoint();
                    firstRun = false;
                }
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    canEscape = true;
                    timer = countDown;
                }
                dirToPlayer = (playerTrans.position - transform.position).normalized;
                if (canEscape && Vector3.Dot(transform.forward, dirToPlayer) > 0.5f && canSee() )
                {
                    canEscape = false;
                    GetNewWayPoint();
                    Debug.Log("Escape");
                }

                if (Vector3.Distance(transform.position, escapePosition) < 1.5f)
                {
                    Debug.Log("Arrive");
                    GetNewWayPoint();
                }
                break;
        }
        #endregion
    }
}
