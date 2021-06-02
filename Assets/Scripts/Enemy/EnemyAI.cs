using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int HP;
    private EnemyController enemyController;

    // void OnEnable()
    // {
    //     PlayerAbilityControl.instance.OnPowerUp += EnemyFlee;
    //     PlayerAbilityControl.instance.OnRecover += EnemyChase;
    // }
    //
    // void OnDisable()
    // {
    //     PlayerAbilityControl.instance.OnPowerUp -= EnemyFlee;
    //     PlayerAbilityControl.instance.OnRecover -= EnemyChase;
    // }

    private void EnemyFlee(object sender, EventArgs e)
    {
        enemyController.isEscape = true;
        enemyController.isChase = false;
    }
    void EnemyChase(object sender, EventArgs e)
    {
        enemyController.isEscape = false;
        enemyController.isChase = true;
    }
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (HP == 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " die!");
        Destroy(gameObject);

    }





}
