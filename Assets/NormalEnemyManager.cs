using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyManager : Singleton<NormalEnemyManager>,IEndGameObserver
{
    public int enemyNum;
    public GameObject Boss;

    void Start()
    {
        enemyNum = transform.childCount;
    }

    public void BossState()
    {
        EggManager.Instance.ShowEggs();
        Boss.GetComponent<EnemyBorn>().BossBorn();
        SpikeManager.Instance.ChangeSpikeActiveTime();
    }

    public void EndNotify()
    {
        enemyNum = transform.childCount;
    }
}
