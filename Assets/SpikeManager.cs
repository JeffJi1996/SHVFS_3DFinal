using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : Singleton<SpikeManager>
{
    [SerializeField]private float NormalActiveTime;
    [SerializeField]private float BossActiveTime;
    public float nowActiveDuration;

    protected override void Awake()
    {
        base.Awake();
        nowActiveDuration = NormalActiveTime;
    }

    public void ChangeSpikeActiveTime()
    {
        nowActiveDuration = BossActiveTime;
    }

}
