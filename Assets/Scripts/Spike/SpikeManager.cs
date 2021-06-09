using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpikeManager : MonoBehaviour
{
    public bool isActive;
    private bool doOnce;
    public Vector3 initialPosition;

    [Header("Time")]
    [SerializeField] private float GoUpDuration;
    [SerializeField] private float GoDownDuration;
    [SerializeField] private float existTime;

    [Header("GoUpDistance")] [SerializeField]
    private float GoUpPosition;

    void Awake()
    {
        isActive = false;
        doOnce = true;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (isActive)
        {
            if (doOnce)
            {
                StartCoroutine(Puncture());
                doOnce = false;
            }
        }
    }

    void GoUp()
    {
        LeanTween.moveLocalY(this.gameObject,GoUpPosition,GoUpDuration).setEaseInQuint();
    }

    void GoDown()
    {
        LeanTween.moveLocalY(gameObject,initialPosition.y,GoDownDuration).setEaseInQuint();
    }

    IEnumerator Puncture()
    {
        GoUp();
        yield return new WaitForSeconds(GoUpDuration+existTime);
        GoDown();
    }

}