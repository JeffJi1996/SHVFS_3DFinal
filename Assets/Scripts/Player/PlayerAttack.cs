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
            SoundManager.instance.PlaySound("sfx_playerPunchwolf");
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<NormalEnemyController>()!= null)
        {
            SoundManager.instance.PlaySound("sfx_enemy01Hurt");
            NormalEnemyManager.Instance.enemyNum--;
            if (NormalEnemyManager.Instance.enemyNum <= 0)
            {
                NormalEnemyManager.Instance.BossState();
                GameManager.Instance.isBossState = true;
            }
            col.GetComponentInParent<EnemyBorn>().Die();
        }

        else if (col.GetComponent<Plank>() != null)
        {
            SoundManager.instance.PlaySound("sfx_playerBreakWood");
            col.gameObject.SetActive(false);
        }

    }
}
