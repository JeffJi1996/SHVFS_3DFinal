using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClose : MonoBehaviour
{
    public static EnemyClose instance;
    public bool isEnemyClose;
    [SerializeField] private float fieldOfCloseArea;
    [SerializeField] private LayerMask enemyLayer;
    private Collider[] enemyColliders;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isEnemyClose = false;
    }

    void Update()
    {
        enemyColliders = Physics.OverlapSphere(transform.position, fieldOfCloseArea,enemyLayer);
        if (enemyColliders.Length>0)
        {
            isEnemyClose = true;
        }
        else
        {
            Debug.Log("no enemy close");
            isEnemyClose = false;
        }
     
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,fieldOfCloseArea);
    }
}
