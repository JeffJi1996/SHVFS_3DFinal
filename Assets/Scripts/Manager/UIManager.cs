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

    private void Start()
    {
        curBreakImage.fillAmount = 0;
        breakPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
