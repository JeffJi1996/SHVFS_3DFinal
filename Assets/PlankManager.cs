using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankManager : Singleton<PlankManager>
{
    public List<GameObject> plankList;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            plankList.Add(transform.GetChild(i).gameObject);
        }
    }
    public void Reset()
    {
        foreach (var plank in plankList)
        {
            if (!plank.activeSelf)
            {
                plank.SetActive(true);
            }
        }
    }
}
