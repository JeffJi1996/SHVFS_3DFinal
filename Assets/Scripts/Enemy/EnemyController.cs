using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public enum EnemyStates { CHASE, PATOL, ESCAPE, CATCH }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    protected EnemyStates enemyStates;
    protected NavMeshAgent agent;
    protected Collider colli;
 
    [Header("Level Seting")]
    public float levelWidth;
    public float levelLength;
    
    [Header("Basic Settings")]
    protected float speed;
    public int floor;
    public float attackRange;
    private GameObject player;
    protected Transform playerTrans;

    [SerializeField]
    protected Vector3 escapePosition;
    protected Vector3 dirToPlayer;
    public LayerMask layerMask;

    public bool isActive;
    public bool isChase;
    public bool isEscape;
    public bool isCaught;
    //bool isDead;
    protected bool firstRun;
    protected bool canEscape;
    protected float countDown;
    protected float timer;
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        colli = GetComponent<Collider>();
        escapePosition = new Vector3(0,0,0);
        speed = agent.speed;
        firstRun = true;
        countDown = 0.5f;
        timer = countDown;
    }

    private void Start()
    {
        player = GameManager.Instance.player;
        playerTrans = player.transform;
    }

    
    protected virtual void Update()
    {
        isChase = !player.GetComponent<PlayerAbilityControl>().PowerUpState;
        isEscape = !isChase;
        
    }
    

    protected void GetNewWayPoint()
    {
        #region GetNewPoint

        dirToPlayer = (playerTrans.position - transform.position).normalized;
        float randomX = 0;
        float randomZ = 0;
        if (dirToPlayer.x <= 0 && dirToPlayer.z <= 0)
        {
            randomX = Random.Range(-levelWidth * 0.5f, levelWidth * 0.5f);
            if(randomX < transform.position.x)
                randomZ = Random.Range(transform.position.z , levelLength * 0.5f);
            else
                randomZ = Random.Range(-levelLength * 0.5f, levelLength * 0.5f);
            //Debug.Log(1);
        }
        else if (dirToPlayer.x <= 0 && dirToPlayer.z >= 0)
        {
            randomX = Random.Range(-levelWidth * 0.5f, levelWidth * 0.5f);
            if(randomX < transform.position.x)
                randomZ = Random.Range(-levelLength * 0.5f, transform.position.z);
            else
                randomZ = Random.Range(-levelLength * 0.5f, levelLength * 0.5f);
            //Debug.Log(2);

        }
        else if (dirToPlayer.x >= 0 && dirToPlayer.z <= 0)
        {
            randomX = Random.Range(-levelWidth * 0.5f, levelWidth * 0.5f);
            if(randomX < transform.position.x)
                randomZ = Random.Range(-levelLength * 0.5f , levelLength * 0.5f);
            else
                randomZ = Random.Range(transform.position.z, levelLength * 0.5f);
            //Debug.Log(3);

        }
        else 
        {
            randomX = Random.Range(-levelWidth * 0.5f, levelWidth * 0.5f);
            if(randomX < transform.position.x)
                randomZ = Random.Range(-levelLength * 0.5f , levelLength * 0.5f);
            else
                randomZ = Random.Range( -levelLength * 0.5f,transform.position.z);
            //Debug.Log(4);

        }
        Vector3 randomPoint = new Vector3(randomX, transform.position.y, randomZ);

        NavMeshHit hit;
        escapePosition = NavMesh.SamplePosition(randomPoint, out hit, Mathf.Max(levelLength, levelWidth), 1) ? hit.position : transform.position;
        if (escapePosition != transform.position)
        {
            agent.destination = escapePosition;
            return;
        }
        Debug.Log("Don't have path");
        GetNewWayPoint();
        #endregion
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,escapePosition);
    }

    protected bool canSee()
    {
        Ray myRay = new Ray(transform.position, dirToPlayer);
        Physics.Raycast(myRay, out RaycastHit hitInfo,100f, layerMask,QueryTriggerInteraction.Ignore);
        if (hitInfo.collider.gameObject.GetComponent<PlayerAbilityControl>() != null)
        {
            //Debug.Log(hitInfo.collider.name);
            return true;
        }
        else
        {
            //Debug.Log(hitInfo.collider.name);
            return false;
        }
    }
    
}
