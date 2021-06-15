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
        if (isCaught)
            enemyStates = EnemyStates.CATCH;
        else if (isChase)
            enemyStates = EnemyStates.CHASE;
        
        switch (enemyStates)
        {
            case EnemyStates.CATCH:
                transform.forward = new Vector3(playerTrans.position.x - transform.position.x,playerTrans.position.y + 1.5f - transform.position.y, playerTrans.position.z - transform.position.z).normalized;
                agent.enabled = false;
                break;
            case EnemyStates.CHASE:
                agent.destination = playerTrans.position;
                firstRun = true;
                if (Vector3.Distance(transform.position, playerTrans.position) < attackRange && PlayerDeath.Instance.isDeath==false)
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
        attackRange = 0;
        agent.baseOffset = 1;
        agent.isStopped = true;
        GetComponent<AudioSource>().enabled = false;
        PlayerAbilityControl.instance.isBossDead = true;
        SoundManager.instance.PlaySound("sfx_BossDeath");
        PlayerAbilityControl.instance.BossDeadState();
    }
}
