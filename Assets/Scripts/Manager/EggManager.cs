using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManager : Singleton<EggManager>
{
    // Start is called before the first frame update
    public List<ChongLuan> eggs;
    public bool isActive;
    public int amount;
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
    }


    // Update is called once per frame
    void Update()
    {
        isActive = false;
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
