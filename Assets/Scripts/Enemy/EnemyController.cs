using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public enum EnemyStates { SLEEP, CHASE, PATOL, ESCAPE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private EnemyStates enemyStates;
    private NavMeshAgent agent;
    private Collider colli;
 
    [Header("Level Seting")]
    public float levelWidth;
    public float levelLength;
    
    [Header("Basic Settings")]
    private float speed;
    public int floor;
    public float attackRange;
    public GameObject player;
    private Transform playerTrans;
    public Transform firstEscapePoint;
    [SerializeField]
    private Vector3 escapePosition;
    private Vector3 dirToPlayer;
    public LayerMask layerMask;

    public bool isActive;
    public bool isChase;
    public bool isEscape;
    bool isDead;
    private bool firstRun;
    private bool canEscape;
    private float countDown;
    private float timer;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        colli = GetComponent<Collider>();
        playerTrans = player.transform;
        escapePosition = new Vector3(0,0,0);
        speed = agent.speed;
        firstRun = true;
        countDown = 0.5f;
        timer = countDown;
    }

    private void Start()
    {
        enemyStates = EnemyStates.SLEEP;
        //GetNewWayPoint();
    }

    private void Update()
    {
        isChase = !player.GetComponent<PlayerAbilityControl>().PowerUpState;
        isEscape = !isChase;
        SwitchStates();
    }

    void SwitchStates()
    {
        if (isDead)
            enemyStates = EnemyStates.DEAD;
        else if (isChase)
            enemyStates = EnemyStates.CHASE;
        else if (isEscape)
            enemyStates = EnemyStates.ESCAPE;

        switch (enemyStates)
        {
            case EnemyStates.SLEEP:
                gameObject.SetActive(false);
                break;
            case EnemyStates.CHASE:
                agent.destination = playerTrans.position;
                firstRun = true;
                if (Vector3.Distance(transform.position, playerTrans.position) < attackRange)
                {
                    Debug.Log("Lose");
                }
                firstRun = true;
                break;
            case EnemyStates.ESCAPE:
                // if (firstRun)
                // {
                //     agent.destination = firstEscapePoint.position;
                //     firstRun = !firstRun;
                // }
                if (firstRun == true)
                {
                    GetNewWayPoint();
                    firstRun = false;
                }
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    canEscape = true;
                    timer = countDown;
                }
                dirToPlayer = (playerTrans.position - transform.position).normalized;
                if (canEscape && Vector3.Dot(transform.forward, dirToPlayer) > 0.5f && canSee() )
                {
                    canEscape = false;
                    GetNewWayPoint();
                    Debug.Log("Escape");
                }

                if (Vector3.Distance(transform.position, escapePosition) < 1.5f)
                {
                    Debug.Log("Arrive");
                    GetNewWayPoint();
                }
                break;
            case EnemyStates.DEAD:
                break;
        }
    }

    private void GetNewWayPoint()
    {
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,escapePosition);
    }

    private bool canSee()
    {
        Ray myRay = new Ray(transform.position, dirToPlayer);
        Physics.Raycast(myRay, out RaycastHit hitInfo,100f, layerMask,QueryTriggerInteraction.Ignore);
        if (hitInfo.collider.gameObject.GetComponent<PlayerAbilityControl>() != null)
        {
            Debug.Log(hitInfo.collider.name);
            return true;
        }
        else
        {
            Debug.Log(hitInfo.collider.name);
            return false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.GetComponent<PlayerAbilityControl>() != null && PlayerAbilityControl.instance.PowerUpState== false)
        {
            player.transform.position = player.GetComponent<PlayerMovement>().InitPosition;
        }
    }
}
