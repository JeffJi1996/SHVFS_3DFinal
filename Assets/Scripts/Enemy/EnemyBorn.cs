using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorn : MonoBehaviour, IEndGameObserver
{
    public GameObject enemy;
    public GameObject portal;
    public float reBornTime;
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
            portal.SetActive(true);
            StartCoroutine(Born());
            
        }
    }

    public void Die()
    {
        enemy.SetActive(false);
        StartCoroutine(Reborn());
    }
 
    public void EndNotify()
    {
        enemy.SetActive(true);
        enemy.transform.position = basicPosition;
        enemy.GetComponent<EnemyController>().isEscape = false;
        enemy.SetActive(false);
        portal.SetActive(false);
        colli.enabled = true;
        StopAllCoroutines();
    }

    IEnumerator Born()
    {
        yield return new WaitForSeconds(bornTime);
        enemy.SetActive(true);
        portal.SetActive(false);
        enemy.GetComponent<EnemyController>().isChase = true;
    }
    IEnumerator Reborn()
    {
        yield return new WaitForSeconds(reBornTime);
        EndNotify();
    }
    
}
