using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChongLuan : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float backForce;
    [SerializeField]
    private float breakForce;

    public float curHealth;

    public bool canAttack;
    public bool isActive;
    // Start is called before the first frame update
    void Awake()
    {
        curHealth = 0;
        canAttack = false;
    }

    private void Update()
    {
        if (canAttack) 
        {
            curHealth -= backForce * Time.deltaTime;
            curHealth = Mathf.Max(curHealth, 0);

            if (PlayerMovement.Instance.LookAtThing() == name && PlayerAbilityControl.instance.PowerUpState)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    curHealth += breakForce;
                }

                curHealth = Mathf.Min(curHealth, maxHealth);
                //Debug.Log(curHealth);
                isActive = true;
                if (EggManager.Instance.isActive)
                {
                    UIManager.Instance.breakPanel.SetActive(true);
                    UIManager.Instance.curBreakImage.fillAmount = curHealth / maxHealth;
                    //Debug.Log(UIManager.Instance.curBreakImage.fillAmount);
                    if (curHealth >= maxHealth)
                    {
                        EggManager.Instance.eggs.Remove(this);
                        EggManager.Instance.amount--;
                        UIManager.Instance.BossHealth(EggManager.Instance.targetAmount - EggManager.Instance.startAmount 
                                                      + EggManager.Instance.amount,EggManager.Instance.targetAmount);
                        if (EggManager.Instance.amount <= EggManager.Instance.startAmount - EggManager.Instance.targetAmount)
                        {
                            EggManager.Instance.Boss.GetComponentInChildren<BossController>().Die();
                        }
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                isActive = false;
            }
        }
    }
}
