using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyManager : Singleton<NormalEnemyManager>
{
    public int enemyNum;
    public bool BossState;

    void Start()
    {
        enemyNum = transform.childCount;
        BossState = false;
    }

    void Update()
    {
        if (enemyNum == 0)
        {
            BossState = true;
        }
    }
}
