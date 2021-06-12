using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private int damage;
    private Animator anim;
    public bool isAtEgg;
    void Start()
    {
        anim = GetComponent<Animator>();
        isAtEgg = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAtEgg)
        {
            anim.SetTrigger("Attack");
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<NormalEnemyController>()!= null)
        {
            col.GetComponentInParent<EnemyBorn>().Die();
        }
    }
}
