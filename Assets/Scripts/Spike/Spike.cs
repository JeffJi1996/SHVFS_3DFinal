using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public bool isActive;
    private bool doOnce;
    private Vector3 initialPosition;
    [SerializeField] private float GoUpDuration;
    [SerializeField] private float GoDownDuration;
    [SerializeField] private float existTime;

    void Awake()
    {
        isActive = false;
        doOnce = true;
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isActive)
        {
            if (doOnce)
            {
                
            }
        }
    }

    IEnumerator Puncture()
    {
        yield break ;
    }

}
