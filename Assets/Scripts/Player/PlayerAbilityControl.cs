using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAbilityControl : MonoBehaviour
{
    public static PlayerAbilityControl instance;
    public PlayerAttack AttackAbility;

    private PlayerMovement playerMove;

    private NavMeshObstacle navMeshObstacle;

    public bool PowerUpState;
    public bool isBossDead;
    public GameObject hand;
    public Material material;
    //private Material tempMaterial;

    public ParticleSystem transEffect1;
    public ParticleSystem transEffect2;

    // public event EventHandler OnPowerUp;
    // public event EventHandler OnRecover;
    [SerializeField] public float superDuration;
    
    private float curDuration;
    private bool playOnce;
    void Awake()
    {
        instance = this;
        PowerUpState = false;
        curDuration = 0;
        playOnce = false;
        //tempMaterial = hand.GetComponent<SkinnedMeshRenderer>().material;
        hand.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //AttackAbility = GetComponentInChildren<PlayerAttack>();
        AttackAbility.enabled = false;
        playerMove = GetComponent<PlayerMovement>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();

        navMeshObstacle.enabled = false;
    }

    private void Update()
    {
        if (curDuration >= 10f)
        {
            playOnce = true;
            SuperAbility();
        }
        else if (curDuration <= 5f && curDuration >= 4.5f && playOnce)
        {
            playOnce = false;
            SoundManager.instance.PlaySound("sfx_heartBeat");
        }
        else if (curDuration <= 0.1f && curDuration > 0 && !playOnce)
        {
            playOnce = true;
            if (!isBossDead)
            {
                SoundManager.instance.PlaySound("sfx_recover");
                RecoverToHuman();
            }
        }
        curDuration -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<SpecialCollection>() != null && !isBossDead)
        {
            SoundManager.instance.PlaySound("sfx_transform");
            SoundManager.instance.PlaySound("sfx_werewolf_Row");
            SoundManager.instance.PlaySound("sfx_collectStone");
            StartCoroutine(col.GetComponent<SpecialCollection>().BeCollected());
            transEffect1.Play();
            transEffect2.Play();
            PowerUp();
        }
    }

    void SuperAbility()
    {
        AttackAbility.enabled = true;
        navMeshObstacle.enabled = true;
        playerMove.currentSpeed = playerMove.SuperSpeed;
        MiniMap.instance.ShowEnemyIcon(1);
        hand.SetActive(true);
        //hand.GetComponent<SkinnedMeshRenderer>().material = material;

    }

    public void RecoverToHuman()
    {
        MiniMap.instance.HideEnemyIcon();
        PowerUpState = false;
        AttackAbility.enabled = false;
        navMeshObstacle.enabled = false;
        playerMove.currentSpeed = playerMove.walkSpeed;
       // hand.GetComponent<SkinnedMeshRenderer>().material = tempMaterial;
        hand.SetActive(false);
        Debug.Log("Recover");
    }

    void PowerUp()
    {
        PowerUpState = true;
        curDuration = superDuration;
        //StartCoroutine(BeingSuper());
    }
    IEnumerator BeingSuper()
    {
        SuperAbility();
        yield return new WaitForSeconds(superDuration);
        RecoverToHuman();
    }

    public void BossDeadState()
    {
        transEffect1.Play();
        transEffect2.loop = true;
        AttackAbility.enabled = true;
        navMeshObstacle.enabled = true;
        playerMove.currentSpeed = playerMove.SuperSpeed;
        navMeshObstacle.enabled = false;
        MiniMap.instance.ShowEnemyIcon(1);
        hand.SetActive(true);
        //hand.GetComponent<SkinnedMeshRenderer>().material = material;
    }
}
