using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();

    public GameObject player;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");
    }

    public void AddObserver(IEndGameObserver observer)
    {
        endGameObservers.Add(observer);
    }

    public void RemoveObserver(IEndGameObserver observer)
    {
        endGameObservers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in endGameObservers)
        {
            observer.EndNotify();
        }
    }
}
