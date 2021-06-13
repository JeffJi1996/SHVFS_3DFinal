using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("EggBreakPanel")]
    public GameObject breakPanel;
    public Image curBreakImage;

    [Header("BossHealthPanel")]
    public GameObject bossHealthPanel;
    public Image curHealth;
    public Image decreaseBar;
    public float lastTime;
    private float bossTimer;
    private float targetNum;
    private void Start()
    {
        curBreakImage.fillAmount = 0;
        breakPanel.SetActive(false);
        bossHealthPanel.SetActive(false);
        bossTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossHealth(float cur,float max)
    {
        targetNum = max;
        bossHealthPanel.SetActive(true);
        curHealth.fillAmount = cur / max;
        StartCoroutine(BossHealthBar());
    }
    
    IEnumerator BossHealthBar()
    {
        while (bossTimer <= 1f)
        {
            bossTimer += Time.deltaTime;
            decreaseBar.fillAmount = curHealth.fillAmount + (1f - bossTimer) / targetNum;
            yield return 0;
        }
        yield return new WaitForSeconds(lastTime);
        bossTimer = 0;
        bossHealthPanel.SetActive(false);
    }
}
