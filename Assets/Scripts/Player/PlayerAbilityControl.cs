using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAbilityControl : MonoBehaviour
{
    public static PlayerAbilityControl instance;
    private PlayerAttack AttackAbility;

    private PlayerMovement playerMove;

    private NavMeshObstacle navMeshObstacle;

    public bool PowerUpState;
    public GameObject hand;
    public Material material;
    private Material tempMaterial;

    public ParticleSystem transEffect1;
    public ParticleSystem transEffect2;

    // public event EventHandler OnPowerUp;
    // public event EventHandler OnRecover;
    [SerializeField] public float superDuration;

    void Awake()
    {
        instance = this;
        PowerUpState = false;
        tempMaterial = hand.GetComponent<SkinnedMeshRenderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        AttackAbility = GetComponentInChildren<PlayerAttack>();
        AttackAbility.enabled = false;
        playerMove = GetComponent<PlayerMovement>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();

        navMeshObstacle.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<SpecialCollection>() != null)
        {
            SoundManager.instance.PlaySound("sfx_transform");
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
        hand.GetComponent<SkinnedMeshRenderer>().material = material;

    }

    void RecoverToHuman()
    {
        MiniMap.instance.HideEnemyIcon();
        PowerUpState = false;
        AttackAbility.enabled = false;
        navMeshObstacle.enabled = false;
        playerMove.currentSpeed = playerMove.walkSpeed;
        hand.GetComponent<SkinnedMeshRenderer>().material = tempMaterial;


    }

    void PowerUp()
    {
        PowerUpState = true;
        StartCoroutine(BeingSuper());
    }
    IEnumerator BeingSuper()
    {
        SuperAbility();
        yield return new WaitForSeconds(superDuration);
        RecoverToHuman();
    }
}
