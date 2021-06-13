using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorn : MonoBehaviour,IEndGameObserver
{
    public GameObject enemy;
    public GameObject portal;
    //public float reBornTime;
    public float bornTime;
    private Collider colli;
    private Vector3 basicPosition;
    private void Start()
    {
        colli = GetComponent<Collider>();
        basicPosition = enemy.GetComponent<Transform>().position;
        enemy.SetActive(false);
        portal.SetActive(false);
        GameManager.Instance.AddObserver(this);
    }
    
    void OnDisable()
    {
        if (!GameManager.IsInitialized) return;
        GameManager.Instance.RemoveObserver(this);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            colli.enabled = false;
            StartCoroutine(Born());
        }
    }

    public void Die()
    {
        enemy.SetActive(false);
        //StartCoroutine(Reborn());
    }
 
    public void EndNotify()
    {
        if (GameManager.Instance.isBossState && GetComponentInChildren<BossController>() != null)
        {
            StopAllCoroutines();
            StartCoroutine(BossDelayTrans());
        }
        else if(!GameManager.Instance.isBossState && GetComponentInChildren<NormalEnemyController>() != null)
        {
            StopAllCoroutines();
            StartCoroutine(DelayTrans());
        }
    }

    public void BossBorn()
    {
        StartCoroutine(Born());
    }
    
    IEnumerator Born()
    {
        portal.SetActive(true);
        yield return new WaitForSeconds(bornTime);
        enemy.SetActive(true);
        portal.SetActive(false);
        if (enemy.GetComponent<EnemyController>() != null)
            enemy.GetComponent<EnemyController>().isChase = true;
        else
            enemy.GetComponent<BossController>().isChase = true;
    }

    IEnumerator DelayTrans()
    {
        yield return new WaitForSeconds(2f);
        enemy.SetActive(true);
        enemy.SetActive(false);
        portal.SetActive(false);
        if(colli != null)
            colli.enabled = true;
        enemy.transform.position = basicPosition;
    }
    
    IEnumerator BossDelayTrans()
    {
        yield return new WaitForSeconds(2f);
        enemy.SetActive(true);
        enemy.SetActive(false);
        portal.SetActive(false);
        if(colli != null)
            colli.enabled = true;
        enemy.transform.position = basicPosition;
        StartCoroutine(Born());
    }
    
    
    // IEnumerator Reborn()
    // {
    //     yield return new WaitForSeconds(reBornTime);
    //     EndNotify();
    // }
    
}
