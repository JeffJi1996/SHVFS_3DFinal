using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManager : Singleton<EggManager>,IEndGameObserver
{
    // Start is called before the first frame update
    public List<ChongLuan> eggs;
    public bool isActive;
    public bool isShown;
    public int amount;
    public int startAmount;
    public int targetAmount;
    public GameObject Boss;
    protected override void Awake()
    {
        base.Awake();
        foreach (Transform child in transform) 
        { 
            if (child.GetComponent<ChongLuan>() != null)
                eggs.Add(child.GetComponent<ChongLuan>()); 
        }
        amount = eggs.Count;
        startAmount = amount;
        foreach (var egg in eggs)
        {
            egg.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        isActive = false;
        if (isShown)
        {
            foreach (var egg in eggs)
            {
                if (egg.isActive)
                    isActive = true;
            }

            if (!isActive)
            {
                UIManager.Instance.breakPanel.SetActive(false);
            }
        }
    }

    public void EndNotify()
    {
        if (eggs.Count >= 1)
        {
            foreach (var egg in eggs)
            {
                egg.gameObject.SetActive(false);
            }
        }

        isShown = false;
    }

    public void ShowEggs()
    {
        isShown = true;
        if (eggs.Count >= 1)
        {
            foreach (var egg in eggs)
            {
                egg.gameObject.SetActive(true);
            }
        }
    }
}
