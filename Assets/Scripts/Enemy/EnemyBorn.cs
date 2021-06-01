using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorn : MonoBehaviour
{
    public GameObject enemy;
    private Collider colli;

    private void Start()
    {
        colli = GetComponent<Collider>();
        
        enemy.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);
        if (other.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log(2);

            enemy.SetActive(true);
            enemy.GetComponent<EnemyController>().isChase = true;
            colli.enabled = false;
        }
    }
}
